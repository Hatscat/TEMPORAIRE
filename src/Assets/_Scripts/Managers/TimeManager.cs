using UnityEngine;
using System.Collections;

public class TimeManager : BaseManager<TimeManager> {

    public static float f_cooldown; //Inspector

    private float _initCoolDownInS;
    private bool isActive;
    private GameObject display;

    #region BaseManager Overriden Methods
    protected override IEnumerator CoroutineStart()
    {
        yield return null;
        IsReady = true;
        Debug.Log("Init TimeManager");

        f_cooldown = _initCoolDownInS = 240;
        //TODO TEST
        //TimerStart();
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        if (!IsReady) return;
        if (isActive && f_cooldown > 0)
        {
            f_cooldown -= Time.deltaTime;
            //Debug.Log(f_cooldown/60);
            var min = f_cooldown / 60;
            var sec = f_cooldown % 60;
            display.GetComponent<GUIText>().text = string.Format("{0:00}:{1:00}", min, sec);
        }
            
            
    }

    public void TimerStart()
    {
        display = GameObject.Find("gui_cooldown");
        //TODO
        var min = f_cooldown / 60;
        var sec = f_cooldown % 60;
        display.GetComponent<GUIText>().text = string.Format("{0:00}:{1:00}", min, sec);
        isActive = true;
    }

    public void Reset()
    {
        f_cooldown = _initCoolDownInS;
    }
}

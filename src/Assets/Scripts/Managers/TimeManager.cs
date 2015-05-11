using UnityEngine;
using System.Collections;

public class TimeManager : BaseManager<TimeManager> {

    #region BaseManager Overriden Methods
    protected override IEnumerator CoroutineStart()
    {
        yield return null;
        IsReady = true;
        Debug.Log("Init TimeManager");
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        if (!IsReady) return;
    }
}

using UnityEngine;
using System.Collections;

public class NPCManager : BaseManager<NPCManager> 
{
    #region BaseManager Overriden Methods
    protected override IEnumerator CoroutineStart()
    {
        yield return null;
        IsReady = true;
        Debug.Log("Init NPCManager");
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        if (!IsReady) return;
    }
}

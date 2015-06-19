using UnityEngine;
using System.Collections;

public class PlayerManager : BaseManager<PlayerManager>
{

    public static bool takeImplantB;
    public static bool takeImplantA;

	// Use this for initialization
	#region BaseManager Overriden Methods
    protected override IEnumerator CoroutineStart()
    {
        yield return null;
        IsReady = true;
        Debug.Log("Init PlayerManager");
    }
    #endregion
    // Update is called once per frame
	void Update () 
    {
        if (!IsReady) return;
	}
}

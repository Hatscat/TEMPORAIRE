using UnityEngine;
using System.Collections;

public class InputManager : BaseManager<InputManager> {

	#region BaseManager Overriden Methods
	// Use this for initialization
	protected override IEnumerator CoroutineStart ()
	{
		IsReady = true;
		yield break;
	}
	#endregion
	
	// Update is called once per frame
	void Update () {

		if(!IsReady) return;
	
	}
}

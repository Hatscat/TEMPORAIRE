using UnityEngine;
using System.Collections;

public abstract class Racket<T> : BaseManager<T> where T: Component {
    /*
	public IMove moveComponent;

	#region BaseManager Overriden Methods
	protected override IEnumerator CoroutineStart ()
	{
		InitRacket();
		
		IsReady = true;
		yield break;
	}
	#endregion

	protected abstract void InitRacket();

	protected virtual void Update()
	{
		if(!(IsReady && GameManager.manager.IsPlaying)) return;

		if(moveComponent!=null)
			moveComponent.Move();
	}
    */
}

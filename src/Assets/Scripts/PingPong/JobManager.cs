using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// JobManager is just a proxy object so we have a launcher for the coroutines
public class JobManager : MonoBehaviour
{	
	// only one JobManager can exist. We use a singleton pattern to enforce this.
	static JobManager _instance = null;
	
	public static JobManager Instance {
		get {
			if (!_instance) {
				// check if an JobManager is already available in the scene graph
				_instance = FindObjectOfType (typeof(JobManager)) as JobManager;
				
				// nope, create a new one
				if (!_instance) {
					var obj = new GameObject ("JobManager");
					_instance = obj.AddComponent<JobManager> ();
				}
			}
			
			return _instance;
		}
	}
	
	void OnApplicationQuit ()
	{
		// release reference on exit
		_instance = null;
	}
	
}

public class Job
{
	public event System.Action<bool> onJobIsComplete;
	
	private bool _isRunning;
	
	public bool IsRunning { get { return _isRunning; } }
	
	private bool _isPaused;
	
	public bool IsPaused { get { return _isPaused; } }
	
	private IEnumerator _coroutine;
	private bool _jobWasKilled;
	private Stack<Job> _childJobStack;
	
	
	#region constructors
	
	public Job (IEnumerator coroutine) : this( coroutine, true )
	{
	}
	
	public Job (IEnumerator coroutine, bool shouldStart)
	{
		_coroutine = coroutine;
		
		if (shouldStart)
			Start ();
	}
	
	#endregion
	
	
	#region static Job makers
	
	public static Job Make (IEnumerator coroutine)
	{
		return new Job (coroutine);
	}
	
	public static Job Make (IEnumerator coroutine, bool shouldStart)
	{
		return new Job (coroutine, shouldStart);
	}
	
	#endregion
	
	private IEnumerator DoWork ()
	{
		// null out the first run through in case we start paused
		yield return null;
		
		while (_isRunning) {
			if (_isPaused) {
				yield return null;
			} else {
				// run the next iteration and stop if we are done
				if (_coroutine.MoveNext ()) {
					yield return _coroutine.Current;
				} else {
					// run our child jobs if we have any
					if (_childJobStack != null && _childJobStack.Count > 0) {
						Job childJob = _childJobStack.Pop ();
						_coroutine = childJob._coroutine;
					} else
						_isRunning = false;
				}
			}
		}
		
		// fire off a complete event
		if (onJobIsComplete != null)
			onJobIsComplete (_jobWasKilled);
	}
	
	#region public API
	
	public Job CreateAndAddChildJob (IEnumerator coroutine)
	{
		var j = new Job (coroutine, false);
		AddChildJob (j);
		return j;
	}
	
	public void AddChildJob (Job childJob)
	{
		if (_childJobStack == null)
			_childJobStack = new Stack<Job> ();
		_childJobStack.Push (childJob);
	}
	
	public void RemoveChildJob (Job childJob)
	{
		if (_childJobStack.Contains (childJob)) {
			var childStack = new Stack<Job> (_childJobStack.Count - 1);
			var allCurrentChildren = _childJobStack.ToArray ();
			System.Array.Reverse (allCurrentChildren);
			
			for (var i = 0; i < allCurrentChildren.Length; i++) {
				var j = allCurrentChildren [i];
				if (j != childJob)
					childStack.Push (j);
			}
			
			// assign the new stack
			_childJobStack = childStack;
		}
	}

	public void Start ()
	{
		_isRunning = true;
		JobManager.Instance.StartCoroutine (DoWork ());
	}
	
	public IEnumerator StartAsCoroutine ()
	{
		_isRunning = true;
		yield return JobManager.Instance.StartCoroutine( DoWork() );
	}
	
	public void Pause ()
	{
		_isPaused = true;
	}
	
	public void Unpause ()
	{
		_isPaused = false;
	}
	
	public void Kill ()
	{
		_jobWasKilled = true;
		_isRunning = false;
		_isPaused = false;
	}
	
	public void Kill (float delayInSeconds)
	{
		var delay = (int)(delayInSeconds * 1000);
		new System.Threading.Timer (obj =>
		                            {
			lock (this) {
				Kill ();
			}
		}, null, delay, System.Threading.Timeout.Infinite);
	}
	
	#endregion
}
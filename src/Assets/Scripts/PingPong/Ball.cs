using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class Ball : BaseManager<Ball>
{
    /*
	#region Events
	public Action onBallHasBeenHitByPlayerRacket;
	public Action onBallHasBeenMissedByPlayerRacket;
	#endregion

	//ICI TOUS VOS CHAMPS ET PROPRIETES QUI ASSURENT LE GAMEPLAY
	//... TO BE COMPLETED
	//
    public bool applySqrt;
    public float animationDuration;
    public float bouncePosisionZ;
    public Collider limitsCollider;

    private float _target_x;
    private Vector3 _target_pos;

	#region BaseManager Overriden Methods
	protected override void Awake ()
	{
		base.Awake();

		//SI BESOIN, INITIALISEZ CE QUI DOIT L'ETRE AVANT LES START
		// ....TO BE COMPLETED
		//
	}
	
	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		PlayerRacket.manager.onPlayerHasServed-=PlayerHasServed;

		//SI BESOIN, DETRUISEZ CE QUI DOIT L'ETRE 
		// ....TO BE COMPLETED
		//
	}

	// Use this for initialization
	protected override IEnumerator CoroutineStart ()
	{
		PlayerRacket.manager.onPlayerHasServed+=PlayerHasServed;

		//SI BESOIN, INITIALISEZ CE QUI DOIT L'ETRE DANS LE START
		// ....TO BE COMPLETED
		//

		IsReady = true;
		yield  break;
	}

	protected override void Play (params object[] prms)
	{
		//TO BE COMPLETED
        _transform.parent = PlayerRacket.manager.transform;
        _transform.position = _transform.parent.position + Vector3.forward * collider.bounds.extents.z;
    }

	protected override void Pause (params object[] prms)
	{
		//TO BE COMPLETED .... (personnellement je n'en ai pas eu besoin ...)
    }

	protected override void Resume (params object[] prms)
	{
		//TO BE COMPLETED .... (personnellement je n'en ai pas eu besoin ...)
	    
    }

	protected override void Menu (params object[] prms)
	{
		//TO BE COMPLETED
    }

	protected override void GameOver (params object[] prms)
	{
		//TO BE COMPLETED
	}
	#endregion
	
	#region PlayerRacket Callback 
	void PlayerHasServed()
	{
		//TO BE COMPLETED
        _transform.parent = null;
        StartCoroutine("RoundTripAnimation");
	}
	#endregion

	//ICI TOUTES VOS METHODES SPECIFIQUES QUI ASSURENT LE GAMEPLAY
	//... TO BE COMPLETED
	//

    float getProgress (float targetZ, float originZ)
    {
        float distFromOrigin = Mathf.Abs(targetZ - originZ);
        float distMax = Mathf.Abs(EnemyRacket.manager.transform.position.z - PlayerRacket.manager.transform.position.z);
        
        return distFromOrigin / distMax;
    }

    Vector3 getNewTarget (float axisTargetZ, bool justLaunch)
    {
        _target_x = justLaunch ? limitsCollider.bounds.center.x + Random.value * limitsCollider.bounds.size.x - limitsCollider.bounds.extents.x : _target_x;
        float y = justLaunch ? 0f : PlayerRacket.manager.transform.position.y;
        float z = justLaunch ? axisTargetZ * bouncePosisionZ : axisTargetZ;

        return new Vector3(_target_x, y, z);
    }

    IEnumerator RoundTripAnimation ()
    {
        Vector3 target = getNewTarget(EnemyRacket.manager.transform.position.z, true);

        yield return StartCoroutine("GravityMvtToTarget", new object[] {
            target,
	        animationDuration * getProgress(target.z, _transform.position.z),
			applySqrt
		});

        target = getNewTarget(EnemyRacket.manager.transform.position.z, false);
        yield return StartCoroutine("GravityMvtToTarget", new object[] {
            target,
	        animationDuration * getProgress(target.z, _transform.position.z),
			applySqrt
		});

        target = getNewTarget(PlayerRacket.manager.transform.position.z, true);
        yield return StartCoroutine("GravityMvtToTarget", new object[] {
            target,
	        animationDuration * getProgress(target.z, _transform.position.z),
			applySqrt
		});

        target = getNewTarget(PlayerRacket.manager.transform.position.z, false);
        yield return StartCoroutine("GravityMvtToTarget", new object[] {
            target,
	        animationDuration * getProgress(target.z, _transform.position.z),
			applySqrt
		});

        if (_transform.position.x < PlayerRacket.manager.transform.position.x + PlayerRacket.manager.collider.bounds.extents.x
            && _transform.position.x > PlayerRacket.manager.transform.position.x - PlayerRacket.manager.collider.bounds.extents.x)
        {
            onBallHasBeenHitByPlayerRacket();
            StartCoroutine("RoundTripAnimation");
        }
        else
        {
            onBallHasBeenMissedByPlayerRacket();
        }
    }

    IEnumerator GravityMvtToTarget (object[] parameters)
    {
        Vector3 targetPos = (Vector3)parameters[0];
        float duration = (float)parameters[1];
        bool applySqrt = (bool)parameters[2];

        float elapsedTime = 0;
        Vector3 initPos = _transform.position;
        Vector3 velocity0 = ((targetPos - initPos) - .5f * duration * duration * Physics.gravity) / duration;

        while (elapsedTime < duration)
        {
           // while (GameManager.manager.IsPaused)
            //{
            //    yield return null;
            //}

            float k = Mathf.Clamp01(elapsedTime / duration);

            if (applySqrt)
            {
                k = Mathf.Sqrt(k);
            }

            float abstractElapsedTime = k * duration;

            _transform.position = initPos + velocity0 * abstractElapsedTime + .5f * abstractElapsedTime * abstractElapsedTime * Physics.gravity;
            elapsedTime += CustomTimer.manager.DeltaTime;//Time.deltaTime;

            yield return null;
        }

        _transform.position = targetPos;
    }
*/
}

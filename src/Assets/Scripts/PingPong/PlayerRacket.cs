using UnityEngine;
using System.Collections;
using System;

public class PlayerRacket : Racket<PlayerRacket> {
	/*
	public Action onPlayerHasServed;
	
	[HideInInspector]
	public bool PlayerHasServed {get;private set;}
	
	//ICI TOUS VOS CHAMPS ET PROPRIETES QUI ASSURENT LE GAMEPLAY
	//... TO BE COMPLETED
	//
	
	#region BaseManager Overriden Methods
	protected override void Play (params object[] prms)
	{
		PlayerHasServed = false;
		
		//TO BE COMPLETED
	}
	#endregion
	
	#region Racket Overriden Methods
	protected override void InitRacket ()
	{
		PlayerHasServed = false;
		moveComponent = gameObject.AddComponent<MoveFromInput>();
		moveComponent.Init(30f,AXIS.x);

		//TO BE COMPLETED
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		//TO BE COMPLETED ... mais ce qui est sur c'est que quelque part vous devrez écrire la ligne ci-dessous
		//if(onPlayerHasServed!=null) onPlayerHasServed();

        if (!PlayerHasServed && GameManager.manager.IsPlaying && Input.GetAxis("Fire1") != 0 && onPlayerHasServed != null)
        {
            PlayerHasServed = true;
            onPlayerHasServed();
        }
		
	}
	#endregion
    
	//ICI TOUTES VOS METHODES SPECIFIQUES QUI ASSURENT LE GAMEPLAY
	//... TO BE COMPLETED (...personnellement je n'ai pas eu besoin de rajouter des méthodes
	//
     */
}

using UnityEngine;
using System.Collections;

public class EnemyRacket : Racket<EnemyRacket> {

	//ICI TOUS VOS CHAMPS ET PROPRIETES QUI ASSURENT LE GAMEPLAY
	//... TO BE COMPLETED
	//
	#region Racket Overriden Methods 
	protected override void InitRacket ()
	{
		moveComponent = gameObject.AddComponent<MoveFollowTarget>();
		moveComponent.Init(Ball.manager.transform,AXIS.x);

		//TO BE COMPLETED
	}
	#endregion

	//ICI TOUTES VOS METHODES SPECIFIQUES QUI ASSURENT LE GAMEPLAY
	//... TO BE COMPLETED (...personnellement je n'ai pas eu besoin de rajouter des méthodes
	//
}

using UnityEngine;
using System.Collections;

public class AlertKitchen : MonoBehaviour {

    public GameObject startPointCuistotA;
    public GameObject startPointCuistotB;
    public GameObject startPointCuistotC;

    public GameObject robotCuistotA;
    public GameObject robotCuistotB;
    public GameObject robotCuistotC;

    public GameObject startPointPlayer;
    public GameObject player;
    

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    //Voir avec le wait dans behavior du cuistot pour la durée d'attente
    public void Alert()
    {
        player.transform.position = startPointPlayer.transform.position;

        robotCuistotA.transform.position = startPointCuistotA.transform.position;
        robotCuistotB.transform.position = startPointCuistotB.transform.position;
        robotCuistotC.transform.position = startPointCuistotC.transform.position;
    }
}

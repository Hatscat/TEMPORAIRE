using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class AlertKitchen : MonoBehaviour {

    public GameObject startPointCuistotA;
    public GameObject startPointCuistotB;
    public GameObject startPointCuistotC;

    public GameObject robotCuistotA;
    public GameObject robotCuistotB;
    public GameObject robotCuistotC;

    public GameObject startPointPlayer;
    public GameObject player;
    
    public AudioSource alertMusic;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    //Voir avec le wait dans behavior du cuistot pour la durée d'attente
    public void DialogAlert()
    {
        DialogueManager.ShowAlert("Fouttez le camp de ma cuisine ! ALERTE !");
    }

    public void Alert()
    {
        alertMusic.Stop();
        player.transform.position = startPointPlayer.transform.position;

        robotCuistotA.transform.position = startPointCuistotA.transform.position;
        robotCuistotB.transform.position = startPointCuistotB.transform.position;
        robotCuistotC.transform.position = startPointCuistotC.transform.position;
		
    }

    public void AlertMusicFeedback()
    {
        alertMusic.Play();
    }
}

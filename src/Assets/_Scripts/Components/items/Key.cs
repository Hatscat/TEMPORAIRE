using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class Key : MonoBehaviour {

    public string name;
    public string message;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter()
    {
        switch (name)
        {
	        case "ImplantA":
		        PlayerManager.manager.key_implant_A = true;
                DialogueManager.ShowAlert(message);
                Destroy(gameObject);
		        break;
	        case "ImplantB":
		        PlayerManager.manager.key_implant_B = true;
                DialogueManager.ShowAlert(message);
                Destroy(gameObject);
		        break;
		
		}
    }
}

using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class TriggerDropKey : MonoBehaviour {
	
	public string name; //Inspector
	
	// Use this for initialization
	void Start () 
	{
		//GameObject newObject = Instantiate(go_objectMDL, go_SpawnObject.transform.position,transform.rotation) as GameObject;
        switch (name)
        {
            case "Black":
                PlayerManager.manager.key_black_material = true;
				DialogueManager.ShowAlert("Vous recevez code de la 'Salle Secrete'" );
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void OnConversationEnd() {
		switch (name){
		case "Armor":
			PlayerManager.manager.key_armory = true;
			DialogueManager.ShowAlert("Vous recevez code de 'l'Amurie'");
			break;
		case "ImplantA":
			PlayerManager.manager.key_implant_A = true;
			break;
		case "ImplantB":
			PlayerManager.manager.key_implant_B = true;
			break;
		case "Area2":
			PlayerManager.manager.key_area_2 = true;
			DialogueManager.ShowAlert("Vous recevez code de la 'salle des Machines'");
			break;
		case "Cokpit":
			PlayerManager.manager.key_cokpit = true;
			DialogueManager.ShowAlert("Vous recevez code du 'Cokpit'");
			break;
		case "Black":
			PlayerManager.manager.key_black_material = true;
			DialogueManager.ShowAlert("Vous recevez code de la 'Salle Matiere noire'");
			break;
		case "Ammo":
			PlayerManager.manager.key_room_ammo = true;
			DialogueManager.ShowAlert("Vous recevez code de la 'Salle Munition'");
			break;
		}
	}
}

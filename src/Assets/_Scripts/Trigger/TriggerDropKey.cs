using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class TriggerDropKey : MonoBehaviour {
	
	public string name; //Inspector
	
	// Use this for initialization
	void Start () 
	{
		//GameObject newObject = Instantiate(go_objectMDL, go_SpawnObject.transform.position,transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void OnConversationEnd() {
		switch (name){
		case "Armor":
			PlayerManager.manager.key_armory = true;
			break;
		case "ImplantA":
			PlayerManager.manager.key_implant_A = true;
			break;
		case "ImplantB":
			PlayerManager.manager.key_implant_B = true;
			break;
		case "Area2":
			PlayerManager.manager.key_area_2 = true;
			break;
		case "Cokpit":
			PlayerManager.manager.key_cokpit = true;
			break;
		case "Black":
			PlayerManager.manager.key_black_material = true;
			break;
		case "Ammo":
			PlayerManager.manager.key_room_ammo = true;
			break;
		}
	}
}

using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class TriggerSpawnObject : MonoBehaviour {
	
	public GameObject go_objectMDL; //Inspector

	// Use this for initialization
	void Start () 
	{
		//GameObject newObject = Instantiate(go_objectMDL, go_SpawnObject.transform.position,transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnConversationEnd(Transform go_SpawnObject) {
		GameObject newObject = Instantiate(go_objectMDL, go_SpawnObject.position,transform.rotation) as GameObject;

	}
}

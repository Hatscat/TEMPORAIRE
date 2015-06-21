using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class TriggerDropCollider : MonoBehaviour {
	
	public GameObject go_objectMDL; //Inspector
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnConversationEnd(Transform go_SpawnObject) {
		//GameObject newObject = Instantiate(go_objectMDL, go_SpawnObject.position,transform.rotation) as GameObject;
		go_objectMDL.GetComponent<MeshCollider> ().isTrigger = true;
	}
}
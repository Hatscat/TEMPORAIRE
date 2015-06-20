using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class TriggerAlertMessage : MonoBehaviour {
	public string message;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnTriggerEnter(Collider other) {
		DialogueManager.ShowAlert(message);
		Destroy(gameObject);
	}

	public void Message() {
		DialogueManager.ShowAlert(message);
	}
}

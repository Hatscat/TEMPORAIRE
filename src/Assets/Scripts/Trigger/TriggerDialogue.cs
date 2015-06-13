using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.Examples {
	
	public class TriggerDialogue : MonoBehaviour {
		
		public string message;
		
		public void OnTriggerEnter(Collider other) {
			//DialogueManager.ShowAlert("Got " + name);
			Sequencer.Message(message);
			Destroy(gameObject);
		}
		
	}
	
}

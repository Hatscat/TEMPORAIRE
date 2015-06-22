using UnityEngine;
using System.Collections;

public class TriggerAutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if(PlayerManager.manager.key_armory)
        {
          Destroy(gameObject);
        }
      
    }
}

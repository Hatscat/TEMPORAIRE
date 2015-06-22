using UnityEngine;
using System.Collections;

public class TriggerChangeStateMission : MonoBehaviour {


    public string mission;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        GameManager.manager.mission = mission;
        Destroy(gameObject);
    }
}

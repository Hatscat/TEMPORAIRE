using UnityEngine;
using System.Collections;

public class AlertTutorial : MonoBehaviour {

    public GameObject go_pointStart;
    public GameObject go_player;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ActionAlert()
    {
        go_player.transform.position = go_pointStart.transform.position;
    }
}

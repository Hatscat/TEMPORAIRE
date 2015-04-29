using UnityEngine;
using System.Collections;

public class STW_demo_FollowAgent : MonoBehaviour {

	public Transform agent;


	// Update is called once per frame
	void Update () 
	{
		camera.transform.LookAt(agent);
	}
}

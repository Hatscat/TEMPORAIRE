using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    public float timer;

	// Use this for initialization
	void Start () 
    {
        if (timer != 0)
            timer = 3;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
	}


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT!");
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Enter called");
    }
}

using UnityEngine;
using System.Collections;

public class AutoDestroyExplosion : MonoBehaviour {

    public float f_cooldown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        f_cooldown -= Time.deltaTime;
        if (f_cooldown <= 0)
            Destroy(gameObject);
	}
}

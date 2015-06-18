using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed;
    public Vector3 direction;

	void Start () {
        //direction = Vector3.forward;
        speed = 10;
        
	}

	
	void Update () 
    {

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        Destroy(transform);
    }
}

using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject Bullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void shoot()
    {
        GameObject go =
                Instantiate(Bullet,
                            transform.position,
                            Bullet.transform.rotation)
                    as GameObject;
        go.GetComponent<NewBehaviourScript>().direction = transform.forward;
    }
}

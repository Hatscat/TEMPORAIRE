using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject go_bulletMDL; //Inspector
    public GameObject go_SpawnBullet;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActionShoot()
    {
        GameObject newBullet = Instantiate(go_bulletMDL, go_SpawnBullet.transform.position, Quaternion.identity) as GameObject;
                      
    }
}

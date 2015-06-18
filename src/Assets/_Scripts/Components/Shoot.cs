using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject go_bulletMDL; //Inspector
    public GameObject go_SpawnBullet;
	Animation anim;
	// Use this for initialization
	void Start () 
    {
		anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActionShoot()
    {
        GameObject newBullet = Instantiate(go_bulletMDL, go_SpawnBullet.transform.position, Quaternion.identity) as GameObject;
    }
}

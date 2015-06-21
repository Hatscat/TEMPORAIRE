using UnityEngine;
using System.Collections;

public class PlayerManager : BaseManager<PlayerManager>
{

    //Boost les stats du player
    public bool takeImplantB; //Plus de vie
    public bool takeImplantA; //Fait plus de dmg

    //TODO Pour le debug et la présentation, laisser le visible
    public bool key_armory;
    public bool key_implant_A;
    public bool key_implant_B;
    public bool key_area_2;
    public bool key_cokpit;
    public bool key_black_material;
    public bool key_room_ammo;

	// Use this for initialization
	#region BaseManager Overriden Methods
    protected override IEnumerator CoroutineStart()
    {
        yield return null;
        IsReady = true;
        Debug.Log("Init PlayerManager");
    }
    #endregion
    // Update is called once per frame
	void Update () 
    {
        if (!IsReady) return;
	}
}

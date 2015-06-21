using UnityEngine;
using System.Collections;

public class DoorBlackMaterial : Door {

    public override void SetPlayerCanOpen()
    {
        this.playerCanOpen = PlayerManager.manager.key_black_material;
    }
	
}

using UnityEngine;
using System.Collections;

public class DoorRoomAmmo : Door {

    public override void SetPlayerCanOpen()
    {
        this.playerCanOpen = PlayerManager.manager.key_room_ammo;
    }
	
}

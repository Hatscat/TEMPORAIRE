using UnityEngine;
using System.Collections;

public class DoorArea2 : Door {

    public override void SetPlayerCanOpen()
    {
        this.playerCanOpen = PlayerManager.manager.key_area_2;
    }
}

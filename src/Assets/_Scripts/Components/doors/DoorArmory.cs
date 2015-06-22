using UnityEngine;
using System.Collections;

public class DoorArmory : Door {

    public override void SetPlayerCanOpen()
    {
        this.playerCanOpen = PlayerManager.manager.key_armory;
    }
}

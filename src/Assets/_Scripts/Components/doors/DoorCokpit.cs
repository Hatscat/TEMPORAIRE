using UnityEngine;
using System.Collections;

public class DoorCokpit : Door {

    public override void SetPlayerCanOpen()
    {
        this.playerCanOpen = PlayerManager.manager.key_cokpit;
    }
}

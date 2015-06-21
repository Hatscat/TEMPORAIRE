using UnityEngine;
using System.Collections;

public class DoorImplantB : Door {

    public override void SetPlayerCanOpen()
    {
        this.playerCanOpen = PlayerManager.manager.key_implant_B;
    }
}

using UnityEngine;
using System.Collections;

public class DoorImplantA : Door {

    public override void SetPlayerCanOpen()
    {
        this.playerCanOpen = PlayerManager.manager.key_implant_A;
    }
}

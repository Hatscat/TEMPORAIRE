using UnityEngine;
using System.Collections;

public class DoorArmory : Door {

    public override void SetPlayerCanOpen()
    {
        Debug.Log("hello");
        this.playerCanOpen = GameManager.manager.key_armory;
    }
}

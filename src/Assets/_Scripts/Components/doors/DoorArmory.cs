using UnityEngine;
using System.Collections;

public class DoorArmory : Door {



    

    void Start()
    {

       

    }


   


    void Update()
    {
        this.playerCanOpen = GameManager.manager.key_armory;
    }


}

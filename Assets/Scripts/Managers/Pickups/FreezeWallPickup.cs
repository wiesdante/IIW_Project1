using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeWallPickup : Pickup
{
    public override void PickupAction(GameObject other)
    {
        //Write everyhing BEFORE base call 



        base.PickupAction(other);
    }

}

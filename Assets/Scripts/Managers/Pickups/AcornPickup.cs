using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornPickup : Pickup
{
    public override void PickupAction(GameObject other)
    {
        score.IncreaseScore(1);
        base.PickupAction(other);
    }
}

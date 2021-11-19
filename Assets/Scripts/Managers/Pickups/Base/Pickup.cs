using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ScoreManager score;
    public float DestroyTime = 0;
    void Start()
    {
        score = ScoreManager.Instance;
    }
    public virtual void PickupAction(GameObject other)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PickupAction(other.gameObject);
            Destroy();
        }
    }
    private void Destroy()
    {
        //pickupManager.SpawnPickup(transform.parent);
        Destroy(gameObject, DestroyTime);
    }
    

}

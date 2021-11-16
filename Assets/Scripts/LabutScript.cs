using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabutScript : MonoBehaviour
{
    bool collided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Labut") || collision.transform.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(9);
            collided = true;
        }
    }
}

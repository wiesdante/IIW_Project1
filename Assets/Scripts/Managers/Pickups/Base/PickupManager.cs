using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pickups;
    public static PickupManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    public void SpawnPickup(Transform spawnGO)
    {
        StartCoroutine(SpawnPickupCorountine(spawnGO));
    }
    private IEnumerator SpawnPickupCorountine(Transform pickupLocation)
    {
        yield return new WaitForSeconds(3f);
        if (pickupLocation.childCount == 0)
        {
            GameObject pickup = Instantiate(pickups[Random.Range(0, pickups.Count)], pickupLocation.position, Quaternion.identity, pickupLocation);
        }

    }
    public void ResetPickups()
    {
        foreach (Transform spawnLocation in this.transform)
        {
            StartCoroutine(SpawnPickupCorountine(spawnLocation));
        }
    }
}

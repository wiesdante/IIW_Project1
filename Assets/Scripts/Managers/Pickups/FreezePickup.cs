using UnityEngine;

namespace Managers.Pickups
{
    public class FreezePickup : MonoBehaviour
    {
        [SerializeField] private float freezeTime;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EventManager.Instance.InvokeGlobalFreeze(freezeTime);
                Destroy(this.gameObject);
            }
        }
    }
}

using UnityEngine;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        public delegate void PickupEvent(float time);

        public static event PickupEvent GlobalFreeze;

        public void InvokeGlobalFreeze(float freezeTime)
        {
            GlobalFreeze?.Invoke(freezeTime);
        }
    }
}

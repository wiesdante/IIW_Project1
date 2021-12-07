using System;
using UnityEngine;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        public delegate void PickupEvent(float time);

        public static event PickupEvent GlobalFreeze;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                InvokeGlobalFreeze(3);
            }
        }

        public void InvokeGlobalFreeze(float freezeTime)
        {
            GlobalFreeze?.Invoke(freezeTime);
        }
    }
}

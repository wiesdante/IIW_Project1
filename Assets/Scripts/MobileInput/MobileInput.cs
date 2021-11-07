using System;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance;
    public bool locked;
    public event Action<Vector3> onReleaseEvent;
    public event Action<Vector3> onHoldEvent; 
    public event Action<Vector3> onStartEvent; 

    private void Awake()
    {
    #region Singleton
            if(Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
        #endregion
        locked = false;
    }
    protected Touch _touch;
    private void Update()
    {
        if(!locked)
        {
            GetTouchDirection();
        }
    }
    protected void InvokeStart(Vector3 dir)
    {
        onStartEvent?.Invoke(dir);
    }
    public virtual void GetTouchDirection()
    { 
    }
    protected void InvokeRelease(Vector3 dir)
    {
        onReleaseEvent?.Invoke(dir);
    }
    protected void InvokeHold(Vector3 dir)
    {
        onHoldEvent?.Invoke(dir);
    }
}

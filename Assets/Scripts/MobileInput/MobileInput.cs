using System;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance;
    protected bool locked;
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
        if(!locked && GameManager.gameStarted)
        {
            GetTouchDirection();
        }
       
    }
    protected void InvokeStart(Vector3 dir)
    {
        if(!locked) onStartEvent?.Invoke(dir);
    }
    public virtual void GetTouchDirection()
    { 
    }
    protected void InvokeRelease(Vector3 dir)
    {
        if (!locked) onReleaseEvent?.Invoke(dir);
       
    }
    protected void InvokeHold(Vector3 dir)
    {
        if (!locked) onHoldEvent?.Invoke(dir);
    }
    public void LockInput()
    {
        locked = true;
    }
    public void UnlockInput()
    {
        locked = false;
    }
}

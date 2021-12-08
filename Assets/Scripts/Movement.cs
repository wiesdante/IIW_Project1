using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    MobileInput input;
    Rigidbody rb;
    bool forceApplied;
    LevelManager levelMan;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<DragRelease>();
        input.onReleaseEvent += Input_onReleaseEvent;
        input.onStartEvent += Input_onStartEvent;
        forceApplied = false;
        levelMan = LevelManager.Instance;
    }

    private void Input_onStartEvent(Vector3 obj)
    {
        if(forceApplied)
        {
            input.LockInput();
        }
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < 5f && rb.angularVelocity.magnitude > 1f)
        {
            rb.AddForce(rb.velocity * -2f, ForceMode.Acceleration);
        }
        if (rb.velocity.magnitude < 1f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            input.UnlockInput();
        }
    }

    private void Input_onReleaseEvent(Vector3 obj)
    {
        levelMan.DecreaseLife();
        rb.AddForce(obj, ForceMode.Impulse);
        forceApplied = true;
        input.LockInput();
    }
}

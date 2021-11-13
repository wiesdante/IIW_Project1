using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    MobileInput input;
    Rigidbody rb;
    bool onGround;
    void Start()
    {
        onGround = false;
        rb = GetComponent<Rigidbody>();
        input = GetComponent<DragRelease>();
        input.onReleaseEvent += Input_onReleaseEvent;
        
    }
    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < 0.2f )
        {
            rb.velocity = Vector3.zero;
            input.locked = false;
        }
    }

    private void Input_onReleaseEvent(Vector3 obj)
    {
        rb.AddForce(obj, ForceMode.Impulse);
    }
        
}

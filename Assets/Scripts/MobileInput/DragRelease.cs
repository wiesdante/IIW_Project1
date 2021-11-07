using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragRelease : MobileInput
{
    [Range(0.01f, 1f)]
    public float speed = 1;
    Vector2 startPoint = Vector2.zero;
    Vector2 endPoint = Vector2.zero;
    public float maxLength;


    public override void GetTouchDirection()
    {
        Vector3 dir = Vector3.zero;
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    startPoint = _touch.position;
                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    endPoint = _touch.position;
                    break;
            }
            dir = endPoint - startPoint;
            dir = ModifyMobileInput(dir);

            if (_touch.phase == TouchPhase.Ended)
            {
                startPoint = Vector3.zero;
                endPoint = Vector3.zero;
                InvokeRelease(dir);
            }
            else if (_touch.phase == TouchPhase.Began)
            {
                InvokeStart(dir);
            }
            else if(_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary )
            {
                InvokeHold(dir);
            }
        }
        
    }
    private Vector3 ModifyMobileInput(Vector3 dir)
    {
        //If player drags upward direction will be on the backward on character so we reversing it

        if (endPoint.y > startPoint.y)
        {
            dir.y *= -1f;
            dir.x *= -1f;
        }
        dir.z = dir.y;
        dir.y = 0;
        dir *= speed;
        double lineLength = Math.Pow( Math.Pow(dir.z, 2) + Math.Pow(dir.x, 2), 0.5);
        if(lineLength > maxLength)
        {
            dir = dir.normalized;
            dir *= maxLength;
        }
        return dir;
    }
}

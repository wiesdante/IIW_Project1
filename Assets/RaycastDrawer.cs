using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDrawer : MonoBehaviour
{
    [SerializeField] GameObject hitSphere;
    MobileInput input;
    LineRenderer renderer;
    private void Start()
    {
        input = GetComponent<DragRelease>();
        renderer = GetComponent<LineRenderer>();
        input.onStartEvent += Input_onStartEvent;
        input.onHoldEvent += Input_onHoldEvent;
        input.onReleaseEvent += Input_onReleaseEvent;
    }

    private void Input_onReleaseEvent(Vector3 obj)
    {

    }

    private void Input_onHoldEvent(Vector3 direction)
    {
        Vector3[] points = new Vector3[] { transform.position, Vector2.zero , Vector2.zero};
        points[1] = points[0] + direction;
        if(Physics.Raycast(transform.position, direction, out RaycastHit hit))
        {
            hitSphere.transform.position = hit.point;
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                Vector3 dir = (hit.point - points[0]); // TO WALL DIR
                if(dir.magnitude < direction.magnitude)
                {
                    dir.x *= -1;
                    float dirLength = direction.magnitude - dir.magnitude;
                    if (dirLength > 0)
                    {
                        dir = dir.normalized;
                        dir *= dirLength;
                    }
                    points[2] = hit.point + dir;
                    points[1] = hit.point;
                }
                else
                {
                    points[2] = points[0];
                }
            }
        }
        else
        {
            points[2] = points[0];
        }
        points[1].y = points[0].y;
        points[2].y = points[1].y;
        renderer.SetPositions(points);
    }

    private void Input_onStartEvent(Vector3 obj)
    {

    }
}

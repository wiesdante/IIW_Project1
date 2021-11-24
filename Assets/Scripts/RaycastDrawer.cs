using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDrawer : MonoBehaviour
{
    [SerializeField] GameObject hitSphere;
    MobileInput input;
    LineRenderer renderer;

    [SerializeField] float animationSpeed = 0.5f;
    [SerializeField] float lineWidth = 3f;
    private void Start()
    {
        input = GetComponent<DragRelease>();
        renderer = GetComponent<LineRenderer>();
        input.onHoldEvent += Input_onHoldEvent;
        input.onReleaseEvent += Input_onReleaseEvent;
        renderer.enabled = false;
        renderer.widthMultiplier = lineWidth;
    }


    private void Input_onHoldEvent(Vector3 direction)
    {
        if(!renderer.enabled)
        {
            renderer.enabled = true;
        }

        renderer.material.SetTextureOffset("_BaseMap", Vector2.right * Time.time * animationSpeed);

        Vector3[] points = new Vector3[] { transform.position, Vector2.zero , Vector2.zero};
        points[1] = points[0] + direction;
        if(Physics.Raycast(transform.position, direction, out RaycastHit hit, direction.magnitude))
        {
            if(hitSphere.activeSelf)
            {
                hitSphere.transform.position = hit.point;
            }
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                Vector3 dir = (hit.point - points[0]); // TO WALL DIR
                if (dir.magnitude < direction.magnitude)
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
                    if (Physics.Raycast(points[1], points[2], out RaycastHit hit2, dirLength))
                    {
                        if (hit2.collider.gameObject.CompareTag("Obstacle") || hit2.collider.gameObject.CompareTag("Labut"))
                        {
                            points[2] = hit2.point;
                            if (hit2.collider.gameObject.CompareTag("Obstacle")) hitSphere.SetActive(true);
                        }
                        else
                        {
                            points[2] = hit2.point;
                            hitSphere.SetActive(false);
                        }
                    }
                }
                else
                {
                    points[2] = points[1];
                }
                hitSphere.SetActive(false);
            }
            else if (hit.collider.gameObject.CompareTag("Obstacle") || hit.collider.gameObject.CompareTag("Labut"))
            {
                points[1] = hit.point;
                points[2] = points[1];
                if(hit.collider.gameObject.CompareTag("Obstacle")) hitSphere.SetActive(true);
            }
            else
            {
                points[2] = points[1];
                hitSphere.SetActive(false);
            }
        }
        else
        {
            points[2] = points[1];
        }
        points[1].y = points[0].y;
        points[2].y = points[1].y;
        renderer.SetPositions(points);
    }

    private void Input_onReleaseEvent(Vector3 obj)
    {
        renderer.enabled = false;
        hitSphere.SetActive(false);
    }
}

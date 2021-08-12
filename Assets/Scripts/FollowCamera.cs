using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    private MoveAni ani;
    public List<Transform> targets;
    public Transform pl1, pl2;
    public Vector3 offset;           // phan bu
    public float smoothTime = .5f;
    public float minZoom;
    public float maxZoom;
    public float zoomLimiter;

    public bool hyo = true;
    private Vector3 velocity  = Vector3.zero;
    private Camera cam;

//toch move camre
    private Vector3 touchStart;
    public float groundZ;

    private void Start()
    {
        cam = GetComponent<Camera>();
        ani = GetComponent<MoveAni>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) & hyo)
        {
            touchStart = GetWorldPosition(groundZ);
            
        }

        if (Input.GetMouseButton(0) & hyo)
            {
                Vector3 direction = touchStart - GetWorldPosition(groundZ);
                Camera.main.transform.position += direction;
            }
           
    }
    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        Move();
      
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
         // cam.fieldOfView =Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
       cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }


    void Move()
    {
        // Vector3 centerPoint = GetCenterPoint();
        if (Check)
        {
            Vector3 newPosition = pl1.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        }
        else
        {
            Vector3 newPosition2 = pl2.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition2, ref velocity, smoothTime);
        }
    }
    public bool Check;
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}

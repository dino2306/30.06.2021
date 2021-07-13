using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
  //  public MoveAni moveAnin;
    public List<Transform> targets;
    public Vector3 offset;           // phan bu
    public float smoothTime = .5f;
    public float minZoom;
    public float maxZoom;
    public float zoomLimiter;


    private Vector3 velocity;
    private Camera cam;



    public Vector2 touchPosition;
    private float swipeResistance = 200.0f;

    private void Start()
    {
        cam = GetComponent<Camera>();
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            touchPosition = Input.mousePosition;
          
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float swipeForce = touchPosition.x - Input.mousePosition.x;
            if (Mathf.Abs(swipeForce) > swipeResistance)
            {
                if (swipeForce < 0)
                {
                    SlideCamera(true);

                }
                else
                {
                    SlideCamera(false);
                }
            }
        }
        

    }
    public void SlideCamera(bool left)
    {
        
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
        //  cam.fieldOfView =Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
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
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
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

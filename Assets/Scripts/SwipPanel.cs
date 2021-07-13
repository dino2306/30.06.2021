using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipPanel : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.position;
    }
    public void OnDrag(PointerEventData data)
    {
         Debug.Log(data.pressPosition - data.position);
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0) * Time.deltaTime;
    }
    public void OnEndDrag(PointerEventData data)
    {
        panelLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static GameObject itemDragging;
    //https://www.youtube.com/watch?v=WBSNrqM707Y

    Vector3 startPosition;
    Transform startParent;
    Transform dragParent;

    void Start()
    {
        //dragParent = GameObject.FindGameObjectWithTag("Draggeable_Item").transform;        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragging = gameObject;
        PlayerPrefs.SetString("foodSelected", gameObject.tag);
        startPosition = transform.position;
        startParent = transform.parent;
        //transform.SetParent(dragParent);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Debug.Log("POSICION MOUSE X" + Input.mousePosition.x);
        //Debug.Log("POSICION MOUSE Y" + Input.mousePosition.y);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        itemDragging = null;
        transform.position = startPosition;
        /*if (transform.parent == dragParent)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);            
        }*/
    }
}

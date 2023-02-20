using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FreeDraw;

public class Func_DragObject_FreeSticker : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private SData_NodeData nodeData;
    private RectTransform rect;
    private Vector2 mousePos = Vector2.zero;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rect.position = mousePos;


    }
    public void OnEndDrag(PointerEventData eventData)
    {
     
    }
}

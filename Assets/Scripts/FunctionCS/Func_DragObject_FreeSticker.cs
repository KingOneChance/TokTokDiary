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
    [SerializeField] private float dragAreaMaxX = 0f;
    [SerializeField] private float dragAreaMinX = 0f;
    [SerializeField] private float dragAreaMaxY = 0f;
    [SerializeField] private float dragAreaMinY = 0f;
    [SerializeField] private bool InArea = true;
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
        if (mousePos.x > dragAreaMaxX || mousePos.y > dragAreaMaxY || mousePos.x < dragAreaMinX || mousePos.y < dragAreaMinY)
        {
            InArea = false;
            rect.position = new Vector2(Mathf.Clamp(rect.position.x, dragAreaMinX, dragAreaMaxX),
                                        Mathf.Clamp(rect.position.y, dragAreaMinY, dragAreaMaxY));
        }
        else
        {
            InArea = true;
            rect.position = mousePos;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Å¬¸¯³¡");
    }
}

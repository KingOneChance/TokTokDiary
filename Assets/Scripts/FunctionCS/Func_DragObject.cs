using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Func_DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    SData_NodeData nodeData;
    RectTransform rect;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        nodeData = new SData_NodeData();
        nodeData.position = rect.position;
        nodeData.rotation = rect.rotation.eulerAngles;
        nodeData.scale = rect.localScale;
        Manager_Main.Instance.manager_PictureDiary.AddDragBegin(nodeData, gameObject);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rect.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        nodeData = new SData_NodeData();
        nodeData.position = rect.position;
        nodeData.rotation = rect.rotation.eulerAngles;
        nodeData.scale = rect.localScale;
        Manager_Main.Instance.manager_PictureDiary.AddDrangEnd(nodeData, gameObject);
    }
}

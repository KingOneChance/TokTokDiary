using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Func_BasicDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    protected RectTransform myRect = null;

    protected void Start()
    {
        myRect = GetComponent<RectTransform>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        myRect.position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
    }
}

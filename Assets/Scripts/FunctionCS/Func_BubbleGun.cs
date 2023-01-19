using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Func_BubbleGun : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] RectTransform ui_transform_icon;
    [SerializeField] Image ui_backGround;
    private Vector3 startPos;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Input.mousePosition;
        ui_transform_icon.position = mousePos;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = ui_backGround.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ui_transform_icon.position = startPos;
    }

}

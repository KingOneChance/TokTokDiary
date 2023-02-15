using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Func_DetectAttach : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UI_PictureDiary ui_PictureDiary = null;
    [SerializeField] private Func_DragObject func_DragObject = null;
    [SerializeField] private EventTrigger myEventTrigger = null;

    private void Start()
    {
        ui_PictureDiary = FindObjectOfType<UI_PictureDiary>();
        func_DragObject = FindObjectOfType<Func_DragObject>();
        myEventTrigger = gameObject.AddComponent<EventTrigger>();
    }

    

    public void OnClick_MouseType()
    {
        if (ui_PictureDiary.MouseStateInfo == MouseType.Niddle)
        {
            func_DragObject.enabled = false;
        }
        else if (ui_PictureDiary.MouseStateInfo == MouseType.BubbleStick)
        {
            func_DragObject.enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick_MouseType();
    }
}

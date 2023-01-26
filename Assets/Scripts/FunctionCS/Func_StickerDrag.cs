using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FreeDraw;
public delegate void Del_DrawStop(bool isStop);
public class Func_StickerDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect = null;
    private Vector2 mousePos = Vector2.zero;
    public Del_DrawStop del_DrawStop = null;
    private Func_Draw func_draw = null;

    [SerializeField] private bool isClickMe = false;

    private RectTransform rectTransform = null;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        func_draw = FindObjectOfType<Func_Draw>();
        del_DrawStop = func_draw.StopDraw;

        rect = GetComponent<RectTransform>();
    }


    public void OnClick_MyButton()
    {
        isClickMe = true;
    }


    public void OnDrag(PointerEventData eventData)
    {
        del_DrawStop(true);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rect.transform.position = mousePos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Ω√¿€");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("≥°");
        del_DrawStop(false);
    }

}

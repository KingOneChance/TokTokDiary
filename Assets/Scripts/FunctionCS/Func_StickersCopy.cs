using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FreeDraw;

public class Func_StickersCopy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("===copy object===")]
    [SerializeField] private GameObject mySticker = null;
    [SerializeField] private GameObject myParents = null;
    [SerializeField] private GameObject myGrandPa = null;
    [SerializeField] private GameObject myPos = null;
    [SerializeField] private Func_TodayFeelingImage drawObject = null;
    private RectTransform myRectTransform;
    private RectTransform copyRectTransform;
    private RectTransform startTransform;
    private Vector2 mousePos = Vector2.zero;

    private void Start()
    {
        drawObject = GetComponent<Func_TodayFeelingImage>();    
        myRectTransform = GetComponent<RectTransform>();
        startTransform.position = myRectTransform.position;
    }
    public void OnClick_MakeClone()
    {
      

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(myGrandPa.transform);
    }
    public void OnDrag(PointerEventData eventData)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myRectTransform.position = mousePos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject newSticker = Instantiate(mySticker,myGrandPa.transform);
        copyRectTransform = newSticker.GetComponent<RectTransform>();
        copyRectTransform = myRectTransform;

        
        myRectTransform.position = myPos.transform.position;

        gameObject.transform.SetParent(myParents.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FreeDraw;
using UnityEngine.UI;
public class Func_StickersCopy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("===copy object===")]
    [SerializeField] private GameObject mySticker = null;
    [SerializeField] private GameObject myParents = null;
    [SerializeField] private GameObject myGrandPa = null;
    [SerializeField] private GameObject myPos = null;
    [SerializeField] private Func_TodayFeelingImage drawObject = null;
    [Header("===Texture===")]
    [SerializeField] private RawImage mainSticker = null;
    [SerializeField] private RawImage signSticker = null;

    private RectTransform myRectTransform;
    private RectTransform copyRectTransform;
    private RectTransform startTransform;
    private Vector2 mousePos = Vector2.zero;

    private void Start()
    {
        drawObject = FindObjectOfType<Func_TodayFeelingImage>();
        myRectTransform = GetComponent<RectTransform>();
    }
    public void OnClick_MakeClone()
    {


    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        drawObject.IsStickerMaking(true);
        gameObject.transform.SetParent(myGrandPa.transform);
    }
    public void OnDrag(PointerEventData eventData)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myRectTransform.position = mousePos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject newSticker = Instantiate(mySticker, myGrandPa.transform);
        copyRectTransform = newSticker.GetComponent<RectTransform>();
        copyRectTransform = myRectTransform;
        newSticker.AddComponent<Func_DragObject>();
        newSticker.AddComponent<Func_DetectOnSticker>();

        Destroy(newSticker.GetComponent<Func_StickersCopy>());
        newSticker.transform.localScale *= 2;//new Vector3(220 / 160f, 220 / 160f, 220 / 160f);
        myRectTransform.position = myPos.transform.position;

        //nodedata plus
        SData_NodeData temp = new SData_NodeData();
        RectTransform tempRect = new RectTransform();

        tempRect = newSticker.GetComponent<RectTransform>();
        temp.position = tempRect.position;
        temp.rotation = tempRect.rotation.eulerAngles;
        temp.scale = tempRect.localScale;
        Manager_Main.Instance.manager_PictureDiary.AddDragInit(temp, newSticker);
        //

        gameObject.transform.SetParent(myParents.transform);
        drawObject.IsStickerMaking(false);

        //gameObject.transform.localScale = Vector3.one;
    }
}

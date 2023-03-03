using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Func_Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Manager_BubbleBear manager_BubbleBear = null;
    [SerializeField] private GameObject restartButton = null;
    [SerializeField] private GameObject beforeButton = null;
    [SerializeField] private GameObject finishCanvas = null;
    [SerializeField] private GameObject checkCanvas = null;
    [SerializeField] private GameObject hitBoxPos = null;
    [SerializeField] private GameObject myPos = null;
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private bool isOn = false;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isOn == true) return;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isOn == true) return;
        //it is for calculating mousePosiont value 
        Vector2 test = Input.mousePosition * 0.01f;
        test.x = test.x - 960 * 0.01f;
        test.y = test.y - 540 * 0.01f;

        rectTransform.transform.position = test;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isOn == true) return;
        gameObject.transform.position = myPos.transform.position;
        //change canvas when drag sign on hedgehog sticker
    }
    //It is called by OnCollisionEnter2D function
    public void LoadFinishCanvas() 
    {
        manager_BubbleBear.MoveCheckToFinishCanvas();
    }
    private void OnMouseUp()
    {
        if (isOn == false) gameObject.transform.position = myPos.transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOn = true;
        gameObject.transform.position = hitBoxPos.transform.position;
        Manager_Main.Instance.GetAudio().PlaySound("ComeBack", SoundType.Touch, gameObject, false, true);
        Invoke("LoadFinishCanvas", 1f);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isOn == true) gameObject.transform.position = hitBoxPos.transform.position;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isOn == true) gameObject.transform.position = hitBoxPos.transform.position;
    }
}

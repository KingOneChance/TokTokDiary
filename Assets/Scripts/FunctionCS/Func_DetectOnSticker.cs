using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Func_DetectOnSticker : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UI_PictureDiary ui_PictureDiary = null;
    [SerializeField] private Func_DragObject func_DragObject = null;

    [SerializeField] private RawImage sticker = null;
    [SerializeField] private RawImage bubble = null;
    [SerializeField] private RawImage sign = null;
    

    private void Start()
    {
        ui_PictureDiary = FindObjectOfType<UI_PictureDiary>();
        func_DragObject = FindObjectOfType<Func_DragObject>();

        sticker = gameObject.GetComponent<RawImage>();
        sign = gameObject.transform.GetChild(0).GetComponent<RawImage>();
        bubble = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>();
        if(sign.texture == null)
        sign.color = new Color(255, 255, 255, 0);
    }
    public void OnClick_MouseType()
    {
        if (ui_PictureDiary.MouseStateInfo == MouseType.Niddle)
        {
            func_DragObject.enabled = false; 
            bubble.gameObject.SetActive(false);
            Manager_Main.Instance.GetAudio().PlaySound("PopBubble", SoundType.Common, gameObject, false, true);
        }
        else if (ui_PictureDiary.MouseStateInfo == MouseType.BubbleStick)
        {
            func_DragObject.enabled = true;
            bubble.gameObject.SetActive(true);
            Manager_Main.Instance.GetAudio().PlaySound("BubbleStick", SoundType.Diary, gameObject, false, true);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick_MouseType();
    }
}

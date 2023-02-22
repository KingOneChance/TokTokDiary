using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Func_DeleteSticker : Func_DragAndDrop
{
    [SerializeField] private Image deletePopUp = null;
    [SerializeField] private Image trashCan = null;
    [SerializeField] private Sprite openTrashCan = null;

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);        
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if(Vector3.Magnitude(gameObject.transform.position - myDestinationPos.position) < 0.01f)
        {
            Manager_Main.Instance.UI_StickerRepository.CheckStickerCount(gameObject);
            //ui ╤Г╬Наж╠Б
            deletePopUp.gameObject.SetActive(true);
            trashCan.sprite = openTrashCan;
            StartCoroutine( ResetPosition());
        }

       
    }

    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(2f);
        isDropDone = false;
        myRect.position = myInitRect.position;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Func_ToBeSticker : MonoBehaviour
{
    [SerializeField] private RawImage[] ui_Stikcers;

    [SerializeField] private RawImage soapBubble = null;

    private Vector2 mousePos = Vector2.zero;




    public void OnClick_InventorySticker(int stickerNum)
    {
        soapBubble.gameObject.SetActive(true);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        soapBubble.transform.position = mousePos;
        soapBubble.texture = ui_Stikcers[stickerNum].texture;
    }
}
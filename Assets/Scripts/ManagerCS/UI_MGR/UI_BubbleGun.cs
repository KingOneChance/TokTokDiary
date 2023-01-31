using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BubbleGun : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas = null;
    [SerializeField] private Canvas subCanvas = null;

    [SerializeField] private RawImage sticker = null;

    Func_OnTriggerStay func_OnTriggerStay = null;

    private void Start()
    {
        func_OnTriggerStay = FindObjectOfType<Func_OnTriggerStay>();
    }
    private void OnDisable()
    {
        sticker.texture = func_OnTriggerStay.go.GetComponent<RawImage>().texture;
    }

    public void OnClick_GoBubbleGun()
    {
        mainCanvas.gameObject.SetActive(true);
        subCanvas.gameObject.SetActive(false);
    }
}

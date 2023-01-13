using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FreeDraw;
using TMPro;
using System.IO;
public class Func_BubbleBearSave : MonoBehaviour
{
    [Header("===Panel===")]
    [SerializeField] private GameObject panel = null;
    [Header("===RawImageSpace===")]
    [SerializeField] private RawImage space = null;
    [Header("===Scripts===")]
    [SerializeField] private Func_Drawable drawable = null;

    [Header("===Text===")]
    [SerializeField] private TextMeshProUGUI spriteText = null;
    private Sprite saveSprite = null;

    private void Awake()
    {
        drawable.gameObject.SetActive(true);
    }
    public void GetSaveSPrite(Sprite sprite)
    {
        if (sprite != null) spriteText.text = "is not null";
        else spriteText.text = "is null";

        saveSprite = sprite;
        space.texture = saveSprite.texture;
    }
    public void OnClick_PencileButton()
    {

    }
    public void OnClick_RealSave()
    {
            
    }
    //I have to add Saving record file Function.
    //I have to add Saving sign file Function.

}

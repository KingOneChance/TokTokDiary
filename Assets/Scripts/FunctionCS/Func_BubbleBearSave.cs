using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Func_BubbleBearSave : MonoBehaviour
{
    [Header("===Panel===")]
    [SerializeField] private GameObject panel = null;
    [Header("===RawImageSpace===")]
    [SerializeField] private RawImage space = null;

    private Sprite saveSprite = null;
    public void GetSaveSPrite(Sprite sprite)
    {
        saveSprite = sprite;
        space.texture = sprite.texture;
    }
    //I have to add Saving record file Function.
    //I have to add Saving sign file Function.

}

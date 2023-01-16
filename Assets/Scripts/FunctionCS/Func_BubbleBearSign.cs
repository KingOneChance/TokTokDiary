using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Func_BubbleBearSign : MonoBehaviour
{
    [Header("===Space===")]
    [SerializeField] private RawImage spaceSign = null;
    [SerializeField] private RawImage spaceBear = null;

    [Header("===SaveOject===")]
    [SerializeField] private GameObject saveObjectFile = null;

    private Sprite signSprite = null;
    private Sprite bearSprite = null;
   
    public void GetSignSPrite(Sprite sprite)
    {
        signSprite = sprite;
        spaceSign.texture = sprite.texture;
    }
    public void GetBearSPrite(Sprite sprite)
    {
        bearSprite = sprite;
        spaceBear.texture = sprite.texture;
    }
    public void LoadOjbect()
    {

    }
}

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
    [Header("===DrawCamera===")]
    [SerializeField] private Camera drawCam=null;
   
    

    public void GetSignSPrite(Texture sprite)
    {
        spaceSign.texture = sprite;
    }
    public void GetBearSPrite(Texture sprite)
    {
        spaceBear.texture = sprite;
    }
    public void LoadOjbect()
    {

    }
}

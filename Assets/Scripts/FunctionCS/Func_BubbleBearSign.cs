using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Func_BubbleBearSign : MonoBehaviour
{
    [Header("===Space===")]
    [SerializeField] private RawImage tempSign = null;
    [Header("===SaveOject===")]
    [SerializeField] private GameObject saveObjectFile = null;
    [SerializeField] private RawImage signImage = null;
    [SerializeField] private RawImage hedgehogImage = null;
    [Header("===ShowOject===")]
    [SerializeField] private RawImage hedgehogShow = null;
    [SerializeField] private RawImage signShow = null;


    [Header("===DrawCamera===")]
    [SerializeField] private Camera drawCam=null;
    [Header("===Script===")]
    [SerializeField] private Manager_BubbleBear manager_bubble= null;



    [SerializeField] private RectTransform beforeSignRect = null;
    [SerializeField] private float startXPos;
    [SerializeField] private float startYPos;
    [SerializeField] private int widthValue;
    [SerializeField] private int heightValue;


    private void Start()
    {
        Debug.Log(saveObjectFile.transform.position);
        beforeSignRect = saveObjectFile.GetComponent<RectTransform>();
        startXPos = saveObjectFile.gameObject.transform.position.x + beforeSignRect.rect.position.x+960;
        startYPos = saveObjectFile.gameObject.transform.position.y + beforeSignRect.rect.position.y+540;
        widthValue = (int)beforeSignRect.rect.width;
        heightValue = (int)beforeSignRect.rect.height;
     //   Debug.Log(startXPos);
    }

    public void SetImage()
    {
        hedgehogShow.texture = hedgehogImage.texture;
        signShow.texture = signImage.texture;
    }

    public void SaveTempSign()
    {
        StartCoroutine(Co_ScreenShotFrame());
    }
    private IEnumerator Co_ScreenShotFrame()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(widthValue, heightValue, TextureFormat.RGB24, false);

        Rect rex = new Rect(startXPos, startYPos, widthValue, heightValue);

        tex.ReadPixels(rex, 0, 0);
        tex.Apply();

        tempSign.texture = tex;
        drawCam.gameObject.SetActive(false);
        manager_bubble.MoveSignToCheckCanvas();
    }
}
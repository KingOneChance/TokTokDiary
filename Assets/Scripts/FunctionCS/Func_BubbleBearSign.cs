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
    [SerializeField] private GameObject backGroundShow = null;


    [Header("===DrawCamera===")]
    [SerializeField] private Camera drawCam = null;
    [Header("===Script===")]
    [SerializeField] private Manager_BubbleBear manager_bubble = null;
    [Header("===RemoveColor===")]
    [SerializeField] private Color backGroundColor;
    [SerializeField] private Color backGroundColor2;

    [SerializeField] private RectTransform beforeSignRect = null;
    [SerializeField] private float startXPos;
    [SerializeField] private float startYPos;
    [SerializeField] private int widthValue;
    [SerializeField] private int heightValue;


    private void Start()
    {
        Debug.Log(saveObjectFile.transform.position);
        beforeSignRect = saveObjectFile.GetComponent<RectTransform>();
        startXPos = saveObjectFile.gameObject.transform.position.x + beforeSignRect.rect.position.x + 960;
        startYPos = saveObjectFile.gameObject.transform.position.y + beforeSignRect.rect.position.y + 540;
        widthValue = (int)beforeSignRect.rect.width;
        heightValue = (int)beforeSignRect.rect.height;
        //Debug.Log(startXPos);
    }

    public void CancelSign()
    {
        GameObject[] cancelList = GameObject.FindGameObjectsWithTag("Line");
        for(int i = 0; i < cancelList.Length; i++)
        {
            Destroy(cancelList[i]);
        }
    }

    public void SetImage()
    {
        hedgehogShow.texture = hedgehogImage.texture;
        signShow.texture = signImage.texture;
    }

    public void SaveTempSign()
    {
        backGroundShow.SetActive(false);
        StartCoroutine(Co_ScreenShotFrame());
    }
    private IEnumerator Co_ScreenShotFrame()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(widthValue, heightValue, TextureFormat.RGB24, false);

        Rect rex = new Rect(startXPos, startYPos, widthValue, heightValue);

        tex.ReadPixels(rex, 0, 0);
        tex.Apply();


        //
        Debug.Log("BGC 22: " + backGroundColor);
        Texture2D newTex = new Texture2D(widthValue, heightValue);
        for (int x = 0; x < widthValue; x++)
        {
            for (int y = 0; y < heightValue; y++)
            {
                Color pixelColor = tex.GetPixel(x, y);
                if (pixelColor != backGroundColor)
                {
                    newTex.SetPixel(x, y, pixelColor);
                }
                else
                {
                    newTex.SetPixel(x, y, Color.clear);
                }
            }
        }
        newTex.Apply();
        //
        tempSign.texture = newTex;

        drawCam.gameObject.SetActive(false);
        manager_bubble.MoveSignToCheckCanvas();

        backGroundShow.SetActive(true);
    }
}
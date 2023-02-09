using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Func_BubbleBearCtrl : MonoBehaviour
{
    [Header("===BubbleStickers===")]
    [SerializeField] private Sprite testBear1 = null;
    [SerializeField] private Sprite testBear2 = null;
    [SerializeField] private Sprite testBear3 = null;
    [SerializeField] private Sprite testBear4 = null;
    [SerializeField] private Sprite testBear5 = null;
    //
    [Header("===ToggleButtons===")]
    [SerializeField] private Toggle[] buttonBody = null;
    [SerializeField] private Toggle[] buttonBelly = null;
    [SerializeField] private Toggle[] buttonHead = null;
    [SerializeField] private Toggle[] buttonArmLeg = null;

    [Header("===BubbleHedgehogCanvas===")]
    [SerializeField] private RawImage bubbleHedgehogCapture = null;
    [SerializeField] private RawImage bubbleHedgehogBody = null;
    [SerializeField] private RawImage bubbleHedgehogBelly = null;
    [SerializeField] private RawImage bubbleHedgehogHead = null;
    [SerializeField] private RawImage bubbleHedgehogArmLeg = null;
    [SerializeField] private GameObject bubbleHedgehogAccPalmtree = null;
    [SerializeField] private GameObject bubbleHedgehogAccVolleyball = null;
    [SerializeField] private GameObject bubbleHedgehogAccHeadBand = null;
    [SerializeField] private GameObject bubbleHedgehogAccCon = null;
    [SerializeField] private RawImage bubbleHedgehogOriginBody = null;
    [SerializeField] private RawImage bubbleHedgehogOriginBelly = null;
    [SerializeField] private RawImage bubbleHedgehogOriginHead = null;
    [SerializeField] private RawImage bubbleHedgehogOriginArmLeg = null;

    [Header("===TemporarySaveImage===")]
    [SerializeField] private RawImage tempHedgehog = null;
    [SerializeField] private RectTransform beforeHogRect = null;
    [SerializeField] private float startXPos;
    [SerializeField] private float startYPos;
    [SerializeField] private int widthValue;
    [SerializeField] private int heightValue;

    [Header("===Scripts===")]
    [SerializeField] private Manager_BubbleBear manager_BubbleBear = null;
    [SerializeField] private Func_RemoveBackGround func_RemoveBackgound = null;

    [Header("===RemoveColor===")]
    [SerializeField] private Color backGroundColor;

    [Header("===NextButton===")]
    [SerializeField] private GameObject nextButton;

    private bool isSelectedBody = false;
    private bool isSelectedBelly = false;
    private bool isSelectedHead = false;
    private bool isSelectedArmLeg = false;

    public Sprite nowSprite { get; private set; }
    //new ver
    public Sprite nowBodySprite { get; private set; }
    public Sprite nowBellySprite { get; private set; }
    public Sprite nowHeadSprite { get; private set; }
    public Sprite nowArmLegSprite { get; private set; }
    public bool canMoveToAudio { get; private set; } = false;

    private void Start()
    {
        beforeHogRect = bubbleHedgehogCapture.GetComponent<RectTransform>();
        startXPos = bubbleHedgehogCapture.gameObject.transform.position.x + beforeHogRect.rect.position.x + 960;
        startYPos = bubbleHedgehogCapture.gameObject.transform.position.y + beforeHogRect.rect.position.y + 540;
        widthValue = (int)beforeHogRect.rect.width;
        heightValue = (int)beforeHogRect.rect.height;
        nextButton.SetActive(false);
    }
    #region Hedgehog Make button
    public void OnClick_ButtonBody1()//summer
    {
        ToggleAlphaChange(buttonBody);
        nowBodySprite = buttonBody[0].image.sprite;
        bubbleHedgehogBody.texture = nowBodySprite.texture;
        if (isSelectedBody != true) isSelectedBody = true;
    }
    public void OnClick_ButtonBody2()
    {
        ToggleAlphaChange(buttonBody);
        nowBodySprite = buttonBody[1].image.sprite;
        bubbleHedgehogBody.texture = nowBodySprite.texture;
        if (isSelectedBody != true) isSelectedBody = true;
    }
    public void OnClick_ButtonBody3()
    {
        ToggleAlphaChange(buttonBody);
        nowBodySprite = buttonBody[2].image.sprite;
        bubbleHedgehogBody.texture = nowBodySprite.texture;
        if (isSelectedBody != true) isSelectedBody = true;
    }
    public void OnClick_ButtonBelly1()
    {
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[0].image.sprite;
        bubbleHedgehogBelly.texture = nowBellySprite.texture;
        AccessoryChange(AccessoryType.VolleyBall);  //ball
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonBelly2()
    {
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[1].image.sprite;
        bubbleHedgehogBelly.texture = nowBellySprite.texture;
        AccessoryChange(AccessoryType.None);  //no accessory
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonBelly3()
    {
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[2].image.sprite;
        bubbleHedgehogBelly.texture = nowBellySprite.texture;
        AccessoryChange(AccessoryType.None);  //no accessory
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonHead1()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[0].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        AccessoryChange(AccessoryType.HeadPalmtree);  //HeadPalmtree
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonHead2()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[1].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        AccessoryChange(AccessoryType.Con);  //Con
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonHead3()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[2].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        AccessoryChange(AccessoryType.HeadBand);  //HeadBand
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonArmLeg1()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[0].image.sprite;
        bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
        canMoveToAudio = true;
        nextButton.SetActive(true);
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
    }
    public void OnClick_ButtonArmLeg2()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[1].image.sprite;
        bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
        canMoveToAudio = true;
        nextButton.SetActive(true);
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
    }
    public void OnClick_ButtonArmLeg3()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[2].image.sprite;
        bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
        canMoveToAudio = true;
        nextButton.SetActive(true);
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
    }
    public void OnClick_ButtonRandom()
    {
        int randomSkin = Random.Range(0, 50); //2ÆÛ¼¾Æ®

        if(randomSkin ==1) //origin skin
        {
            AccessoryChange(AccessoryType.None);
            buttonBody[randomSkin].isOn = true;
            buttonBelly[randomSkin].isOn = true;
            buttonHead[randomSkin].isOn = true;
            buttonArmLeg[randomSkin].isOn = true;
            bubbleHedgehogBody.texture = bubbleHedgehogOriginBody.texture;
            bubbleHedgehogBelly.texture = bubbleHedgehogOriginBelly.texture;
            bubbleHedgehogHead.texture = bubbleHedgehogOriginHead.texture;
            bubbleHedgehogArmLeg.texture = bubbleHedgehogOriginArmLeg.texture;
            if (isSelectedBody != true) isSelectedBody = true;
            if (isSelectedBelly != true) isSelectedBelly = true;
            if (isSelectedHead != true) isSelectedHead = true;
            if (isSelectedArmLeg != true) isSelectedArmLeg = true;
        }
        else
        {
            int body, belly, head, amrleg;
            body = Random.Range(0, 3);
            ToggleAlphaChange(buttonBody);
            nowBodySprite = buttonBody[body].image.sprite;
            bubbleHedgehogBody.texture = nowBodySprite.texture;
            if (isSelectedBody != true) isSelectedBody = true;

            belly = Random.Range(0, 3);
            ToggleAlphaChange(buttonBelly);
            nowBellySprite = buttonBelly[belly].image.sprite;
            bubbleHedgehogBelly.texture = nowBellySprite.texture;
            if (belly == 0) AccessoryChange(AccessoryType.VolleyBall); //ball
            else if (belly == 1) AccessoryChange(AccessoryType.None);  //none
            else if (belly == 2) AccessoryChange(AccessoryType.None);  //none
            if (isSelectedBelly != true) isSelectedBelly = true;

            head = Random.Range(0, 3);
            ToggleAlphaChange(buttonHead);
            nowHeadSprite = buttonHead[head].image.sprite;
            bubbleHedgehogHead.texture = nowHeadSprite.texture;
            if (belly == 0) AccessoryChange(AccessoryType.HeadPalmtree);    //HeadPalmtree
            else if (belly == 1) AccessoryChange(AccessoryType.Con);        //Con
            else if (belly == 2) AccessoryChange(AccessoryType.HeadBand);   //HeadBand
            if (isSelectedHead != true) isSelectedHead = true;

            amrleg = Random.Range(0, 3);
            ToggleAlphaChange(buttonArmLeg);
            nowArmLegSprite = buttonArmLeg[amrleg].image.sprite;
            bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
            if (isSelectedArmLeg != true) isSelectedArmLeg = true;

            buttonBody[body].isOn = true;
            buttonBelly[body].isOn = true;
            buttonHead[body].isOn = true;
            buttonArmLeg[body].isOn = true;
        }
        canMoveToAudio = true;
        nextButton.SetActive(true);
    }

    #endregion
    //Change Button Color when Click toggle button
    private void ToggleAlphaChange(Toggle[] toggles)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn) toggles[i].image.color = new Color(255, 255, 255, 0.3f);
            else toggles[i].image.color = new Color(255, 255, 255, 1);
        }
    }
    private void AccessoryChange(AccessoryType type)
    {
        bubbleHedgehogAccPalmtree.SetActive(false);
        bubbleHedgehogAccVolleyball.SetActive(false);
        bubbleHedgehogAccHeadBand.SetActive(false);
        bubbleHedgehogAccCon.SetActive(false);
        switch (type)
        {
            case AccessoryType.VolleyBall:
                bubbleHedgehogAccVolleyball.SetActive(true);
                break;
            case AccessoryType.HeadPalmtree:
                bubbleHedgehogAccPalmtree.SetActive(true);
                break;
            case AccessoryType.HeadBand:
                bubbleHedgehogAccHeadBand.SetActive(true);
                break;
            case AccessoryType.Con:
                bubbleHedgehogAccCon.SetActive(true);
                break;
            case AccessoryType.None:
                Debug.Log("There is no accessory");
                break;

            default:
                Debug.Log("There is no type");
                break;
        }
    }

    public bool IsSelected(SelectHedgehogState part)
    {
        switch (part)
        {
            case SelectHedgehogState.Body:
                if (isSelectedBody == true) return true;
                break;
            case SelectHedgehogState.Belly:
                if (isSelectedBelly == true) return true;
                break;
            case SelectHedgehogState.Head:
                if (isSelectedHead == true) return true;
                break;
            case SelectHedgehogState.ArmLeg:
                if (isSelectedArmLeg == true) return true;
                break;
            default:
                return false;
        }
        return false;
    }
    public void SaveTempHedgeHog()
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

        //
        Debug.Log("BGC : " + backGroundColor);
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
        tempHedgehog.texture = newTex;
        manager_BubbleBear.MoveMakeToAudioCanvas();
    }
}

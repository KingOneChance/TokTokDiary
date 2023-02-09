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
    [SerializeField] private RawImage bubbleHedgehogOriginBody = null;
    [SerializeField] private RawImage bubbleHedgehogOriginBelly = null;
    [SerializeField] private RawImage bubbleHedgehogOriginHead = null;
    [SerializeField] private RawImage bubbleHedgehogOriginArmLeg = null;
    [SerializeField] private GameObject bubbleHedgehogAccPalmtree = null;
    [SerializeField] private GameObject bubbleHedgehogAccVolleyball = null;
    [SerializeField] private GameObject bubbleHedgehogAccHeadBand = null;
    [SerializeField] private GameObject bubbleHedgehogAccCon = null;
    [SerializeField] private GameObject bubbleHedgehogOriginStage = null;

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

    [SerializeField] private bool isSelectedBody = false;
    [SerializeField] private bool isSelectedBelly = false;
    [SerializeField] private bool isSelectedHead = false;
    [SerializeField] private bool isSelectedArmLeg = false;
    [SerializeField] private int nowHeadNum = -1;
    [SerializeField] private int nowBellyNum = -1;


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
        // startXPos = bubbleHedgehogCapture.gameObject.transform.position.x + beforeHogRect.rect.position.x + 960;
        // startYPos = bubbleHedgehogCapture.gameObject.transform.position.y + beforeHogRect.rect.position.y + 540;
        startXPos = beforeHogRect.rect.position.x + 960;
        startYPos = beforeHogRect.rect.position.y + 540;
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
        nowBellyNum = 0;
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[0].image.sprite;
        bubbleHedgehogBelly.texture = nowBellySprite.texture;
        if (isSelectedHead == false)
            AccessoryChange(AccessoryType.VolleyBall);  //ball
        else
            AccessoryChange(AccessoryType.VolleyBall, nowBellyNum, nowHeadNum);  //ball
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonBelly2()
    {
        nowBellyNum = 1;
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[1].image.sprite;
        bubbleHedgehogBelly.texture = nowBellySprite.texture;
        if (isSelectedHead == false)
            AccessoryChange(AccessoryType.None);  //no accessory
        else
            AccessoryChange(AccessoryType.VolleyBall, nowBellyNum, nowHeadNum);
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonBelly3()
    {
        nowBellyNum = 2;
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[2].image.sprite;
        bubbleHedgehogBelly.texture = nowBellySprite.texture;
        if (isSelectedHead == false)
            AccessoryChange(AccessoryType.None);  //no accessory
        else
            AccessoryChange(AccessoryType.VolleyBall, nowBellyNum, nowHeadNum);
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonHead1()
    {
        nowHeadNum = 0;
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[0].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        AccessoryChange(AccessoryType.HeadPalmtree, nowBellyNum);  //HeadPalmtree
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonHead2()
    {
        nowHeadNum = 1;
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[1].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        AccessoryChange(AccessoryType.Con, nowBellyNum); //Con
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonHead3()
    {
        nowHeadNum = 2;
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[2].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        AccessoryChange(AccessoryType.HeadBand, nowBellyNum);  //HeadBand
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

        if (randomSkin == 1) //origin skin
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
            belly = Random.Range(0, 3);
            head = Random.Range(0, 3);
            amrleg = Random.Range(0, 3);
            nowBellyNum = belly;
            nowHeadNum = head;

            ToggleAlphaChange(buttonBody);
            nowBodySprite = buttonBody[body].image.sprite;
            bubbleHedgehogBody.texture = nowBodySprite.texture;
            if (isSelectedBody != true) isSelectedBody = true;


            ToggleAlphaChange(buttonBelly);
            nowBellySprite = buttonBelly[belly].image.sprite;
            bubbleHedgehogBelly.texture = nowBellySprite.texture;
            if (belly == 0) AccessoryChange(AccessoryType.VolleyBall, belly, head); //ball
            else if (belly == 1) AccessoryChange(AccessoryType.None, belly, head);  //none
            else if (belly == 2) AccessoryChange(AccessoryType.None, belly, head);  //none
            if (isSelectedBelly != true) isSelectedBelly = true;


            ToggleAlphaChange(buttonHead);
            nowHeadSprite = buttonHead[head].image.sprite;
            bubbleHedgehogHead.texture = nowHeadSprite.texture;
            if (head == 0) AccessoryChange(AccessoryType.HeadPalmtree, belly);    //HeadPalmtree
            else if (head == 1) AccessoryChange(AccessoryType.Con, belly);        //Con
            else if (head == 2) AccessoryChange(AccessoryType.HeadBand, belly);   //HeadBand
            if (isSelectedHead != true) isSelectedHead = true;


            ToggleAlphaChange(buttonArmLeg);
            nowArmLegSprite = buttonArmLeg[amrleg].image.sprite;
            bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
            if (isSelectedArmLeg != true) isSelectedArmLeg = true;

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
    private void AccessoryChange(AccessoryType type, int boddy = -1, int head = -1)
    {
        bubbleHedgehogAccPalmtree.SetActive(false);
        bubbleHedgehogAccVolleyball.SetActive(false);
        bubbleHedgehogAccHeadBand.SetActive(false);
        bubbleHedgehogAccCon.SetActive(false);
        switch (type)
        {
            case AccessoryType.VolleyBall:
                bubbleHedgehogAccVolleyball.SetActive(true);
                if (head == 0)
                    bubbleHedgehogAccPalmtree.SetActive(true);
                else if (head == 1)
                    bubbleHedgehogAccHeadBand.SetActive(true);
                else if (head == 2)
                    bubbleHedgehogAccCon.SetActive(true);
                break;
            case AccessoryType.HeadPalmtree:
                if (boddy == 0)
                    bubbleHedgehogAccVolleyball.SetActive(true);
                bubbleHedgehogAccPalmtree.SetActive(true);
                break;
            case AccessoryType.HeadBand:
                if (boddy == 0)
                    bubbleHedgehogAccVolleyball.SetActive(true);
                bubbleHedgehogAccHeadBand.SetActive(true);
                break;
            case AccessoryType.Con:
                if (boddy == 0)
                    bubbleHedgehogAccVolleyball.SetActive(true);
                bubbleHedgehogAccCon.SetActive(true);
                break;
            case AccessoryType.None:
                if (head == 0)
                    bubbleHedgehogAccPalmtree.SetActive(true);
                else if (head == 1)
                    bubbleHedgehogAccHeadBand.SetActive(true);
                else if (head == 2)
                    bubbleHedgehogAccCon.SetActive(true);
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
        //if(bubbleHedgehogOriginStage.activeSelf==false)
        // bubbleHedgehogOriginStage.SetActive(true);
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

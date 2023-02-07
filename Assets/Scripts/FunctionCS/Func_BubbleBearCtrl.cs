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
    [SerializeField] private RawImage bubbleHedgehogBody = null;
    [SerializeField] private RawImage bubbleHedgehogBelly = null;
    [SerializeField] private RawImage bubbleHedgehogHead = null;
    [SerializeField] private RawImage bubbleHedgehogArmLeg = null;
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
        Debug.Log(bubbleHedgehogBody.transform.position);
        beforeHogRect = bubbleHedgehogBody.GetComponent<RectTransform>();
        startXPos = bubbleHedgehogBody.gameObject.transform.position.x + beforeHogRect.rect.position.x + 960;
        startYPos = bubbleHedgehogBody.gameObject.transform.position.y + beforeHogRect.rect.position.y + 540;
        widthValue = (int)beforeHogRect.rect.width;
        heightValue = (int)beforeHogRect.rect.height;
    }
    #region Hedgehog Make button
    public void OnClick_ButtonBody1()
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
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonBelly2()
    {
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[1].image.sprite;
        bubbleHedgehogBelly.texture = nowBellySprite.texture;
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonBelly3()
    {
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[2].image.sprite;
        bubbleHedgehogBelly.texture = nowBellySprite.texture;
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonHead1()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[0].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonHead2()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[1].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonHead3()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[2].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonArmLeg1()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[0].image.sprite;
        bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
        canMoveToAudio = true;
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
    }
    public void OnClick_ButtonArmLeg2()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[1].image.sprite;
        bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
        canMoveToAudio = true;
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
    }
    public void OnClick_ButtonArmLeg3()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[2].image.sprite;
        bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
        canMoveToAudio = true;
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
    }
    public void OnClick_ButtonRandom()
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
        if (isSelectedBelly != true) isSelectedBelly = true;

        head = Random.Range(0, 3);
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[head].image.sprite;
        bubbleHedgehogHead.texture = nowHeadSprite.texture;
        if (isSelectedHead != true) isSelectedHead = true;

        amrleg = Random.Range(0, 3);
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[amrleg].image.sprite;
        bubbleHedgehogArmLeg.texture = nowArmLegSprite.texture;
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
        canMoveToAudio = true;

        buttonBody[body].isOn = true;
        buttonBelly[body].isOn = true;
        buttonHead[body].isOn = true;
        buttonArmLeg[body].isOn = true;
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

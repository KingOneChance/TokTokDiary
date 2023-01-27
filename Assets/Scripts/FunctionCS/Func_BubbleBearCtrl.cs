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

    [Header("===ToggleButtons===")]
    [SerializeField] private Toggle button1 = null;
    [SerializeField] private Toggle button2 = null;
    [SerializeField] private Toggle button3 = null;
    [SerializeField] private Toggle button4 = null;
    [SerializeField] private Toggle button5 = null;

    [SerializeField] private Toggle[] buttonBody = null;
    [SerializeField] private Toggle[] buttonBelly = null;
    [SerializeField] private Toggle[] buttonHead = null;
    [SerializeField] private Toggle[] buttonArmLeg = null;

    [Header("===BubbleBearCanvas===")]
    [SerializeField] private RawImage bubbleBearCanvas = null;

    [Header("===BubbleHedgehogCanvas===")]
    [SerializeField] private RawImage bubbleHedgehogThorn = null;
    [SerializeField] private RawImage bubbleHedgehogBelly = null;
    [SerializeField] private RawImage bubbleHedgehogHead = null;
    [SerializeField] private RawImage bubbleHedgehogLArm = null;
    [SerializeField] private RawImage bubbleHedgehogRArm = null;
    [SerializeField] private RawImage bubbleHedgehogLLeg = null;
    [SerializeField] private RawImage bubbleHedgehogRLeg = null;

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

    private void Start()
    {
        button1.image.sprite = testBear1;
        button2.image.sprite = testBear2;
        button3.image.sprite = testBear3;
        button4.image.sprite = testBear4;
        button5.image.sprite = testBear5;
    }

    public void OnClick_Button1()
    {
        AlphaChange(1);
        bubbleBearCanvas.texture = testBear1.texture;
        nowSprite = testBear1;
    }
    public void OnClick_Button2()
    {
        AlphaChange(2);
        bubbleBearCanvas.texture = testBear2.texture;
        nowSprite = testBear2;
    }
    public void OnClick_Button3()
    {
        AlphaChange(3);
        bubbleBearCanvas.texture = testBear3.texture;
        nowSprite = testBear3;
    }
    public void OnClick_Button4()
    {
        AlphaChange(4);
        bubbleBearCanvas.texture = testBear4.texture;
        nowSprite = testBear4;
    }
    public void OnClick_Button5()
    {
        AlphaChange(5);
        bubbleBearCanvas.texture = testBear5.texture;
        nowSprite = testBear5;
    }

    #region Hedgehog Make button
    public void OnClick_ButtonBody1()
    {
        ToggleAlphaChange(buttonBody);
        nowBodySprite = buttonBody[0].image.sprite;
        if (isSelectedBody != true) isSelectedBody = true;
    }
    public void OnClick_ButtonBody2()
    {
        ToggleAlphaChange(buttonBody);
        nowBodySprite = buttonBody[1].image.sprite;
        if (isSelectedBody != true) isSelectedBody = true;
    }
    public void OnClick_ButtonBody3()
    {
        ToggleAlphaChange(buttonBody);
        nowBodySprite = buttonBody[2].image.sprite;
        if (isSelectedBody != true) isSelectedBody = true;
    }
    public void OnClick_ButtonBelly1()
    {
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[0].image.sprite;
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonBelly2()
    {
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[1].image.sprite;
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonBelly3()
    {
        ToggleAlphaChange(buttonBelly);
        nowBellySprite = buttonBelly[2].image.sprite;
        if (isSelectedBelly != true) isSelectedBelly = true;
    }
    public void OnClick_ButtonHead1()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[0].image.sprite;
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonHead2()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[1].image.sprite;
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonHead3()
    {
        ToggleAlphaChange(buttonHead);
        nowHeadSprite = buttonHead[2].image.sprite;
        if (isSelectedHead != true) isSelectedHead = true;
    }
    public void OnClick_ButtonArmLeg1()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[0].image.sprite;
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
    }
    public void OnClick_ButtonArmLeg2()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[1].image.sprite;
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
    }
    public void OnClick_ButtonArmLeg3()
    {
        ToggleAlphaChange(buttonArmLeg);
        nowArmLegSprite = buttonArmLeg[2].image.sprite;
        if (isSelectedArmLeg != true) isSelectedArmLeg = true;
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
    private void AlphaChange(int buttonNum)
    {
        button1.image.color = new Color(255, 255, 255, 1f);
        button2.image.color = new Color(255, 255, 255, 1f);
        button3.image.color = new Color(255, 255, 255, 1f);
        button4.image.color = new Color(255, 255, 255, 1f);
        button5.image.color = new Color(255, 255, 255, 1f);
        switch (buttonNum)
        {
            case 1:
                button1.image.color = new Color(255, 255, 255, 0.3f);
                break;
            case 2:
                button2.image.color = new Color(255, 255, 255, 0.3f);
                break;
            case 3:
                button3.image.color = new Color(255, 255, 255, 0.3f);
                break;
            case 4:
                button4.image.color = new Color(255, 255, 255, 0.3f);
                break;
            case 5:
                button5.image.color = new Color(255, 255, 255, 0.3f);
                break;
            default:
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
}

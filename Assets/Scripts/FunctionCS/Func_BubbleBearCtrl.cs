using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_BubbleBearCtrl : MonoBehaviour
{
    [Header("BubbleStickers")]
    [SerializeField] private Sprite testBear1 = null;
    [SerializeField] private Sprite testBear2 = null;
    [SerializeField] private Sprite testBear3 = null;
    [SerializeField] private Sprite testBear4 = null;
    [SerializeField] private Sprite testBear5 = null;

    [Header("ToggleButtons")]
    [SerializeField] private Toggle button1 = null;
    [SerializeField] private Toggle button2 = null;
    [SerializeField] private Toggle button3 = null;
    [SerializeField] private Toggle button4 = null;
    [SerializeField] private Toggle button5 = null;

    [Header("BubbleBearCanvas")]
    [SerializeField] private RawImage bubbleBearCanvas = null;

    public Sprite nowSprite { get; private set; }

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
}

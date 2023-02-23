using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;
using UnityEngine.UI;

public class Func_FreeStickerDraw : Func_Draw
{
    [SerializeField] private Transform LinesParent = null;
    [SerializeField] public Button widthSmallBtn;
    [SerializeField] public Button widthMediumBtn;
    [SerializeField] public Button widthLargeBtn;
    Func_Draw func_draw;
   
    
    public float penWidth;
    

    protected new void Start()
    {
        base.currentPenWidth = 0.5f;
        widthSmallBtn.onClick.AddListener(OnClick_WidthSmall);
        widthMediumBtn.onClick.AddListener(OnClick_WidthMedium);
        widthLargeBtn.onClick.AddListener(OnClick_WidthLarge);
    }

    public new void Update()
    {
        if (onObject == false)
        {
            if (CheckArea() == true)
            {
                Draw();
            }
            else
            {
                internalClick = false;
            }
        }
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && (internalClick == false || onObject == true)))
        {
            obj.transform.SetParent(LinesParent);
        }
    }

    public void SetPenWidth(float width)
    {
        base.fixedPenWidth = width;
    }

    public void OnClick_WidthLarge()
    {
        SetPenWidth(0.3f);
    }

    public void OnClick_WidthMedium()
    {
        SetPenWidth(0.2f);
    }

    public void OnClick_WidthSmall()
    {
        SetPenWidth(0.1f);
    }
}
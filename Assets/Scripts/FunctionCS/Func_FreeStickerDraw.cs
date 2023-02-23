using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;
using UnityEngine.UI;

public class Func_FreeStickerDraw : Func_Draw
{
    [SerializeField] private Transform LinesParent = null;
    //[SerializeField] private float currentPenWidth_Free = 0f;
    //Func_Draw func_draw;
    public float penWidth;



    protected new void Start()
    {
        base.currentPenWidth = 0.5f;
        GetComponent<Button>().onClick.AddListener(OnClick);
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
        base.currentPenWidth = width;
    }

    public void OnClick()
    {
        SetPenWidth(penWidth);
    }
    public void OnClick_WidthLarge()
    {
        base.currentPenWidth = 0.3f;
        
    
    }

    public void OnClick_WidthMedium()
    {
        base.currentPenWidth = 0.2f;
    
    }

    public void OnClick_WidthSmall()
    {
        base.currentPenWidth = 0.1f;
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;
using UnityEngine.UI;

public class Func_FreeStickerDraw : Func_Draw
{
    [SerializeField] private Transform LinesParent = null;
    [SerializeField] private float currentPenWidth_Free = 0f;
    [SerializeField] private LineRenderer lineRenderer;

    protected new void Start()
    {
        currentPenWidth_Free = 0.1f;
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

    public void OnClick_WidthLarge()
    {
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;
    }

    public void OnClick_WidthMedium()
    {
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
    }

    public void OnClick_WidthSmall()
    {
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }
}

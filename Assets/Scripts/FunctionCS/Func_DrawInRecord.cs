using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;
public class Func_DrawInRecord : Func_Draw
{
    [SerializeField] private Transform LinesParent = null;
    protected new void Update()
    {
        if (onObject == false)
        {
            if (CheckArea() == true)
            {
                Draw();
            }
            else internalClick = false;
        }
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && (internalClick == false || onObject == true)))
        {
            obj.transform.SetParent(LinesParent);
        }
    }
}

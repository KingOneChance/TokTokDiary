using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Func_SwellUpDrag : Func_DragAndDrop
{
    [SerializeField] GameObject[] SwellUpPanels = null;
    [SerializeField] Func_SwellUpDrag[] func_SwellUpDrags = null;
    private bool isDragDone = false;
    public bool IsDragDone { get { return isDragDone; } private set { } }

    private new void OnEnable()
    {
        isDragDone = false;
        SwellUpPanels[0].SetActive(true);
        SwellUpPanels[1].SetActive(false);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (Vector3.Distance(myDestinationPos.position, myRect.position) < 100f)
        {
            isDragDone = true;
            CheckIsDragDone();
        }
    }

    private void CheckIsDragDone()
    {
        for(int i = 0; i < func_SwellUpDrags.Length; ++i)
        {
            if (func_SwellUpDrags[i].IsDragDone == false)
                return;
        }
        SwellUpPanels[0].SetActive(false);
        SwellUpPanels[1].SetActive(true);
    }
}

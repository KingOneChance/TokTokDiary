using UnityEngine;
using UnityEngine.EventSystems;

public class Func_DragAndDrop : Func_BasicDrag
{
    [SerializeField] protected RectTransform myInitRect = null;
    [SerializeField] protected RectTransform myDestinationPos = null;

    protected bool isDropDone = false;

    protected void OnEnable()
    {
        Start();
        isDropDone = false;
        myRect.position = myInitRect.position;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (isDropDone == true) return;
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (isDropDone == true) return;
        base.OnEndDrag(eventData);
        if (Vector3.Distance(myDestinationPos.position, myRect.position) < 100f)
        {
            myRect.position = myDestinationPos.position;
            isDropDone = true;
        }
        else
        {
            myRect.position = myInitRect.position;
        }
    }
}

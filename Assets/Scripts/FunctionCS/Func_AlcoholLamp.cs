using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Func_AlcoholLamp : Func_BasicDrag
{
    [SerializeField] private RectTransform myDestinationPos = null;
    [SerializeField] private RawImage alcoholLampPos = null;
    [SerializeField] private Manager_BubbleSticker bsManager = null;
    private Vector3 myInitpos = Vector3.zero;
    private bool isAlcoholampDone = false;

    private void OnEnable()
    {
        isAlcoholampDone = false;
    }

    protected new void Start()
    {
        base.Start();
        myInitpos = myRect.position;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (isAlcoholampDone == true) return;
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (Vector3.Distance(myDestinationPos.position, myRect.position) < 100f)
        {
            bsManager.ActiveColortBucket(true);
            myRect.position = myDestinationPos.position;
            isAlcoholampDone = true;
        }
        else
        {
            myRect.position = myInitpos;
        }
    }

    private IEnumerator Twinkle()
    {
        yield return new WaitForFixedUpdate();
        float alphaValue = 0;
        
        if(alphaValue < 0.5f)
        {
            alphaValue -= Time.fixedDeltaTime;
        }
        alcoholLampPos.color = new Color(1, 1, 1, alphaValue);
    }
}

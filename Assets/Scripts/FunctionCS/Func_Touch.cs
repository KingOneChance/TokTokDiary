using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Func_Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("��ġ����");
    }

    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("��ġ��");
    }
}

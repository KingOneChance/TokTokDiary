using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_Tilt : MonoBehaviour
{
    [SerializeField] RectTransform tiltObj = null;
    private Vector3 angle = new Vector3(0, 0, 1);

    public void StartTilt(bool forward) => StartCoroutine(Tilt(forward));

    private IEnumerator Tilt(bool forward)
    {
        Debug.Log("±â¿ï¿©~~");
        int dir = forward == true ? 1 : -1;
        float stopCondition = (dir == 1) ? 120f : 0f;
        
        while (true)
        {
            Debug.Log(Mathf.Abs(stopCondition - tiltObj.localEulerAngles.z));
            if(Mathf.Abs(stopCondition - tiltObj.localEulerAngles.z) < 1f)
                yield break;
            tiltObj.localEulerAngles += dir * angle * Time.deltaTime * 120f;
            yield return null;
        }
    }
}

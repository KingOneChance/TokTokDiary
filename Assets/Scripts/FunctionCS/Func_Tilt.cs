using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_Tilt : MonoBehaviour
{
    [SerializeField] RectTransform tiltObj = null;
    private Vector3 angle = new Vector3(0, 0, 1);
    private bool isTouched = false;

    private void Update()
    {
        Manager_UserInput.UpdateTouch(); // It will remove before merge
        if (Input.touchCount != 1) return;
        if(Manager_UserInput.curInputState == UserInputState.TouchStationary && isTouched == false)
        {
            isTouched = true;
            StartTilt();
        }
        if(Manager_UserInput.curInputState == UserInputState.TouchEnded)
        {
            isTouched = false;
            StopTilt();
        }
    }

    public void StartTilt()
    {
        StopAllCoroutines();
        StartCoroutine(CO_TiltStart());
    }
    public void StopTilt() 
    {
        StopAllCoroutines();
        StartCoroutine(CO_TiltStop());
    }
   

    private IEnumerator CO_TiltStart()
    {
        while (true)
        {
            if(tiltObj.localEulerAngles.z > 119f || isTouched == false)
            {
                tiltObj.localEulerAngles = new Vector3(0, 0, 120);
                Debug.Log("Start Fill");
                yield break;
            }
            tiltObj.localEulerAngles += angle * Time.deltaTime * 120f;
            yield return null;
        }
    }

    private IEnumerator CO_TiltStop()
    {
        while (true)
        {
            if (tiltObj.localEulerAngles.z < 1f)
            {
                tiltObj.localEulerAngles = Vector3.zero;
                yield break;
            }
            tiltObj.localEulerAngles += -1 * angle * Time.deltaTime * 120f;
            yield return null;
        }
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Func_Tilt : MonoBehaviour
{
    [SerializeField] private RectTransform tiltObj = null;
    [SerializeField] private Slider progressBar = null;
    [SerializeField] private RawImage myImage = null;

    private Color myInitColor = new Vector4(255,255,255,255);
    private Vector3 angle = new Vector3(0, 0, 1);

    private Vector3 FirstPoint;
    private Vector3 SecondPoint;

    [SerializeField] private bool isSwipeDown = false;
    [SerializeField] private bool isProgress = false;

    private void Start()
    {
        isSwipeDown = false;
        isProgress = false;
    }
    private void Update()
    {
        //Manager_UserInput.UpdateTouch();
        if (Manager_UserInput.touchCount > 0)
        {
            if (Manager_UserInput.touches[0].phase == TouchPhase.Began)
            {
                FirstPoint = Manager_UserInput.touches[0].position;
            }
            if (Manager_UserInput.touches[0].phase == TouchPhase.Moved)
            {
                SecondPoint = Manager_UserInput.touches[0].position;
                if (Mathf.Abs(FirstPoint.y - SecondPoint.y) > 150f)
                {
                    if (FirstPoint.y - SecondPoint.y > 0)
                        isSwipeDown = true;
                    else
                        isSwipeDown = false;
                }
            }
            //if(Manager_UserInput.touches[0].phase == TouchPhase.Ended)
            //{
            //    isProgress = false;
            //    StopCoroutine(CO_ProgressBar());
            //    FirstPoint = Vector3.zero;
            //    SecondPoint = Vector3.zero;
            //    return;
            //}
        }

        if (isSwipeDown == true)
        {
            tiltObj.localEulerAngles += angle * Time.deltaTime * 100f;
            if (tiltObj.localEulerAngles.z > 120f)
            {
                tiltObj.localEulerAngles = new Vector3(0, 0, 120);
                if (isProgress == false)
                    StartCoroutine(CO_ProgressBar());
            }
        }
        else
        {
            isProgress = false;
            tiltObj.localEulerAngles += -1 * angle * Time.deltaTime * 100f;
            if (tiltObj.rotation.z < 0f)
            {
                tiltObj.localEulerAngles = Vector3.zero;
            }
        }
    }

    private IEnumerator CO_ProgressBar()
    {
        isProgress = true;
        while (true)
        {
            if (isProgress == false)
            {
                isProgress = false;
                yield break;
            }
            if(progressBar.value >= 1f)
            {
                isProgress = false;
                progressBar.value = 0f;
                yield break;

            }
            progressBar.value += 0.001f;
            yield return null;
        }
    }

    public void ResetBucket()
    {
        myImage.color = myInitColor;
    }
}

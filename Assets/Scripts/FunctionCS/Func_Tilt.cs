using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Func_Tilt : MonoBehaviour
{
    [SerializeField] private RectTransform tiltObj = null;
    [SerializeField] private Slider progressBar = null;
    [SerializeField] private RawImage myImage = null;
    [SerializeField] private Button[] ColorButtons = null;
    [SerializeField] private Button NextButton = null;
    [SerializeField] private Button SkipButton = null;

    private Color myInitColor = new Vector4(255, 255, 255, 255);
    private Vector3 angle = new Vector3(0, 0, 1);
    private Vector3 FirstPoint = Vector3.zero;
    private Vector3 SecondPoint = Vector3.zero;

    private bool isSwipeDown = false;
    private bool isProgress = false;

    private void Start()
    {
        SkipButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        isProgress = false;
        isSwipeDown = false;
    }

    private void Update()
    {
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
        }

        if (isSwipeDown == true)
        {
            TiltDown();
            for (int i = 0; i < ColorButtons.Length; ++i)
            {
                ColorButtons[i].interactable = false;
            }
        }
        else
        {
            TiltUp();
            for (int i = 0; i < ColorButtons.Length; ++i)
            {
                ColorButtons[i].interactable = true;
            }
        }
    }

    private IEnumerator CO_ProgressBar()
    {
        SkipButton.gameObject.SetActive(true);
        isProgress = true;
        for(int i = 0; i < ColorButtons.Length; ++i)
        {
            ColorButtons[i].interactable = false;
        }
        while (true)
        {
            if (isProgress == false)
            {
                SkipButton.gameObject.SetActive(false);
                for (int i = 0; i < ColorButtons.Length; ++i)
                {
                    ColorButtons[i].interactable = true;
                }
                yield break;
            }
            if(progressBar.value >= 1f)
            {
                SkipButton.gameObject.SetActive(false);
                isProgress = false;
                isSwipeDown = false;
                TiltUp();
                NextButton.interactable = true;
                progressBar.value = 0f;
                for (int i = 0; i < ColorButtons.Length; ++i)
                {
                    ColorButtons[i].interactable = true;
                }
                yield break;

            }
            progressBar.value += 0.01f;
            yield return null;
        }
    }

    private void TiltDown()
    {
        tiltObj.localEulerAngles += angle * Time.deltaTime * 100f;
        if (tiltObj.localEulerAngles.z > 120f)
        {
            tiltObj.localEulerAngles = new Vector3(0, 0, 120);
            if (isProgress == false)
                StartCoroutine(CO_ProgressBar());
        }
    }

    private void TiltUp()
    {
        isProgress = false;
        tiltObj.localEulerAngles += -1 * angle * Time.deltaTime * 100f;
        if (tiltObj.rotation.z < 0f)
        {
            tiltObj.localEulerAngles = Vector3.zero;
        }
    }

    public void ResetBucket()
    {
        myImage.color = myInitColor;
    }

    public void OnClick_SkipButton() => progressBar.value = 1f;
    
}

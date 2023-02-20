using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Func_Tilt : MonoBehaviour
{
    [SerializeField] private Toggle[] ColorBucketToggles = null;
    [SerializeField] private RawImage beakerSolutionImg = null;
    [SerializeField] private RawImage[] colorBeakerImg = null;
    [SerializeField] private RectTransform beakerSolutionImgPos = null;
    [SerializeField] private RectTransform beakerSolutionImgInitPos = null;
    [SerializeField] private Button NextButton = null;

    private RectTransform tiltObj = null;
    private Vector3 angle = new Vector3(0, 0, 1);
    private Vector3 FirstPoint = Vector3.zero;
    private Vector3 SecondPoint = Vector3.zero;

    private bool isSwipeDown = false;
    private bool isSolutionFall = false;

    private void Start()
    {
        tiltObj = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        isSwipeDown = false;
        beakerSolutionImg.gameObject.SetActive(false);
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
            for (int i = 0; i < ColorBucketToggles.Length; ++i)
            {
                ColorBucketToggles[i].interactable = false;
            }
        }
        else
        {
            TiltUp();
            for (int i = 0; i < ColorBucketToggles.Length; ++i)
            {
                ColorBucketToggles[i].interactable = true;
            }
        }
    }

    private void TiltDown()
    {
        tiltObj.localEulerAngles += angle * Time.deltaTime * 100f;

        if (tiltObj.localEulerAngles.z > 120f)
        {
            tiltObj.localEulerAngles = new Vector3(0, 0, 120);
            beakerSolutionImg.gameObject.SetActive(true);
            if (isSolutionFall == false)
            {
                StartCoroutine(CO_FallSolution()); 
            }
        }
    }

    private void TiltUp()
    {
        beakerSolutionImg.gameObject.SetActive(false);
        StopCoroutine(CO_FallSolution());
        Manager_Main.Instance.GetAudio().StopPlaySound(gameObject);
        tiltObj.localEulerAngles += -1 * angle * Time.deltaTime * 100f;
        if (tiltObj.rotation.z < 0f)
        {
            tiltObj.localEulerAngles = Vector3.zero;
        }
    }

    private IEnumerator CO_FallSolution()
    {
        isSolutionFall = true;
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            Manager_Main.Instance.GetAudio().PlaySound("PongPong", SoundType.BubbleSticker, gameObject, true, true);
            while (true)
            {
                if(count == 25)
                {
                    count = 0;
                    beakerSolutionImgPos.position = beakerSolutionImgInitPos.position;
                    
                    break;
                }
                beakerSolutionImgPos.position += new Vector3(0,-10f,0);
                count++;
                yield return new WaitForFixedUpdate();
            } 
        }
        colorBeakerImg[0].gameObject.SetActive(false);
        CheckColor(gameObject.name);
        isSwipeDown = false;
        isSolutionFall = false;
        NextButton.gameObject.SetActive(true);
    }

    private void CheckColor(string name)
    {
        switch (name)
        {
            case "GreenBeaker":
                colorBeakerImg[1].gameObject.SetActive(true);
                colorBeakerImg[2].gameObject.SetActive(false);
                colorBeakerImg[3].gameObject.SetActive(false);
                break;

            case "PinkBeaker":
                colorBeakerImg[2].gameObject.SetActive(true);
                colorBeakerImg[1].gameObject.SetActive(false);
                colorBeakerImg[3].gameObject.SetActive(false);
                break;

            case "BlueBeaker":
                colorBeakerImg[3].gameObject.SetActive(true);
                colorBeakerImg[1].gameObject.SetActive(false);
                colorBeakerImg[2].gameObject.SetActive(false);
                break;
        }
    }
}

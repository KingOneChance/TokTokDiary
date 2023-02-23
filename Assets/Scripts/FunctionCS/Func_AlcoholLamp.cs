using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Func_AlcoholLamp : Func_DragAndDrop
{
    [SerializeField] private Image[] alcoholLampSolutions = null;
    [SerializeField] private RawImage[] beakerImg = null;
    [SerializeField] private RawImage[] colorBeakerImg = null;
    [SerializeField] private RawImage alcoholLampPosImg = null;
    [SerializeField] private RawImage paintCase = null;
    [SerializeField] private Button paintCaseButton = null;
    [SerializeField] private Manager_BubbleSticker bsManager = null;
    [SerializeField] private GameObject alcoholLampDragGuide = null;
    [SerializeField] private GameObject[] colorBeakerPoppingGuideLine = null;

    private bool isSolutionFill = false;

    private new void OnEnable()
    {
        base.OnEnable();
        isSolutionFill = false;
        alcoholLampDragGuide.SetActive(true);
        alcoholLampPosImg.color = Vector4.one;
        bsManager.ActiveColorBucket(false);
        bsManager.InitColorBucket();
        paintCaseButton.interactable = false;
        StartCoroutine(CO_BlinkBlink(alcoholLampPosImg));
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (isSolutionFill == true) return;
        base.OnEndDrag(eventData);
        if (Vector3.Distance(myDestinationPos.position, myRect.position) < 100f)
        {
            Manager_Main.Instance.GetAudio().PlaySound("ComeBack", SoundType.Touch, gameObject, false, true);
            StopCoroutine(CO_BlinkBlink(alcoholLampPosImg));
            StartCoroutine(CO_FillSolution());
            alcoholLampPosImg.color = Vector4.one;
            alcoholLampDragGuide.SetActive(false);
        }
    }

    private IEnumerator CO_BlinkBlink(RawImage blinkImage)
    {
        float fadeAmount = 0;
        while (true)
        {
            if (isDropDone == true)
            {
                blinkImage.color = Vector4.one;
                yield break;
            }
            while (fadeAmount > 0.5f)
            {
                fadeAmount -= 0.1f;
                yield return new WaitForFixedUpdate();
                blinkImage.color = new Color(1, 1, 1, fadeAmount);
            }
            while (fadeAmount < 1.0f)
            {
                fadeAmount += 0.1f;
                yield return new WaitForFixedUpdate();
                blinkImage.color = new Color(1, 1, 1, fadeAmount);
            }
        }
    }

    private IEnumerator CO_BlinkBlink(RawImage[] blinkImage)
    {
        float fadeAmount = 0;
        int fadeCount = 0;
        while (true)
        {
            while (fadeAmount > 0.5f)
            {
                fadeAmount -= 0.05f;
                yield return new WaitForFixedUpdate();
                for (int i = 0; i < blinkImage.Length; i++)
                {
                    blinkImage[i].color = new Color(1, 1, 1, fadeAmount);
                }
            }
            while (fadeAmount < 1.0f)
            {
                fadeAmount += 0.05f;
                yield return new WaitForFixedUpdate();
                for (int i = 0; i < blinkImage.Length; i++)
                {
                    blinkImage[i].color = new Color(1, 1, 1, fadeAmount);
                }
            }
            if (++fadeCount == 3)
            {
                OnClick_PaintCase();
                yield break;
            }
        }
    }

    private IEnumerator CO_FillSolution()
    {
        Manager_Main.Instance.GetAudio().PlaySound("PongPong", SoundType.BubbleSticker, bsManager.gameObject, true, true);
        while (true)
        {
            if (alcoholLampSolutions[0].fillAmount >= 1f)
            {
                break;
            }
            alcoholLampSolutions[0].fillAmount += 0.01f;
            yield return new WaitForFixedUpdate();
        }

        while (true)
        {
            if (alcoholLampSolutions[1].fillAmount >= 1f)
            {
                break;
            }
            alcoholLampSolutions[1].fillAmount += 0.02f;
            yield return new WaitForFixedUpdate();
        }
        Manager_Main.Instance.GetAudio().StopPlaySound(bsManager.gameObject);
        int repeatCount = 0;
        Manager_Main.Instance.GetAudio().PlaySound("BubbleDrop", SoundType.BubbleSticker, gameObject, true, true);
        while (true)
        {
            if (alcoholLampSolutions[2].fillAmount >= 1f)
            {
                alcoholLampSolutions[2].fillAmount = 0f;
                repeatCount++;
                break;
            }
            alcoholLampSolutions[2].fillAmount += 0.03f;
            yield return new WaitForFixedUpdate();
        }
        Manager_Main.Instance.GetAudio().StopPlaySound(gameObject);
        alcoholLampSolutions[0].fillAmount = 0f;
        alcoholLampSolutions[1].fillAmount = 0f;
        beakerImg[0].gameObject.SetActive(false);
        beakerImg[1].gameObject.SetActive(true);
        bsManager.ActiveColorBucket(true);
        paintCaseButton.interactable = true;
        for (int i = 0; i < colorBeakerPoppingGuideLine.Length; i++)
        {
            colorBeakerPoppingGuideLine[i].gameObject.SetActive(true);
        }
        isSolutionFill = true;
        StartCoroutine(CO_BlinkBlink(colorBeakerImg));
        yield break;
    }

    public void OnClick_PaintCase()
    {
        paintCase.raycastTarget = false;
        StopCoroutine(CO_BlinkBlink(colorBeakerImg));
    }
}

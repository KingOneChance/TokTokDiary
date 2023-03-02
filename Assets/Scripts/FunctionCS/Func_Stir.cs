using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Func_Stir : MonoBehaviour
{
    [SerializeField] private Toggle[] beakersMixGreen = null;
    [SerializeField] private Toggle[] beakersMixPink = null;
    [SerializeField] private Toggle[] beakersMixBlue = null;
    [SerializeField] private RectTransform myRect = null;
    [SerializeField] private RectTransform stickInitPos = null;
    [SerializeField] private Button stickButton = null;
    [SerializeField] private Button NextButton = null;
    [SerializeField] private Button SkipButton = null;
    [SerializeField] private Manager_BubbleSticker bsManager = null;
    [SerializeField] private GameObject bubbleStickGuide = null;

    [SerializeField] private GameObject explain1 = null;
    [SerializeField] private GameObject explain2 = null;
    [SerializeField] private GameObject explain3 = null;

    private float radius = 0;
    private float deg = 0;
    private float stirSpeed = 0;
    private int stirCount = 0;

    private void Start()
    {
        radius = 30f;
        stirSpeed = 500f;
    }

    private void OnEnable()
    {
        explain1.SetActive(true);
        explain2.SetActive(false);
        explain3.SetActive(false);
        bubbleStickGuide.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            beakersMixGreen[i].gameObject.SetActive(false);
            beakersMixPink[i].gameObject.SetActive(false);
            beakersMixBlue[i].gameObject.SetActive(false);
        }
        switch (bsManager.ColorType)
        {
            case 1:
                beakersMixGreen[0].gameObject.SetActive(true);
                break;

            case 2:
                beakersMixPink[0].gameObject.SetActive(true);
                break;

            case 3:
                beakersMixBlue[0].gameObject.SetActive(true);
                break;
        }
        stickButton.enabled = true;
        myRect.position = stickInitPos.position;
        SkipButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        stirCount = 0;
    }

    private IEnumerator Stir()
    {
        Manager_Main.Instance.GetAudio().PlaySound("Rolling", SoundType.BubbleSticker, gameObject, false, true);
        stickButton.enabled = false;
        float rad = 0f;
        float xCoord = 0f;
        float yCoord = 0f;
        while (true)
        {
            deg -= Time.deltaTime * stirSpeed;
            if (deg > -360)
            {
                rad = Mathf.Deg2Rad * (deg);
                xCoord = radius * Mathf.Cos(rad);
                yCoord = radius * Mathf.Sin(rad);
                myRect.anchoredPosition = myRect.anchoredPosition + new Vector2(-1 * xCoord, yCoord);
            }
            else
            {
                deg = 0;
                stickButton.enabled = true;
                stirCount++;
                if(stirCount == 1)
                {
                    explain1.SetActive(false);
                    explain2.SetActive(true);
                    explain3.SetActive(false);
                }
                else if(stirCount == 2)
                {
                    explain1.SetActive(false);
                    explain2.SetActive(true);
                    explain3.SetActive(true);
                }
                MixProcess(bsManager.ColorType, stirCount);
                if (stirCount == 3)
                {
                    Manager_Main.Instance.GetAudio().PlaySound("RollingGood", SoundType.BubbleSticker, gameObject, false, true);
                    SkipButton.gameObject.SetActive(false);
                    stickButton.enabled = false;
                    Invoke(nameof(DelayProcess), 2f);
                }
                myRect.position = stickInitPos.position;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void MixProcess(int type, int idx)
    {
        switch (type)
        {
            case 1:
                beakersMixGreen[idx].isOn = true;
                for(int i = 0; i < beakersMixGreen.Length; ++i)
                {
                    beakersMixGreen[i].gameObject.SetActive(beakersMixGreen[i].isOn);
                }
                break;

            case 2:
                beakersMixPink[idx].isOn = true;
                for (int i = 0; i < beakersMixPink.Length; ++i)
                {
                    beakersMixPink[i].gameObject.SetActive(beakersMixPink[i].isOn);
                }
                break;

            case 3:
                beakersMixBlue[idx].isOn = true;
                for (int i = 0; i < beakersMixBlue.Length; ++i)
                {
                    beakersMixBlue[i].gameObject.SetActive(beakersMixBlue[i].isOn);
                }
                break;
        }
    }

    public void OnClick_BubbleStick() => StartCoroutine(Stir());

    public void OnClick_SkipButton()
    {
        Manager_Main.Instance.GetAudio().PlaySound("RollingGood", SoundType.BubbleSticker, gameObject, false, true);
        stickButton.enabled = false;
        Invoke(nameof(DelayProcess), 2f);
    }

    private void DelayProcess()
    {
        Manager_Main.Instance.GetAudio().PlaySound("NextButton", SoundType.Common, gameObject, false, true);
        MixProcess(bsManager.ColorType, 3);
        NextButton.gameObject.SetActive(true);
    }
}

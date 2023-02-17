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
                MixProcess(bsManager.ColorType, stirCount);
                if (stirCount == 3)
                {
                    NextButton.gameObject.SetActive(true);
                    SkipButton.gameObject.SetActive(false);
                    stickButton.enabled = false;
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
        NextButton.gameObject.SetActive(true);

        stickButton.enabled = false;
    }
}

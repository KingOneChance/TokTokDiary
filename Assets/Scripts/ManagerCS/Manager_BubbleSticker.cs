using UnityEngine;
using UnityEngine.UI;

public class Manager_BubbleSticker : MonoBehaviour
{
    #region SerializeField
    [SerializeField] GameObject[] Panels = null;

    [SerializeField] private RectTransform[] bucketPos = null;

    [SerializeField] private RectTransform myPos = null;

    [SerializeField] private RawImage[] ColorBuckets = null;

    [SerializeField] private Button[] ColorButtons = null;

    [SerializeField] private Button BackButton = null;

    [SerializeField] private Button NextButton = null;

    [SerializeField] private RawImage myColor = null;

    [SerializeField] private Func_Tilt myTilt = null;

    #endregion

    private bool isDefaultBottleSelected = false;
    private int PanelIdx = 0;

    public RawImage BubbleSicker = null;
    public Color BubbleStickerColor = Vector4.zero;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        PanelIdx = 0;
        BackButton.interactable = false;
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        Panels[0].SetActive(true);
        myTilt.enabled = false;
        myPos.transform.position = bucketPos[0].transform.position;
        for (int i = 0; i < ColorButtons.Length; i++)
        {
            ColorButtons[i].interactable = false;
        }
    }

    public void OnClick_NextBtn()
    {
        BackButton.interactable = true;
        if (PanelIdx + 1 == Panels.Length - 1) NextButton.interactable = false;
        Panels[PanelIdx + 1].SetActive(true);
        Panels[PanelIdx].SetActive(false);
        PanelIdx++;
    }

    public void OnClick_BackBtn()
    {
        NextButton.interactable = true;
        if (PanelIdx - 1 == 0) BackButton.interactable = false;
        Panels[PanelIdx - 1].SetActive(true);
        Panels[PanelIdx].SetActive(false);
        PanelIdx--;
    }

    public void OnClick_DefaultBottle()
    {
        isDefaultBottleSelected = !isDefaultBottleSelected;
        if (isDefaultBottleSelected == true)
        {
            myPos.transform.position = bucketPos[1].transform.position;
            myTilt.enabled = true;
            for (int i = 0; i < ColorButtons.Length; i++)
            {
                ColorButtons[i].interactable = true;
            }
        }
        else
        {
            myPos.transform.position = bucketPos[0].transform.position;
            myTilt.enabled = false;
            myTilt.ResetBucket();
            myPos.transform.localEulerAngles = Vector3.zero;
            for (int i = 0; i < ColorButtons.Length; i++)
            {
                ColorButtons[i].interactable = false;
            }
        }
    }

    public void OnClick_SelectColor(string color)
    {
        switch (color)
        {
            case "Yellow":
                myColor.color = ColorBuckets[0].color;
                break;

            case "Orange":
                myColor.color = ColorBuckets[1].color;
                break;

            case "Purple":
                myColor.color = ColorBuckets[2].color;
                break;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Manager_BubbleSticker : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private GameObject[] Panels = null;
    [SerializeField] private RectTransform[] bucketPos = null;
    [SerializeField] private RectTransform myPos = null;
    [SerializeField] private RawImage[] ColorBuckets = null;
    [SerializeField] private RawImage[] StickerDesignArr = null;
    [SerializeField] private Button[] ColorButtons = null;
    [SerializeField] private Button BackButton = null;
    [SerializeField] private Button NextButton = null;
    [SerializeField] private RawImage defaultBucketColor = null;
    [SerializeField] private Func_Tilt myTilt = null;
    [SerializeField] private Func_Stir myStir = null;
    [SerializeField] private Func_SwellUp mySwellUp = null;
    [SerializeField] private RawImage BubbleSicker = null;
    [SerializeField] private Func_SwipeMove StickerDesign = null;
    #endregion

    private bool isDefaultBottleSelected = false;
    [SerializeField] private int PanelIdx = 0;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        PanelIdx = 0;
        BackButton.gameObject.SetActive(false);
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
        CheckCurPanel();
        BackButton.gameObject.SetActive(true);
        if (PanelIdx + 1 == Panels.Length - 1) NextButton.gameObject.SetActive(false);
        Panels[PanelIdx + 1].SetActive(true);
        Panels[PanelIdx].SetActive(false);
        PanelIdx++;
        NextButton.interactable = false;
    }

    public void OnClick_BackBtn()
    {
        if (PanelIdx == 1) NextButton.interactable = true;
        NextButton.gameObject.SetActive(true);
        if (PanelIdx - 1 == 0) BackButton.gameObject.SetActive(false);
        Panels[PanelIdx - 1].SetActive(true);
        Panels[PanelIdx].SetActive(false);
        PanelIdx--;
        if (PanelIdx == 0) NextButton.interactable = true;
        else NextButton.interactable = false;
    }

    private void CheckCurPanel()
    {
        switch (PanelIdx)
        {
            case 0:
                BubbleSicker.texture = StickerDesignArr[StickerDesign.CurrentSticker].texture;
                break;

            case 1:
                BubbleSicker.color = defaultBucketColor.color;
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;
        }
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
                defaultBucketColor.color = ColorBuckets[0].color;
                break;

            case "Orange":
                defaultBucketColor.color = ColorBuckets[1].color;
                break;

            case "Purple":
                defaultBucketColor.color = ColorBuckets[2].color;
                break;
        }
    }
}

using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Manager_BubbleSticker : Func_SaveSticker
{
    private BubbleStickerType bubbleStickerType;
    #region SerializeField
    [Header("========== 기본 UI ==========")]
    [SerializeField] private Button BackButton = null;
    [SerializeField] private Button NextButton = null;
    [SerializeField] private GameObject[] Panels = null;
    [SerializeField] private RawImage BubbleSicker = null;

    [Header("========== 도안 선택 ==========")]
    [SerializeField] private RawImage[] StickerDesignArr = null;
    [SerializeField] private Func_SwipeMove StickerDesign = null;

    [Header("========== 기울이기 ==========")]
    [SerializeField] private Func_Tilt myTilt = null;
    [SerializeField] private RectTransform[] bucketPos = null;
    [SerializeField] private RectTransform myPos = null;
    [SerializeField] private RawImage[] ColorBuckets = null;
    [SerializeField] private Button[] ColorButtons = null;
    [SerializeField] private RawImage defaultBucketColor = null;

    #endregion

    private bool isDefaultBottleSelected = false;
    private int PanelIdx = 0;

    protected override void Start()
    {
        base.Start();
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
        DecideDesignAndColor();
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

    private void DecideDesignAndColor()
    {
        switch (PanelIdx)
        {
            case 0:
                BubbleSicker.texture = StickerDesignArr[StickerDesign.CurrentSticker].texture;
                break;

            case 1:
                BubbleSicker.color = defaultBucketColor.color;
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
            case "Green":
                defaultBucketColor.color = ColorBuckets[0].color;
                break;

            case "Pink":
                defaultBucketColor.color = ColorBuckets[1].color;
                break;

            case "Blue":
                defaultBucketColor.color = ColorBuckets[2].color;
                break;
        }
    }

    public void OnClick_SaveBubbleSticker()
    {
        OnClick_SaveImgae(StickerType.BubbleSticker);
        // StickerDesign.CurrentSticker : 0 -> turtle, 1 -> trueseal, 2 -> grampus

        switch (StickerDesign.CurrentSticker)
        {
            case 0:
                
                break;

            case 1:

                break;

            case 2:

                break;
        }

        
    }

    protected override void OnClick_SaveImgae(StickerType stickerType)
    {
        base.OnClick_SaveImgae(stickerType);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Manager_BubbleSticker : Func_SaveSticker
{
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

    #region AnotherVariables
    [SerializeField] private bool isDefaultBottleSelected = false;
    private int PanelIdx = 0;
    private int colorType = 0;
    #endregion

    protected override void Start()
    {
        base.Start();
        Init();
    }

    public void Init()
    {
        PanelIdx = 0;
        BackButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(true);
        NextButton.interactable = true;
        myTilt.enabled = false;
        myPos.transform.position = bucketPos[0].transform.position;

        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        Panels[0].SetActive(true);

        for (int i = 0; i < ColorButtons.Length; i++)
        {
            ColorButtons[i].interactable = false;
        }

        isDefaultBottleSelected = false;
        InitDefaultBucket();
    }

    private void DecideDesignAndColor()
    {
        switch (PanelIdx)
        {
            case 0:
                BubbleSicker.texture = StickerDesignArr[StickerDesign.CurrentSticker].texture;
                break;

            case 1:
                if (defaultBucketColor.color == ColorBuckets[0].color)
                {
                    colorType = 1;
                }
                else if (defaultBucketColor.color == ColorBuckets[1].color)
                {
                    colorType = 2;
                }
                else if (defaultBucketColor.color == ColorBuckets[2].color)
                {
                    colorType = 3;
                }

                string file = "";
                // StickerDesign.CurrentSticker : 0 -> turtle, 1 -> trueseal, 2 -> grampus
                // colorType : 0 -> green, 1 -> pink, 2 -> blue
                switch (StickerDesign.CurrentSticker)
                {
                    case 0:
                        switch (colorType)
                        {
                            case 1:
                                file = "TurtleGreen";
                                break;

                            case 2:
                                file = "TurtlepPink";
                                break;

                            case 3:
                                file = "TurtleBlue";
                                break;
                        }
                        break;

                    case 1:
                        switch (colorType)
                        {
                            case 1:
                                file = "TrueSealGreen";
                                break;

                            case 2:
                                file = "TrueSealPink";
                                break;

                            case 3:
                                file = "TrueSealBlue";
                                break;
                        }
                        break;

                    case 2:
                        switch (colorType)
                        {
                            case 1:
                                file = "GrampusGreen";
                                break;

                            case 2:
                                file = "GrampusPink";
                                break;

                            case 3:
                                file = "GrampusBlue";
                                break;
                        }
                        break;
                }
                GetBubbleStickerImage(file);

                break;
        }
    }

    private void GetBubbleStickerImage(string fileName)
    {
        Texture2D texture = Resources.Load<Texture2D>("FinishedDesigns/" + fileName);
        BubbleSicker.texture = texture;
    }

    private void InitDefaultBucket()
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

    private void PlaceDefaultBucket()
    {
        myPos.transform.position = bucketPos[1].transform.position;
        myTilt.enabled = true;
        for (int i = 0; i < ColorButtons.Length; i++)
        {
            ColorButtons[i].interactable = true;
        }
    }

    public void OnClick_NextBtn()
    {
        DecideDesignAndColor();
        BackButton.gameObject.SetActive(true);
        if (PanelIdx + 1 == Panels.Length - 1) NextButton.gameObject.SetActive(false);
        NextButton.interactable = false;
        Panels[PanelIdx + 1].SetActive(true);
        Panels[PanelIdx].SetActive(false);
        PanelIdx++;
    }

    public void OnClick_BackBtn()
    {
        if(PanelIdx == 2) InitDefaultBucket();
        NextButton.gameObject.SetActive(true);
        if (PanelIdx - 1 == 0) BackButton.gameObject.SetActive(false);
        Panels[PanelIdx - 1].SetActive(true);
        Panels[PanelIdx].SetActive(false);
        PanelIdx--;
        if (PanelIdx == 0)
        {
            NextButton.interactable = true;
            InitDefaultBucket();
        }
        else NextButton.interactable = false;
    }

    public void OnClick_DefaultBottle()
    {
        isDefaultBottleSelected = !isDefaultBottleSelected;
        if (isDefaultBottleSelected == true)
        {
            PlaceDefaultBucket();
        }
        else
        {
            InitDefaultBucket();
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

    public void SaveBubbleSticker()
    {
        OnClick_SaveImgae(StickerType.BubbleSticker);
    }

    protected override void OnClick_SaveImgae(StickerType stickerType)
    {
        base.OnClick_SaveImgae(stickerType);
        
    }
}

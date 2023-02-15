using System.Collections;
using System.Collections.Generic;
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
    public GameObject BackGround = null;

    [Header("========== 도안 선택 ==========")]
    [SerializeField] private RawImage[] StickerDesignArr = null;
    [SerializeField] private Func_SwipeMove StickerDesign = null;

    [Header("========== 기울이기 ==========")]
    [SerializeField] private Func_Tilt[] colorBucketTitlts = null;
    [SerializeField] private RectTransform placePos = null;
    [SerializeField] private RectTransform[] colorBucketsRect = null;
    [SerializeField] private RectTransform[] colorBucketsInitPos = null;
    [SerializeField] private Toggle[] colorBucketToggles = null;
    [SerializeField] private RawImage[] colorBeakersImgs = null;
    #endregion

    #region AnotherVariables
    private int PanelIdx = 0;
    [SerializeField] private int colorType = 0;
    public int ColorType { get { return colorType; } private set { } }

    #endregion

    protected override void Start()
    {
        base.Start();
        Init();
    }

    public void Init()
    {
        PanelIdx = 0;
        colorType = 0;
        BackButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(true);

        for(int i = 0; i < colorBucketTitlts.Length; ++i)
        {
            colorBucketTitlts[i].enabled = false;
        }

        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        Panels[0].SetActive(true);

        ActiveColortBucket(false);
    }

    private void DecideDesignAndColor()
    {
        switch (PanelIdx)
        {
            case 0:
                BubbleSicker.texture = StickerDesignArr[StickerDesign.CurrentSticker].texture;
                break;

            case 1:
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

    public void ActiveColortBucket(bool active)
    {
        for (int i = 0; i < colorBucketToggles.Length; i++)
        {
            colorBucketToggles[i].interactable = active;
        }
        for (int i = 0; i < colorBucketToggles.Length; i++)
        {
            colorBucketToggles[i].interactable = active;
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
        NextButton.gameObject.SetActive(false);
        InitPanel();
    }

    private void InitPanel()
    {
        switch (PanelIdx)
        {
            case 0:

                break;

            case 1:
                colorBeakersImgs[0].gameObject.SetActive(true);
                colorBeakersImgs[1].gameObject.SetActive(false);
                colorBeakersImgs[2].gameObject.SetActive(false);
                colorBeakersImgs[3].gameObject.SetActive(false);
                colorBeakersImgs[4].gameObject.SetActive(false);
                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;
        }
    }

    public void OnClick_BackBtn()
    {
        if(PanelIdx == 2) ActiveColortBucket(false);
        if (PanelIdx - 1 == 0) BackButton.gameObject.SetActive(false);
        Panels[PanelIdx - 1].SetActive(true);
        Panels[PanelIdx].SetActive(false);
        PanelIdx--;
        if (PanelIdx == 0)
        {
            NextButton.gameObject.SetActive(true);
            ActiveColortBucket(false);
        }
    }

    public void OnClick_SelectColor(string color)
    {
        switch (color)
        {
            case "Green":
                colorType = 1;
                break;

            case "Pink":
                colorType = 2;
                break;

            case "Blue":
                colorType = 3;
                break;
        }
        for(int i = 0; i < colorBucketToggles.Length; ++i)
        {
            if (colorBucketToggles[i].isOn == true)
            {
                colorBucketTitlts[i].enabled = true;
                colorBucketsRect[i].position = placePos.position;
            }
            else
            {
                colorBucketTitlts[i].enabled = false;
                colorBucketsRect[i].position = colorBucketsInitPos[i].position;
                colorBucketsRect[i].localEulerAngles = Vector3.zero;
            }
        }
    }

    public void InitColorBucket()
    {
        for (int i = 0; i < colorBucketTitlts.Length; i++)
        {
            colorBucketTitlts[i].enabled = false;
            colorBucketsRect[i].position = colorBucketsInitPos[i].position;
            colorBucketsRect[i].localEulerAngles = Vector3.zero;
        }
    }

    public void SaveBubbleSticker()
    {
        BackGround.SetActive(false);
        OnClick_SaveImgae(StickerType.BubbleSticker);
    }

    protected override void OnClick_SaveImgae(StickerType stickerType)
    {
        StartCoroutine(SaveProcess(stickerType));
    }

    IEnumerator SaveProcess(StickerType stickerType)
    {
        base.OnClick_SaveImgae(stickerType);
        isSaveDone = false;
        yield return new WaitUntil(() => isSaveDone == true);
        BackGround.SetActive(true);
    }
}

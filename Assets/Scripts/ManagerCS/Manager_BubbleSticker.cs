using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager_BubbleSticker : Func_SaveSticker
{
    #region SerializeField
    [Header("========== 기본 UI ==========")]
    [SerializeField] private Button backButton = null;
    [SerializeField] private Button nextButton = null;
    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private RawImage bubbleSicker = null;
    public RawImage BubbleSticker { get { return bubbleSicker; } private set { } } 
    public GameObject BackGround = null;

    [Header("========== 도안 선택 ==========")]
    [SerializeField] private RawImage[] stickerDesignArr = null;
    [SerializeField] private Func_SwipeMove stickerDesign = null;

    [Header("========== 기울이기 ==========")]
    [SerializeField] private Func_Tilt[] colorBucketTilts = null;
    [SerializeField] private RectTransform placePos = null;
    [SerializeField] private RectTransform[] colorBucketsRect = null;
    [SerializeField] private RectTransform[] colorBucketsInitPos = null;
    [SerializeField] private Toggle[] colorBucketToggles = null;
    [SerializeField] private RawImage[] colorBeakersImgs = null;
    [SerializeField] private GameObject colorBeakersTiltGuideLine = null;

    [SerializeField] private GameObject collocatePanel = null;
    #endregion

    #region AnotherVariables
    private bool isTouchColorBeaker = false;
    private int PanelIdx = 0;
    private int colorType = 0;
    public int ColorType { get { return colorType; } private set { } }
    private string file = "";
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
        backButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);

        for(int i = 0; i < colorBucketTilts.Length; ++i)
        {
            colorBucketTilts[i].enabled = false;
        }

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[0].SetActive(true);

        ActiveColorBucket(false);
    }

    public void RestartBubbleSticker()
    {
        SceneManager.LoadScene("BubbleSticker");
    }

    private void DecideDesignAndColor()
    {
        switch (PanelIdx)
        {
            case 0:
                bubbleSicker.texture = stickerDesignArr[stickerDesign.CurrentSticker].texture;
                break;

            case 1:
                file = "";
                // StickerDesign.CurrentSticker : 0 -> turtle, 1 -> trueseal, 2 -> grampus
                // colorType : 0 -> green, 1 -> pink, 2 -> blue
                switch (stickerDesign.CurrentSticker)
                {
                    case 0:
                        switch (colorType)
                        {
                            case 1:
                                file = "TurtleGreen";
                                break;

                            case 2:
                                file = "TurtlePink";
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
        bubbleSicker.texture = texture;
    }

    public void ActiveColorBucket(bool active)
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

    private void InitCurPanel()
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
                colorBeakersTiltGuideLine.SetActive(false);
                isTouchColorBeaker = false;
                nextButton.gameObject.SetActive(false);
                break;

            case 2:

                break;

            case 3:
                collocatePanel.SetActive(true);
                break;

            case 4:

                break;
        }
    }

    public void OnClick_NextBtn()
    {
        DecideDesignAndColor();
        backButton.gameObject.SetActive(true);
        if (PanelIdx + 1 == panels.Length - 1) nextButton.gameObject.SetActive(false);
        panels[PanelIdx + 1].SetActive(true);
        panels[PanelIdx].SetActive(false);
        PanelIdx++;
        nextButton.gameObject.SetActive(false);
        InitCurPanel();
    }

    public void OnClick_BackBtn()
    {
        if (PanelIdx == 2) ActiveColorBucket(false);
        if (PanelIdx - 1 == 0) backButton.gameObject.SetActive(false);
        panels[PanelIdx - 1].SetActive(true);
        panels[PanelIdx].SetActive(false);
        PanelIdx--;
        if (PanelIdx == 0)
        {
            Manager_Main.Instance.GetAudio().PlaySound("NextButton", SoundType.Common, gameObject, false, true);
            nextButton.gameObject.SetActive(true);
            ActiveColorBucket(false);
        }
        InitCurPanel();
    }

    public void OnClick_SelectColor(string color)
    {
        Manager_Main.Instance.GetAudio().PlaySound("ComeBack", SoundType.Touch, gameObject, false, true);
        nextButton.gameObject.SetActive(false);
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
                colorBucketTilts[i].enabled = true;
                colorBucketsRect[i].position = placePos.position;
            }
            else
            {
                colorBucketTilts[i].enabled = false;
                colorBucketsRect[i].position = colorBucketsInitPos[i].position;
                colorBucketsRect[i].localEulerAngles = Vector3.zero;
            }
        }

        if (isTouchColorBeaker == false)
        {
            isTouchColorBeaker = true;
            colorBeakersTiltGuideLine.SetActive(true);
        }
    }

    public void InitColorBucket()
    {
        for (int i = 0; i < colorBucketTilts.Length; i++)
        {
            colorBucketTilts[i].enabled = false;
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
        saveFileName = file;
        base.OnClick_SaveImgae(stickerType);
        isSaveDone = false;
        yield return new WaitUntil(() => isSaveDone == true);
        BackGround.SetActive(true);
    }
}

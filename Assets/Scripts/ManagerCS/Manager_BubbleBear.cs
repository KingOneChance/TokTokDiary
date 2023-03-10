using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Manager_BubbleBear : MonoBehaviour
{
    [Header("===Buttons===")]
    [SerializeField] private Button buttonRecord = null;
    [SerializeField] private Button buttonRecordStop = null;
    [SerializeField] private Button buttonRerecord = null;
    [SerializeField] private Button buttonPlay = null;
    [SerializeField] private Button buttonSave = null;

    [SerializeField] private GameObject buttonGoHome = null;
    [SerializeField] private GameObject buttonBefore = null;
    [SerializeField] private GameObject buttonNext = null;

    [Header("===Ojects===")]
    [SerializeField] private GameObject homeCanvas = null;
    [SerializeField] private GameObject makeCanvas = null;
    [SerializeField] private GameObject audioCanvas = null;
    [SerializeField] private GameObject signCanvas = null;
    [SerializeField] private GameObject checkCanvas = null;
    [SerializeField] private GameObject finishCanvas = null;

    [SerializeField] private GameObject afterRecord = null;
    [SerializeField] private GameObject popUpImage = null;
    [SerializeField] private GameObject drawCam = null;

    [SerializeField] private GameObject toggleBoddy = null;
    [SerializeField] private GameObject toggleBelly = null;
    [SerializeField] private GameObject toggleHead = null;
    [SerializeField] private GameObject toggleArmLeg = null;

    [SerializeField] private GameObject sceneBefore = null;
    [SerializeField] private GameObject sceneNext = null;
    [SerializeField] private GameObject restartAll = null;
    [SerializeField] private GameObject stickerRepo = null;
    [SerializeField] private GameObject hedgehogStage = null;
    [SerializeField] private TextMeshProUGUI explainText = null;


    [Header("===StageUI===")]
    [SerializeField] private RawImage Ui_Stage1 = null;
    [SerializeField] private RawImage Ui_Stage2 = null;
    [SerializeField] private RawImage Ui_Stage3 = null;
    [SerializeField] private GameObject Ui_Process1On = null;
    [SerializeField] private GameObject Ui_Process2On = null;
    [SerializeField] private GameObject Ui_Process3On = null;
    [SerializeField] private GameObject Ui_Process1Off = null;
    [SerializeField] private GameObject Ui_Process2Off = null;
    [SerializeField] private GameObject Ui_Process3Off = null;
    [SerializeField] private Color stageColor;

    [Header("===Scripts===")]
    [SerializeField] private Func_BubbleBearCtrl bubbleBearCtrl = null;
    [SerializeField] private Func_BubbleBearSave bubbleBearSave = null;
    [SerializeField] private Func_BubbleBearSign bubbleBearSign = null;
    [SerializeField] private Func_Record record = null;

    private bool isRecording = false;
    private bool canMoveAudio = false;
    private SelectHedgehogState selectState = SelectHedgehogState.Body;
    private NowStateInAudio nowState = NowStateInAudio.Make;

    private void Start()
    {
        //canvas on
        makeCanvas.SetActive(true);
        audioCanvas.SetActive(false);
        signCanvas.SetActive(false);
        checkCanvas.SetActive(false);
        finishCanvas.SetActive(false);
        drawCam.SetActive(false);
        buttonBefore.SetActive(false);
        stickerRepo.SetActive(false);
        toggleBoddy.SetActive(true);
        toggleBelly.SetActive(false);
        toggleHead.SetActive(false);
        toggleArmLeg.SetActive(false);
        buttonGoHome.SetActive(false);

        //Canvas move button GameObject
        sceneBefore.SetActive(false);
        sceneNext.SetActive(true);
        Manager_Main.Instance.GetAudio().PlaySound("NextButton", SoundType.Common, gameObject, false, true);
        StageUIChange(1);
        InitAfterRecordButton();
    }
    #region Hedgehog make button

    public void OnClick_ButtonNext()
    {      
        toggleBoddy.SetActive(false);
        toggleBelly.SetActive(false);
        toggleHead.SetActive(false);
        toggleArmLeg.SetActive(false);

        //body to belly
        if (selectState == SelectHedgehogState.Body)
        {
            if (bubbleBearCtrl.IsSelected(SelectHedgehogState.Body))
            {
                selectState = SelectHedgehogState.Belly;
                buttonBefore.SetActive(true);
                toggleBelly.SetActive(true);
            }
            else
            {
                toggleBoddy.SetActive(true);
            }
        }
        //belly to head
        else if (selectState == SelectHedgehogState.Belly)
        {
            if (bubbleBearCtrl.IsSelected(SelectHedgehogState.Belly))
            {
                selectState = SelectHedgehogState.Head;
                toggleHead.SetActive(true);
            }
            else
            {
                toggleBelly.SetActive(true);
            }
        }
        //head to arm&leg
        else if (selectState == SelectHedgehogState.Head)
        {
            if (bubbleBearCtrl.IsSelected(SelectHedgehogState.Head))
            {
                selectState = SelectHedgehogState.ArmLeg;
                buttonNext.SetActive(false);
                toggleArmLeg.SetActive(true);
            }
            else
            {
                toggleHead.SetActive(true);
            }
        }
        //arm&leg to Record saveCanvas
        else if (selectState == SelectHedgehogState.ArmLeg)
        {
            if (bubbleBearCtrl.IsSelected(SelectHedgehogState.Head))
            {
                selectState = SelectHedgehogState.ArmLeg;
                toggleArmLeg.SetActive(true);

                //  taskCanvas.SetActive(false);
                //  saveCanvas.SetActive(true);
            }
            else
            {
                toggleArmLeg.SetActive(true);
            }
        }
    }
    public void OnClick_ButtonBefore()
    {
        toggleBoddy.SetActive(false);
        toggleBelly.SetActive(false);
        toggleHead.SetActive(false);
        toggleArmLeg.SetActive(false);
        //body to body
        if (selectState == SelectHedgehogState.Body)
        {
            selectState = SelectHedgehogState.Body;
            toggleBoddy.SetActive(true);
        }
        //belly to body
        else if (selectState == SelectHedgehogState.Belly)
        {
            selectState = SelectHedgehogState.Body;
            buttonBefore.SetActive(false);
            toggleBoddy.SetActive(true);
        }
        //head to belly
        else if (selectState == SelectHedgehogState.Head)
        {
            selectState = SelectHedgehogState.Belly;
            toggleBelly.SetActive(true);
        }
        //arm&leg to head
        else if (selectState == SelectHedgehogState.ArmLeg)
        {
            selectState = SelectHedgehogState.Head;
            buttonNext.SetActive(true);
            toggleHead.SetActive(true);
        }
    }

    #endregion

    #region Record Buttons

    //Record Start
    public void OnClick_ButtonRecord()
    {
        if (isRecording == true) return;

        InitAfterRecordButton();
        //record button hide
        buttonRecord.gameObject.SetActive(false);
        //recordStop button open
        buttonRecordStop.gameObject.SetActive(true);
        isRecording = true;
    }
    //Record Stop
    public void OnClick_ButtonRecordStop()
    {
        if (isRecording == false) return;
        //recordStop button hide
        buttonRecordStop.gameObject.SetActive(false);
        //AfterRecord buttons are opened
        afterRecord.SetActive(true);
        isRecording = false;
    }
    public void OnClick_ButtonPlay()
    {
        //Sound play eff
    }
    //Rerecord
    public void OnClick_ButtonRerecord()
    {
        InitAfterRecordButton();
    }
    #endregion
    #region Canvas Change
    //Canvas move to next Canvas
    public void OnClick_ToNextCanvas()
    {
        if (nowState == NowStateInAudio.Make)
        {
            if (bubbleBearCtrl.canMoveToAudio)
            {
                hedgehogStage.SetActive(false);
                StartCoroutine(Co_SetActiveCheck());
            }
        }
        else if (nowState == NowStateInAudio.Audio)//no func
        {

        }
        else if (nowState == NowStateInAudio.Sign)
        {
            //temporary save sign image
            bubbleBearSign.SaveTempSign();
            nowState = NowStateInAudio.Attatch;
            StageUIChange(3);
        }
    }
    //Canvas move to before Canvas
    public void OnClick_ToBeforeCanvas()
    {
        if (nowState == NowStateInAudio.Audio)
        {
            RestartScene();
        }
        else if (nowState == NowStateInAudio.Sign) //no func
        {

        }
        else if (nowState == NowStateInAudio.Attatch)
        {
            signCanvas.SetActive(true);
            checkCanvas.SetActive(false);
            sceneBefore.SetActive(false);
            sceneNext.SetActive(true);
            Manager_Main.Instance.GetAudio().PlaySound("NextButton", SoundType.Common, gameObject, false, true);
            drawCam.SetActive(true);
            nowState = NowStateInAudio.Sign;
            //back to stage2
            StageUIChange(2);
        }
    }
    //Canvas Change "AudioCanvas" to "SignCanvas"
    public void OnClick_ButtonSave()
    {
        InitAfterRecordButton();
        sceneNext.SetActive(true);
        Manager_Main.Instance.GetAudio().PlaySound("NextButton", SoundType.Common, gameObject, false, true);
        sceneBefore.SetActive(false);
        nowState = NowStateInAudio.Sign;
        //Send Picked SpriteImage to SaveScript
        //bubbleBearSave.GetSaveSPrite(bubbleBearCtrl.nowSprite);

        audioCanvas.SetActive(false);
        signCanvas.SetActive(true);
        drawCam.SetActive(true);
    }
    //This is PopUp Function 
    public void OnClick_HomePopUp()
    {
        if (nowState == NowStateInAudio.Make || nowState==NowStateInAudio.Done) SceneManager.LoadScene("StickerFactory");
        else popUpImage.SetActive(true);
    }
    //Canvas change "signCanvas" to "checkCanvas"
    public void MoveSignToCheckCanvas()
    {
        signCanvas.SetActive(false);
        checkCanvas.SetActive(true);
        sceneBefore.SetActive(true);
        sceneNext.SetActive(false);
        buttonNext.SetActive(false);
        bubbleBearSign.SetImage();
    }
    //Canvas change "MakeCanvas" to "AudioCanvas"
    public void MoveMakeToAudioCanvas()
    {
        makeCanvas.SetActive(false);
        audioCanvas.SetActive(true);
        sceneBefore.SetActive(true); //beforScene button on
        sceneNext.SetActive(false); //nextScene button off
    }
    //Canvas Change "CheckCanvas" to "FinishCanvas"
    public void MoveCheckToFinishCanvas()
    {
        finishCanvas.SetActive(true);
        checkCanvas.SetActive(false);
        restartAll.SetActive(true);
        stickerRepo.SetActive(true);
        sceneBefore.SetActive(false);
        buttonGoHome.SetActive(true);

        nowState = NowStateInAudio.Done;

        bubbleBearSave.TextureChange();
    }
    #endregion
    //Initiate Button to OriginState.
    private void InitAfterRecordButton()
    {
        buttonRecord.gameObject.SetActive(true);
        buttonRecordStop.gameObject.SetActive(false);
        afterRecord.SetActive(false);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("BubbleBear");
    }
    private void StageUIChange(int num)
    {
        switch (num)
        {
            case 1:
                Ui_Process1On.SetActive(true);
                Ui_Process2On.SetActive(false);
                Ui_Process3On.SetActive(false);
                Ui_Process1Off.SetActive(false);
                Ui_Process2Off.SetActive(true);
                Ui_Process3Off.SetActive(true);
                break;

            case 2:
                Ui_Process1On.SetActive(false);
                Ui_Process2On.SetActive(true);
                Ui_Process3On.SetActive(false);
                Ui_Process1Off.SetActive(true);
                Ui_Process2Off.SetActive(false);
                Ui_Process3Off.SetActive(true);
                break;

            case 3:
                Ui_Process1On.SetActive(false);
                Ui_Process2On.SetActive(false);
                Ui_Process3On.SetActive(true);
                Ui_Process1Off.SetActive(true);
                Ui_Process2Off.SetActive(true);
                Ui_Process3Off.SetActive(false);
                break;
        }
    }
    IEnumerator Co_SetActiveCheck()
    {
        yield return new WaitForEndOfFrame();
        bubbleBearCtrl.SaveTempHedgeHog();
        nowState = NowStateInAudio.Audio;
        StageUIChange(2);
    }
}

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
    [SerializeField] private Button buttonSign = null;

    [SerializeField] private Button buttonBefore = null;
    [SerializeField] private Button buttonNext = null;

    [Header("===Ojects===")]
    [SerializeField] private GameObject homeCanvas = null;
    [SerializeField] private GameObject taskCanvas = null;
    [SerializeField] private GameObject saveCanvas = null;
    [SerializeField] private GameObject checkCanvas = null;
    [SerializeField] private GameObject afterRecord = null;
    [SerializeField] private GameObject popUpImage = null;
    [SerializeField] private GameObject drawDraw = null;

    [SerializeField] private GameObject toggleBoddy = null;
    [SerializeField] private GameObject toggleBelly = null;
    [SerializeField] private GameObject toggleHead = null;
    [SerializeField] private GameObject toggleArmLeg = null;


    [Header("===Scripts===")]
    [SerializeField] private Func_BubbleBearCtrl bubbleBearCtrl = null;
    [SerializeField] private Func_BubbleBearSave bubbleBearSave = null;
    [SerializeField] private Func_BubbleBearSign bubbleBearSign = null;
    [SerializeField] private Func_Record record = null;

    private bool isRecording = false;
    private SelectHedgehogState selectState = SelectHedgehogState.Body;

    private void Start()
    {
        //canvas on
        saveCanvas.SetActive(false);
        checkCanvas.SetActive(false);
        drawDraw.SetActive(false);

        toggleBoddy.SetActive(true);
        toggleBelly.SetActive(false);
        toggleHead.SetActive(false);
        toggleArmLeg.SetActive(false);

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
                toggleBelly.SetActive(true);
            }
            else
            {
                toggleBoddy.SetActive(true);
                Debug.Log("몸을 선택해주세요");
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
                Debug.Log("배를 선택해주세요");
            }
        }
        //head to arm&leg
        else if (selectState == SelectHedgehogState.Head)
        {
            if (bubbleBearCtrl.IsSelected(SelectHedgehogState.Head))
            {
                selectState = SelectHedgehogState.ArmLeg;
                toggleArmLeg.SetActive(true);
            }
            else
            {
                toggleHead.SetActive(true);
                Debug.Log("머리를 선택해주세요");
            }
        }
        //arm&leg to Record saveCanvas
        else if (selectState == SelectHedgehogState.ArmLeg)
        {
            if (bubbleBearCtrl.IsSelected(SelectHedgehogState.Head))
            {
                selectState = SelectHedgehogState.ArmLeg;
                taskCanvas.SetActive(false);
                saveCanvas.SetActive(true);
            }
            else
            {
                toggleArmLeg.SetActive(true);
                Debug.Log("팔다리를 선택해주세요");
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
            toggleHead.SetActive(true);
        }
    }

    #endregion
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
    //Canvas Change "TaskCanvas" to "SaveCanvas"
    public void OnClick_ButtonSave()
    {
        InitAfterRecordButton();

        //Send Picked SpriteImage to SaveScript
        bubbleBearSave.GetSaveSPrite(bubbleBearCtrl.nowSprite);

        taskCanvas.SetActive(false);
        saveCanvas.SetActive(true);
        drawDraw.SetActive(true);
    }
    //This is PopUp Function 
    public void OnClick_HomePopUp()
    {
        popUpImage.SetActive(true);
    }
    //Canvas change "saveCanvas" to "checkCanvas"
    public void OnClick_ButtonSign()
    {
        homeCanvas.SetActive(false);
        saveCanvas.SetActive(false);
        checkCanvas.SetActive(true);
        drawDraw.SetActive(false);
    }
    //Initiate Button to OriginState.
    private void InitAfterRecordButton()
    {
        buttonRecord.gameObject.SetActive(true);
        buttonRecordStop.gameObject.SetActive(false);
        afterRecord.SetActive(false);
    }
}

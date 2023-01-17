using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Manager_BubbleBear : MonoBehaviour
{
    [Header("===Buttons===")]
    [SerializeField] private Button buttonHome = null;
    [SerializeField] private Button buttonRecord = null;
    [SerializeField] private Button buttonRecordStop = null;
    [SerializeField] private Button buttonPlay = null;
    [SerializeField] private Button buttonRerecord = null;
    [SerializeField] private Button buttonSave = null;
    [SerializeField] private Button buttonSign = null;

    [Header("===Ojects===")]
    [SerializeField] private GameObject taskCanvas = null;
    [SerializeField] private GameObject saveCanvas = null;
    [SerializeField] private GameObject checkCanvas = null;
    [SerializeField] private GameObject afterRecord = null;
    [SerializeField] private GameObject popUpImage = null;

    [Header("===Scripts===")]
    [SerializeField] private Func_BubbleBearCtrl bubbleBearCtrl = null;
    [SerializeField] private Func_BubbleBearSave bubbleBearSave = null;
    [SerializeField] private Func_BubbleBearSign bubbleBearSign = null; 
    [SerializeField] private Func_Record record = null;

    private bool isRecording = false;

    private void Start()
    {
        record = FindObjectOfType<Func_Record>();
        saveCanvas.SetActive(false);
        checkCanvas.SetActive(false);
        InitAfterRecordButton();
    }

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
    }
    //This is PopUp Function 
    public void OnClick_HomePopUp()
    {
        popUpImage.SetActive(true);
    }
    //Canvas change "saveCanvas" to "checkCanvas"
    public void OnClick_ButtonSign()
    {
        saveCanvas.SetActive(false);
        checkCanvas.SetActive(true);
    }




    //Initiate Button to OriginState.
    private void InitAfterRecordButton()
    {
        buttonRecord.gameObject.SetActive(true);
        buttonRecordStop.gameObject.SetActive(false);
        afterRecord.SetActive(false);
    }
}

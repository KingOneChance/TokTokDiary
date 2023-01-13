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

    [Header("===Ojects===")]
    [SerializeField] private GameObject taskCanvas = null;
    [SerializeField] private GameObject saveCanvas = null;
    [SerializeField] private GameObject AfterRecord = null;

    [Header("===Scripts===")]
    [SerializeField] private Func_BubbleBearCtrl bubbleBearCtrl = null;
    [SerializeField] private Func_BubbleBearSave bubbleBearSave = null;
    [SerializeField] private Func_Record record = null;

    private bool isRecording = false;

    private void Start()
    {
        bubbleBearCtrl = FindObjectOfType<Func_BubbleBearCtrl>();
        bubbleBearSave = FindObjectOfType<Func_BubbleBearSave>();
        record = FindObjectOfType<Func_Record>();
        saveCanvas.SetActive(false);
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
        AfterRecord.SetActive(true);
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
    public void OnClick_ButtonSave()
    {
        InitAfterRecordButton();

        //Send Picked SpriteImage to SaveScript
        bubbleBearSave.GetSaveSPrite(bubbleBearCtrl.nowSprite);
        
        //Canvas Change TaskCanvas to SaveCanvas
        taskCanvas.SetActive(false);
        saveCanvas.SetActive(true);
    }
    public void OnClick_ButtonHome()
    {
        SceneManager.LoadScene("Main");
    }
    //Initiate Button to OriginState.
    private void InitAfterRecordButton()
    {
        buttonRecord.gameObject.SetActive(true);
        buttonRecordStop.gameObject.SetActive(false);
        AfterRecord.SetActive(false);
    }
}

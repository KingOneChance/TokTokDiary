using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_BubbleBear : MonoBehaviour
{
    [Header("===Buttons===")]
    [SerializeField] private Button buttonRecord = null;
    [SerializeField] private Button buttonRecordStop = null;
    [SerializeField] private Button buttonRecordDone = null;
    [SerializeField] private Button buttonPlay = null;
    [SerializeField] private Button buttonRerecord = null;
    [SerializeField] private Button buttonSave = null;
    [SerializeField] private GameObject AfterRecord = null;

    [Header("===Canvas===")]
    [SerializeField] private GameObject taskCanvas = null;
    [SerializeField] private GameObject saveCanvas = null;

    private bool isRecording = false;

    private void Start()
    {
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
        buttonRecordDone.gameObject.SetActive(true);
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
        
        taskCanvas.SetActive(false);
        saveCanvas.SetActive(true);
    }

    //Initiate Button to OriginState.
    private void InitAfterRecordButton()
    {
        buttonRecord.gameObject.SetActive(true);
        buttonRecordStop.gameObject.SetActive(false);
        buttonRecordDone.gameObject.SetActive(false);
        AfterRecord.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.IO;
using UnityEngine.UI;
//isRun has purpose for preventing the multi Sound Playing. WonChan
//isRun has the purpose of preventing multiple sound playback. Google
//The Purpose of isRun is to prevent the multi Sound play. JiHye
public class Func_Record : MonoBehaviour
{
    [Header("===RecordPart===")]
    [SerializeField] private AudioSource playSouce = null;
    [SerializeField] private AudioClip newClip = null;
    [SerializeField] private string soundSouceName = null;
    [SerializeField] private bool isRun = false;
    [SerializeField] private int audioNum;
    [SerializeField] private int auidPlayNum;
    [SerializeField] private float nowClipTime;
    [Header("===BubbleBearImage===")]
    [SerializeField] private RawImage bubbleBearImage = null;
    [Header("===Buttons===")]
    [SerializeField] private GameObject finishPlayButton = null;
    [Header("===Timer===")]
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private GameObject timerBox = null;
    [SerializeField] private bool timerRun = false;
    [SerializeField] private float limitedMinutes = 5;

    [Header("===ListenEff===")]
    [SerializeField] private GameObject leftHearing1 = null;
    [SerializeField] private GameObject leftHearing2 = null;
    [SerializeField] private GameObject leftHearing3 = null;
    [SerializeField] private GameObject rightHearing1 = null;
    [SerializeField] private GameObject rightHearing2 = null;
    [SerializeField] private GameObject rightHearing3 = null;


    private float limitedSeconds;

    private void Start()
    {
        playSouce = GetComponent<AudioSource>();
        audioNum = 0;
        auidPlayNum = 1;
        timerBox.SetActive(false);
        limitedSeconds = limitedMinutes * 60;
        timerRun = false;
    }

    public void OnClick_Record()
    {
        timerBox.SetActive(true);
        timerRun = true;
        newClip = null;
        newClip = Microphone.Start(null, true, 240, 44100);
        audioNum = Manager_Main.Instance.GetRecordNum("RecordFile");
        StartCoroutine(Co_UiTimer());
        //this is for timer

    }
    public void OnClick_RecordStop()
    {
        timerRun = false;
        StopRecordMicrophone(newClip);
        StopCoroutine(Co_UiTimer());
        timerBox.SetActive(false);
    }

    //After record
    public void OnClick_Play()
    {
        if (playSouce.isPlaying == true)
        {
            playSouce.Stop();
            return;
        }
        else
        {
            playSouce.clip = newClip;
            playSouce.Play();
            //Listening eff start
            StartCoroutine(Co_ListenEff());
        }
 
    }
    public void OnClick_PlayInFinish()
    {
        playSouce.clip = newClip;
        finishPlayButton.SetActive(false);
        playSouce.Play();
        StartCoroutine(Co_EndReturn());
    }


    public void OnClick_Rerecord()
    {
        DeleteRecordSource();
    }
    public void OnClick_Save()
    {
        //canvas change in Manager_BubbleBear ..connect with del
        if (bubbleBearImage.texture == null) return;
        else
        {
            StopSoundPlay();
            Manager_BubbleBear bubbleBear = FindObjectOfType<Manager_BubbleBear>();
            bubbleBear.OnClick_ButtonSave();
        }
    }
    public void DeleteRecordSource()
    {
        if (File.Exists(Application.persistentDataPath + "/RecordFile/" + soundSouceName + "_" + audioNum + ".wav"))
            File.Delete(Application.persistentDataPath + "/RecordFile/" + soundSouceName + "_" + audioNum + ".wav");
    }
    //Save File Logic
    public void StopRecordMicrophone(AudioClip recordClip)
    {
        int lastTime = Microphone.GetPosition(null);

        if (lastTime == 0)
            return;
        else
        {
            Microphone.End(Microphone.devices[0]);

            float[] samples = new float[recordClip.samples];

            recordClip.GetData(samples, 0);

            float[] cutSamples = new float[lastTime];

            Array.Copy(samples, cutSamples, cutSamples.Length - 1);

            recordClip = AudioClip.Create("Notice", cutSamples.Length, 1, 44100, false);

            recordClip.SetData(cutSamples, 0);
        }
        string today = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        //count for number of recorded audioes
        //audioNum++;
        Func_SavWav.Save(Application.persistentDataPath + "/RecordFile/" + soundSouceName + "_" + audioNum, recordClip);
    }

    //The purpose of "OnClick_Load" is Test about Save file
    public void OnClick_Load()
    {
        playSouce.clip = Func_WavUtility.ToAudioClip(Application.persistentDataPath + "/RecordFile/" + soundSouceName + auidPlayNum + ".wav");

        playSouce.Play();
        auidPlayNum++;
        if (auidPlayNum > audioNum) auidPlayNum = 1;
    }
    //It is called by only OnClick_Play() method.
    private void IsRunStateChange() => isRun = false;
    private void StopSoundPlay() => playSouce.Stop();

    IEnumerator Co_EndReturn()
    {
        yield return new WaitForSeconds(time);
        finishPlayButton.SetActive(true);
    }
    [SerializeField] private float time = 0f;
    IEnumerator Co_UiTimer()
    {
        time = 0f;
        yield return null;
        while (timerRun == true)
        {
            time += Time.deltaTime;
            yield return null;
            if (time > limitedSeconds)
            {
                OnClick_Save();
                yield break;
            }
            if (time % 60 < 10)
                timerText.text = (int)(time / 60) + ":0" + (int)(time % 60);
            else
                timerText.text = (int)(time / 60) + ":" + (int)(time % 60);
        }
    }
    IEnumerator Co_ListenEff()
    {
        float playTime = 0;
        Debug.Log("사운드 이펙트 재생");
        while (playTime <= time)
        {
            playTime += Time.deltaTime;
            leftHearing1.SetActive(true);
            rightHearing1.SetActive(true);

            leftHearing2.SetActive(false);
            rightHearing2.SetActive(false);

            leftHearing3.SetActive(false);
            rightHearing3.SetActive(false);
            if (playSouce.isPlaying == false) break;
            yield return new WaitForSeconds(0.5f);
            playTime += 0.5f;

            leftHearing1.SetActive(true);
            rightHearing1.SetActive(true);

            leftHearing2.SetActive(true);
            rightHearing2.SetActive(true);

            leftHearing3.SetActive(false);
            rightHearing3.SetActive(false);
            if (playSouce.isPlaying == false) break;
            yield return new WaitForSeconds(0.5f);
            playTime += 0.5f;

            leftHearing1.SetActive(true);
            rightHearing1.SetActive(true);

            leftHearing2.SetActive(true);
            rightHearing2.SetActive(true);

            leftHearing3.SetActive(true);
            rightHearing3.SetActive(true);
            if (playSouce.isPlaying == false) break;
            yield return new WaitForSeconds(0.5f);
            playTime += 0.5f;
        }
        leftHearing1.SetActive(false);
        rightHearing1.SetActive(false);

        leftHearing2.SetActive(false);
        rightHearing2.SetActive(false);

        leftHearing3.SetActive(false);
        rightHearing3.SetActive(false);
    }
}
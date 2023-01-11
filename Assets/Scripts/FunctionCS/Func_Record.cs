using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.IO;
//isRun has purpose for preventing the multi Sound Playing. WonChan
//isRun has the purpose of preventing multiple sound playback. Google
//The Purpose of isRun is to prevent the multi Sound play. JiHye
public class Func_Record : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newClipState;
    [SerializeField] private TextMeshProUGUI playSourceState;
    [SerializeField] private AudioSource playSouce;
    [SerializeField] private AudioClip newClip;
    [SerializeField] private string soundSouceName;
    [SerializeField] private bool isRun;
    [SerializeField] private int audioNum;
    [SerializeField] private int auidPlayNum;
    [SerializeField] private float nowClipTime;


    private void Start()
    {
        playSouce = GetComponent<AudioSource>();
        audioNum = 0;
        auidPlayNum = 1;
    }

    public void OnClick_Record()
    {
        newClip = null;
        newClip = Microphone.Start(null, true, 240, 44100);
    }
    public void OnClick_RecordStop()
    {
        StopRecordMicrophone(newClip);
    }

    //After record
    public void OnClick_Play()
    {
        playSouce.clip = newClip;
        playSouce.Play();
    }
    public void OnClick_Rerecord()
    {
        File.Delete(Application.persistentDataPath + "/Record/" + soundSouceName + audioNum + ".wav");
    }
    public void OnClick_Save()
    {
        //canvas change in Manager_BubbleBear ..connect with del
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
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        //count for number of recorded audioes
        audioNum++;
        Func_SavWav.Save(Application.persistentDataPath + "/Record/" + soundSouceName + audioNum, recordClip);
    }

    //The purpose of "OnClick_Load" is Test about Save file
    public void OnClick_Load()
    {
        playSouce.clip = Func_WavUtility.ToAudioClip(Application.persistentDataPath + "/Record/" + soundSouceName + auidPlayNum + ".wav");

        playSouce.Play();
        auidPlayNum++;
        if (auidPlayNum > audioNum) auidPlayNum = 1;
    }



    //It is called by only OnClick_Play() method.
    private void IsRunStateChange() => isRun = false;
}
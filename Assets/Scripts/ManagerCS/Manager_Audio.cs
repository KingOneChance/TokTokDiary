using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Audio : MonoBehaviour
{
    private Manager_Audio() { }
    [SerializeField] Data_Audios DiarySounds = null;
    [SerializeField] Data_Audios BubbleGunSounds = null;
    [SerializeField] Data_Audios BubbleBearSounds = null;
    [SerializeField] Data_Audios BubbleStickerSounds = null;
    [SerializeField] Data_Audios BGMSounds = null;
    [SerializeField] Data_Audios TouchSounds = null;
    [SerializeField] Data_Audios CommonSounds = null;

    [SerializeField] Slider bgmSlider = null;
    [SerializeField] Slider effSlider = null;

    private Dictionary<string, AudioClip> DiaryClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> BubbleGunClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> BubbleBearClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> BubbleStickerClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> BGMClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> TouchClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> CommonClips = new Dictionary<string, AudioClip>();

    [SerializeField] private float val_BGM = 0f;
    [SerializeField] private float val_Eff = 0f;
    [SerializeField] private float curTempVal_BGM = 0f;
    [SerializeField] private float curTempVal_Eff = 0f;

    private void Awake()
    {
        InitVolumeVal();
        AddAllClipsToDic(DiarySounds);
        AddAllClipsToDic(BubbleGunSounds);
        AddAllClipsToDic(BubbleBearSounds);
        AddAllClipsToDic(BubbleStickerSounds);
        AddAllClipsToDic(BGMSounds);
        AddAllClipsToDic(TouchSounds);
        AddAllClipsToDic(CommonSounds);
    }
   
    /// <summary>
    /// Init volume value
    /// </summary>
    private void InitVolumeVal()
    {
        if (PlayerPrefs.HasKey("Val_BGM"))
        {
            val_BGM = PlayerPrefs.GetFloat("Val_BGM");
        }
        else
        {
            val_BGM = 0.5f;
        }

        if (PlayerPrefs.HasKey("Val_Eff"))
        {
            val_Eff = PlayerPrefs.GetFloat("Val_Eff");
        }
        else
        {
            val_Eff = 0.5f;
        }

        bgmSlider.value = val_BGM;
        effSlider.value = val_Eff;
    }

    /// <summary>
    /// Add the clips held by the scriptable object to the Dictionary
    /// </summary>
    /// <param name="obj"> A scriptable object that contains audio clips. </param>
    private void AddAllClipsToDic(Data_Audios obj)
    {
        switch (obj.soundType)
        {
            case SoundType.Diary:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    DiaryClips.Add(DiarySounds.Sounds[i].name, DiarySounds.Sounds[i]);
                }
                break;

            case SoundType.BubbleGun:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    BubbleGunClips.Add(BubbleGunSounds.Sounds[i].name, BubbleGunSounds.Sounds[i]);
                }
                break;

            case SoundType.BubbleBear:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    BubbleBearClips.Add(BubbleBearSounds.Sounds[i].name, BubbleBearSounds.Sounds[i]);
                }
                break;

            case SoundType.BubbleSticker:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    BubbleStickerClips.Add(BubbleStickerSounds.Sounds[i].name, BubbleStickerSounds.Sounds[i]);
                }
                break;

            case SoundType.BGM:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    BGMClips.Add(BGMSounds.Sounds[i].name, BGMSounds.Sounds[i]);
                }
                break;
            case SoundType.Touch:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    TouchClips.Add(TouchSounds.Sounds[i].name, TouchSounds.Sounds[i]);
                }
                break;
            case SoundType.Common:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    CommonClips.Add(CommonSounds.Sounds[i].name, CommonSounds.Sounds[i]);
                }
                break;
        }
    }

    /// <summary>
    /// Unlock Memory Unnecessary
    /// </summary>
    /// <param name="obj"></param>
    public void UnlockClipsToDic(Data_Audios obj)
    {
        switch (obj.soundType)
        {
            case SoundType.Diary:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    DiaryClips.Clear();
                }
                break;

            case SoundType.BubbleGun:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    BubbleGunClips.Clear();
                }
                break;

            case SoundType.BubbleBear:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    BubbleBearClips.Clear();
                }
                break;

            case SoundType.BubbleSticker:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    BubbleStickerClips.Clear();
                }
                break;

            case SoundType.BGM:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    BGMClips.Clear();
                }
                break;
        }
    }

    /// <summary>
    /// return requested Audioclip for others
    /// </summary>
    /// <param name="type">The type in AudioManager containing the audio clip to request.</param>
    /// <param name="clipName">The name of requested audioclip</param>
    /// <returns></returns>
    private AudioClip GetClip(SoundType type, string clipName)
    {
        return CheckType(type).ContainsKey(clipName) ? CheckType(type)[clipName] : null;
    }

    /// <summary>
    /// Return the corresponding Dictionary via type
    /// </summary>
    /// <param name="soundType"> The type containing the audio clip to request. </param>
    /// <returns></returns>
    private Dictionary<string, AudioClip> CheckType(SoundType soundType)
    {
        Dictionary<string, AudioClip> curdic = new Dictionary<string, AudioClip>();
        switch (soundType)
        {
            case SoundType.BGM:
                curdic = BGMClips;
                break;

            case SoundType.Diary:
                curdic = DiaryClips;
                break;

            case SoundType.Touch:
                curdic = TouchClips;
                break;

            case SoundType.BubbleGun:
                curdic = BubbleGunClips;
                break;

            case SoundType.BubbleBear:
                curdic = BubbleBearClips;
                break;

            case SoundType.BubbleSticker:
                curdic = BubbleStickerClips;
                break;

            case SoundType.Common:
                curdic = CommonClips;
                break;
        }

        if (curdic == null)
        {
            Debug.LogError("The type does not exist");
            return null;
        }
        else return curdic;
    }

    public void PlaySound(string ClipName, SoundType soundType,GameObject ObjToPlaySound, bool playLoop, bool overLap)
    {
        // Check AudioSource Component and set loop
        AudioSource audioSource = null;
        if(ObjToPlaySound.TryGetComponent(out AudioSource source))
        {
            audioSource = source;
            audioSource.playOnAwake = false;
            audioSource.loop = playLoop;
            audioSource.clip = GetClip(soundType, ClipName);
            SetVolume(soundType, audioSource);
            // Check AudioClip to play And PlaySound
            if (overLap == true)
            {
                audioSource.Play();
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }
        else
        {
            ObjToPlaySound.AddComponent<AudioSource>();
            audioSource = ObjToPlaySound.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = playLoop;
            audioSource.clip = GetClip(soundType, ClipName);
            SetVolume(soundType, audioSource);
            if (overLap == true)
            {
                audioSource.Play();
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }        
    }

    public void PlayLocalSound(string ClipName, GameObject ObjToPlaySound, bool playLoop, bool overLap)
    {
        // Check AudioSource Component and set loop
        AudioSource audioSource = null;
        if (ObjToPlaySound.TryGetComponent(out AudioSource source))
        {
            audioSource = source;
            audioSource.playOnAwake = false;
            audioSource.loop = playLoop;
            //audioSource.clip = Func_WavUtility.ToAudioClip(Application.persistentDataPath + "/RecordFile/" + ClipName + ".wav");
            audioSource.clip = Func_WavUtility.ToAudioClip(ClipName);
            audioSource.volume = val_Eff;
            // Check AudioClip to play And PlaySound
            if (overLap == true)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }
        else
        {
            ObjToPlaySound.AddComponent<AudioSource>();
            audioSource = ObjToPlaySound.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = playLoop;
            //audioSource.clip = Func_WavUtility.ToAudioClip(Application.persistentDataPath + "/RecordFile/" + ClipName + ".wav");
            audioSource.clip = Func_WavUtility.ToAudioClip(ClipName);
            audioSource.volume = val_Eff;
            // Check AudioClip to play And PlaySound
            if (overLap == true)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }
    }

    public void StopPlaySound(GameObject stopPlaySoundObj)
    {
        if (stopPlaySoundObj == null) return;
        // Check AudioSource Component and set loop
        AudioSource audioSource = null;
        if (stopPlaySoundObj.TryGetComponent(out AudioSource source))
        {
            audioSource = source;
            audioSource.pitch = 1f;
            audioSource.Stop();
        }
    }

    private void SetVolume(SoundType type, AudioSource source)
    {
        if(type != SoundType.BGM)
        {
            source.volume = val_Eff;
            return;
        }
        else
        {
            Manager_Main.Instance.mainAudioSource.volume = val_BGM;
        }
    }

    public void SetBGMValue(Slider slider)
    {
        val_BGM = slider.value;
        Manager_Main.Instance.mainAudioSource.volume = val_BGM;
        if (val_BGM == 0)
        {
            Manager_Main.Instance.bgmXImage.CrossFadeAlpha(1.0f, 0.5f, true);
        }
        else
        {
            Manager_Main.Instance.bgmXImage.CrossFadeAlpha(0f, 0.5f, true);
        }
    }

    public void CheckVolume()
    {
        if (val_BGM == 0)
        {
            Manager_Main.Instance.bgmXImage.CrossFadeAlpha(1.0f, 0.5f, true);
        }
        else
        {
            Manager_Main.Instance.bgmXImage.CrossFadeAlpha(0f, 0.5f, true);
        }

        if (val_Eff == 0) Manager_Main.Instance.effXImage.CrossFadeAlpha(1.0f, 0.5f, true);
        else Manager_Main.Instance.effXImage.CrossFadeAlpha(0f, 0.5f, true);
    }

    public void SetEffValue(Slider slider)
    {
        val_Eff = slider.value;
        if (val_Eff == 0) Manager_Main.Instance.effXImage.CrossFadeAlpha(1.0f, 0.5f, true);
        else Manager_Main.Instance.effXImage.CrossFadeAlpha(0f, 0.5f, true);
    }

    public void OnClick_VolumeXButton(Slider slider)
    {
        if(slider.value != 0)
        {
            if(slider.name == "bgmController")
            {
                curTempVal_BGM = slider.value;
            }
            else
            {
                curTempVal_Eff = slider.value;
            }
            slider.value = 0f;
        }
        else
        {
            if (slider.name == "bgmController")
            {
                slider.value = curTempVal_BGM;
                curTempVal_BGM = 0f;
            }
            else
            {
                slider.value = curTempVal_Eff;
                curTempVal_Eff = 0f;
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Val_BGM", val_BGM);
        PlayerPrefs.SetFloat("Val_Eff", val_Eff);
    }
}

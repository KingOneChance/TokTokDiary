using System.Collections.Generic;
using UnityEngine;

public class Manager_Audio : MonoBehaviour
{
    private Manager_Audio() { }
    [SerializeField] Data_Audios DiaryCaseSounds = null;
    [SerializeField] Data_Audios BubbleGunSounds = null;
    [SerializeField] Data_Audios BubbleBearSounds = null;
    [SerializeField] Data_Audios BubbleStickerSounds = null;
    [SerializeField] Data_Audios FreeStickerSounds = null;
    [SerializeField] Data_Audios TouchSounds = null;
    [SerializeField] Data_Audios CommonSounds = null;

    private Dictionary<string, AudioClip> DiaryCaseClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> BubbleGunClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> BubbleBearClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> BubbleStickerClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> FreeStickerClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> TouchClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> CommonClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        AddAllClipsToDic(DiaryCaseSounds);
        AddAllClipsToDic(BubbleGunSounds);
        AddAllClipsToDic(BubbleBearSounds);
        AddAllClipsToDic(BubbleStickerSounds);
        AddAllClipsToDic(FreeStickerSounds);
        AddAllClipsToDic(TouchSounds);
        AddAllClipsToDic(CommonSounds);
    }

    /// <summary>
    /// Add the clips held by the scriptable object to the Dictionary
    /// </summary>
    /// <param name="obj"> A scriptable object that contains audio clips. </param>
    private void AddAllClipsToDic(Data_Audios obj)
    {
        switch (obj.soundType)
        {
            case SoundType.DiaryCase:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    DiaryCaseClips.Add(DiaryCaseSounds.Sounds[i].name, DiaryCaseSounds.Sounds[i]);
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
                    BubbleStickerClips.Add(BubbleBearSounds.Sounds[i].name, BubbleBearSounds.Sounds[i]);
                }
                break;

            case SoundType.FreeSticker:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    FreeStickerClips.Add(FreeStickerSounds.Sounds[i].name, FreeStickerSounds.Sounds[i]);
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
            case SoundType.DiaryCase:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    DiaryCaseClips.Clear();
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

            case SoundType.FreeSticker:
                for (int i = 0; i < obj.Sounds.Length; i++)
                {
                    FreeStickerClips.Clear();
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
            case SoundType.FreeSticker:
                curdic = FreeStickerClips;
                break;

            case SoundType.DiaryCase:
                curdic = DiaryCaseClips;
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

    public void PlaySound(string ClipName, SoundType soundType,GameObject ObjToPlaySound, bool playLoop)
    {
        // Check AudioSource Component and set loop
        AudioSource audioSource = null;
        if(ObjToPlaySound.TryGetComponent(out AudioSource source))
        {
            audioSource = source;
            audioSource.playOnAwake = false;
            audioSource.loop = playLoop;
            audioSource.clip = GetClip(soundType, ClipName);
            // Check AudioClip to play And PlaySound
            audioSource.PlayOneShot(GetClip(soundType, ClipName));
        }
        else
        {
            ObjToPlaySound.AddComponent<AudioSource>();
            audioSource = ObjToPlaySound.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = playLoop;
            audioSource.clip = GetClip(soundType, ClipName);
            // Check AudioClip to play And PlaySound
            audioSource.PlayOneShot(GetClip(soundType, ClipName));
            Debug.LogError("No Exist AudioSourceComponent Please Check");
            return;
        }        
    }
}

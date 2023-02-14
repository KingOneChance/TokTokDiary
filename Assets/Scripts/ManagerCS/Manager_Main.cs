using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Scripting;
using Unity.VisualScripting;

public class Manager_Main : MonoBehaviour
{
    #region SingleTon
    private static Manager_Main instance = null;
    public static Manager_Main Instance { get { return instance; } }
    private void Awake()
    {
        // DontDestroyOnLoad instance of MainManager to make MainManager SingleTon
        if (instance == null)
        {
            // Init Process Run
            ui_StickerRepository = FindObjectOfType<UI_StickerRepository>();
            GetAllInfoFromServer();
            instance = this;
            Screen.SetResolution(1920, 1080, true);
            DontDestroyOnLoad(instance);
        }
        else return;
    }
    #endregion

    /// <summary>
    /// Declare all "Variable"
    /// </summary>
    #region Declared Variable
    [Header("UIManager")]
    //����
    [SerializeField] private UI_Main ui_Main = null;

    public UI_Main UI_Main { get { return ui_Main; } }
    [field: SerializeField]
    public Manager_PictureDiary manager_PictureDiary { get; private set; }

    private UI_StickerRepository ui_StickerRepository = null;
    public UI_StickerRepository UI_StickerRepository { get { return ui_StickerRepository; } }

    [field: SerializeField]
    [SerializeField] public GameObject ui_StickerRepositoryPrefab { get; private set; }

    [Header("===NumberOfStickers===")]
    [SerializeField] private int getBubbleGunStickerNum = 0;
    [SerializeField] private int getAudioStickerNum = 0;
    [SerializeField] private int getSignStickerNum = 0;
    [SerializeField] private int getBubbleStickerNum = 0;
    [SerializeField] private int getFreeStickerNum = 0;
    [SerializeField] private int getDiaryNum = 0;

    [Header("===NumberOfStickers===")]
    [SerializeField] private int setBubbleGunStickerNum = 0;
    [SerializeField] private int setAudioStickerNum = 0;
    [SerializeField] private int setSignStickerNum = 0;
    [SerializeField] private int setBubbleStickerNum = 0;
    [SerializeField] private int setFreeStickerNum = 0;
    [SerializeField] private int setDiaryNum = 0;

 

    #endregion


    #region These functions are for the purpose of finding a specific manager in specific Scene.

    public void InitPictureDiaryScene()
    {
        manager_PictureDiary = FindObjectOfType<Manager_PictureDiary>();
    }
    public void LeaveAtPictureDiaryScene()
    {
        manager_PictureDiary = null;
    }

    public void InitMainScene()
    {

    }

    #endregion

    private void Start()
    {
        
    }

    private void Update()
    {
        // Check user input every frame
        Manager_UserInput.UpdateTouch();
    }

    private void GetAllInfoFromServer()
    {
        // Receiving data from server, initial work, etc.
    }

    public void OnClick_StickerRepositoryOn()
    {
        if (!ui_StickerRepositoryPrefab.activeSelf)
            ui_StickerRepositoryPrefab.SetActive(true);
        UI_StickerRepository.OnClick_RepositoryOpen();
    }
    public void OnClick_StickerRepositoryOff()
    {
        if (ui_StickerRepositoryPrefab.activeSelf)
            ui_StickerRepositoryPrefab.SetActive(false);
    }

    //manange recordSticker Number 
    public int GetBubbleGunStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getBubbleGunStickerNum = allFiles.Length;
            return getBubbleGunStickerNum;
        }
    }
    public int GetBubbleStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getBubbleStickerNum = allFiles.Length;
            return getBubbleStickerNum;
        }
    }
    public int GetAudioStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getAudioStickerNum = allFiles.Length;
            return getAudioStickerNum;
        }
    }
    public int GetSignStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getSignStickerNum = allFiles.Length;
            return getSignStickerNum;
        }
    }
    public int GetRecordNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.wav", SearchOption.TopDirectoryOnly);
            getAudioStickerNum = allFiles.Length;
            return getAudioStickerNum;
        }
    }
    public int GetFreeStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getFreeStickerNum = allFiles.Length;
            return getFreeStickerNum;
        }
    }
    public int GetDiaryNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getDiaryNum = allFiles.Length;
            return getDiaryNum;
        }
    }

    /// <summary>
    /// If the sticker already exists,
    /// 5 is added to the number of times the sticker can be used, 
    /// and if the sticker does not exist, 
    /// the value of 5 is added as a new key.
    /// </summary>
    /// <param name="stickerName"></param>
    public void SaveStickerNumberOfTimesAvailable(string stickerName)
    {
        if (PlayerPrefs.HasKey(stickerName))
        {
            PlayerPrefs.SetInt(stickerName, PlayerPrefs.GetInt(stickerName) + 5);
        }
        else
        {
            PlayerPrefs.SetInt(stickerName, 5);
        }
    }

    /// <summary>
    /// First, check if the key of the sticker you want to use exists,
    /// return it if it does not exist,
    /// and if it exists,
    /// subtract 1 from the key value and save it.
    /// </summary>
    /// <param name="stickerName"></param>
    public void UseStickerNumberOfTimesAvailable(string stickerName)
    {
        if(PlayerPrefs.HasKey(stickerName) == false)
        {
            Debug.LogError("The sticker you are trying to use does not exist. Please check Again - Request JongHoon");
            return;
        }
        PlayerPrefs.SetInt(stickerName, PlayerPrefs.GetInt(stickerName) - 1);
        if (PlayerPrefs.GetInt(stickerName) == 0)
            PlayerPrefs.DeleteKey(stickerName);
    }

    public void SetBubbleGunStickerNum() => setBubbleGunStickerNum++;
    public void SetBubbleStickerNum() => setBubbleStickerNum++;
    public void SetAudioStickerNum() => setAudioStickerNum++;
    public void SetSignStickerNum() => setSignStickerNum++;
    public void SetFreeStickerNum() => setFreeStickerNum++;
    public void SetDiaryNum() => setDiaryNum++;
}
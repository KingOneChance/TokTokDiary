using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Scripting;

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
            data_LocalSticker = GetComponent<Data_LocalSticker>();
            func_LoadPngFile = GetComponent<Func_LoadPngFile>();
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

    [Header("Datas")]
    private Data_LocalSticker data_LocalSticker = null;
    public Data_LocalSticker Data_LocalSticker { get { return data_LocalSticker; } }

    private Func_LoadPngFile func_LoadPngFile = null;
    public Func_LoadPngFile Func_LoadPngFile { get { return func_LoadPngFile; } }
    public Manager_PictureDiary manager_PictureDiary { get; private set; }

    private UI_StickerRepository ui_StickerRepository = null;
    public UI_StickerRepository UI_StickerRepository { get { return ui_StickerRepository; } }

    [field: SerializeField]
    [SerializeField] public GameObject ui_StickerRepositoryPrefab { get; private set; }

    [Header("===NumberOfRecordSticker===")]
    [SerializeField] int recordStickerNum = 0;
    #endregion


    #region These functions are for the purpose of finding a specific manager in specific Scene.
    public void InitMainScene()
    {

    }
    public void LeaveAtPictureDiaryScene()
    {
        manager_PictureDiary = null;
    }
    #endregion



    private void Start()
    {
#if UNITY_EDITOR_WIN
       
#else
       string[] allFiles = Directory.GetFiles(Application.persistentDataPath + "/RecordSticker/", "*.png", SearchOption.TopDirectoryOnly);

        recordStickerNum = allFiles.Length/2;
#endif



    }
    private void GetAllInfoFromServer()
    {
        // Receiving data from server, initial work, etc.
    }

    public void OnClick_StickerRepositoryOn()
    {
        if (!ui_StickerRepositoryPrefab.activeSelf) 
            ui_StickerRepositoryPrefab.SetActive(true);
    }
    public void OnClick_StickerRepositoryOff()
    {
        if (ui_StickerRepositoryPrefab.activeSelf)
            ui_StickerRepositoryPrefab.SetActive(false);
    }

    //manange recordSticker Number 
    public int GetRecordStickerNum()
    {
        return recordStickerNum;
    }
    public void SetRecordStickerNum()
    {
        recordStickerNum++;
    }
}

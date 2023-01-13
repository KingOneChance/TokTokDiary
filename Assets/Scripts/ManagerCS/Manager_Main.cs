using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Manager_Main : MonoBehaviour
{
    #region SingleTon
    private static Manager_Main instance = null;
    public static Manager_Main Instance { get { return instance; } }

    [Header("UIManager")]
    //메인
    [SerializeField] private UI_Main ui_Main = null;
    public UI_Main UI_Main { get { return ui_Main; } }
    //그림일기
    [SerializeField] private UI_PictureDiary ui_PictureDiary = null;
    public UI_PictureDiary UI_PictureDiary { get { return ui_PictureDiary; } }

    [Header("Datas")]
    [SerializeField] private Data_LocalSticker data_LocalSticker = null;
    public Data_LocalSticker Data_LocalSticker { get { return data_LocalSticker; } }

    private void Awake()
    {
        // DontDestroyOnLoad instance of MainManager to make MainManager SingleTon
        if (instance == null)
        {
            // Init Process Run
            data_LocalSticker = GetComponent<Data_LocalSticker>();
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

    public void AwakeOnCreateSticker()
    {
        if(ui_PictureDiary == null)
            ui_PictureDiary = FindObjectOfType<UI_PictureDiary>();
    }
    private void GetAllInfoFromServer()
    {
        // Receiving data from server, initial work, etc.
    }
}

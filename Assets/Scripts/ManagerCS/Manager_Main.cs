using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            GetAllInfoFromServer();
            instance = this;
            Screen.SetResolution(960, 540, false);
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

    }
    private void GetAllInfoFromServer()
    {
        // Receiving data from server, initial work, etc.
    }
}

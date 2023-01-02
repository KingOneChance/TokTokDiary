using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Manager_Main : MonoBehaviour
{
    #region SingleTon
    private static Manager_Main instance = null;
    public static Manager_Main Instance { get { return instance; } }
    #endregion

    private void Awake()
    {
        // DontDestroyOnLoad instance of MainManager to make MainManager SingleTon
        if (instance == null)
        {
            // Init Process Run
            GetAllInfoFromServer();
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else return;
    }

    private void GetAllInfoFromServer()
    {
        // Receiving data from server, initial work, etc.
    }
}

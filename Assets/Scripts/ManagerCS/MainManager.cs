using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    #region SingleTon
    private static MainManager instance = null;
    public static MainManager Instance { get { return instance; } }
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

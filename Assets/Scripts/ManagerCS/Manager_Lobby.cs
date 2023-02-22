using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Lobby : MonoBehaviour
{
    [SerializeField] private GameObject mainManager = null;
    private void Awake()
    {
        if (FindObjectOfType<Manager_Main>() != null) return;
        else Instantiate(mainManager);      
    }
}

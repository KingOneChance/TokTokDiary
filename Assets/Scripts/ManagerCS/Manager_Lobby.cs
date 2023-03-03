using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Lobby : MonoBehaviour
{
    [SerializeField] private GameObject mainManager = null;
    [SerializeField] private Button quitButton = null;
    private void Awake()
    {
        if (FindObjectOfType<Manager_Main>() != null) return;
        else Instantiate(mainManager);
    }

    private void OnEnable()
    {
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit(0);
        });
    }
}

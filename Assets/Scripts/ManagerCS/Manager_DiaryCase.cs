using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class Manager_DiaryCase : MonoBehaviour
{
    [SerializeField] GameObject[] Profiles = null;
    [SerializeField] RawImage[] ProfileImages = null;
    [SerializeField] TextMeshProUGUI[] ProfileName = null;

    private string path = "";
    private void Start()
    {
        path = Application.persistentDataPath  + "/Profile";
        DirectoryInfo di = new DirectoryInfo(path);

        for(int i = 0; i < di.GetDirectories().Length; i++)
        {
            ProfileName[i].text = di.GetDirectories()[i].Name;
        }

        for (int i = 0; i < ProfileName.Length; i++)
        {
            if (ProfileName[i].text == "")
            {
                Profiles[i].GetComponent<Button>().interactable = true;
                ProfileImages[i].color = Vector4.zero;
            } 
        }
    }   
}

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_DiaryCase : MonoBehaviour
{
    [SerializeField] RawImage[] profiles = null;
    [SerializeField] GameObject[] panels = null;
    [SerializeField] Button[] profileButton = null;
    [SerializeField] TextMeshProUGUI[] profileName = null;

    [SerializeField] RawImage previewImg = null;

    [SerializeField] List<Texture2D> diaryList = new List<Texture2D>();
    [SerializeField] private string selectedProfileName = "";
    [SerializeField] private string selectedProfilPath = "";

    private void Start()
    {
        for (int i = 0; i < profiles.Length; i++)
        {
            if (profiles[i].texture == null) profileButton[i].interactable = false;
        }
    }

    public void OnClick_Profile(int idx)
    {
        selectedProfileName = profileName[idx - 1].text;
        selectedProfilPath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/" + "Diary";
        AddDiaryFiles();
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    private void AddDiaryFiles()
    {
        string[] allFiles = Directory.GetFiles(selectedProfilPath, "*.png", SearchOption.AllDirectories);

        for (int i = 0; i < allFiles.Length; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);
                diaryList.Add(texture);
            }
        }
        previewImg.texture = diaryList[0];
    }
}

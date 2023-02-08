using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_DiaryCase : MonoBehaviour
{
    [SerializeField] RawImage[] profiles = null;
    [SerializeField] GameObject[] panels = null;
    [SerializeField] Button[] profileButton = null;
    [SerializeField] TextMeshProUGUI[] profileName = null;

    [SerializeField] private string selectedProfileName = "";
    private string selectedProfilPath = "";

    private void Start()
    {
        for(int i = 0; i < profiles.Length; i++)
        {
            if (profiles[i].texture == null) profileButton[i].interactable = false;
        }
    }

    public void OnClick_Profile(int idx)
    {
        selectedProfileName = profileName[idx - 1].text;
        selectedProfilPath = Application.persistentDataPath + "/" + selectedProfileName + "/";
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    private void CheckDiaryFiles()
    {

    }
}

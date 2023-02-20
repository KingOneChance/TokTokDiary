using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Func_LoadProfilePicture : MonoBehaviour
{
    [SerializeField] RawImage[] profileImages = null;
    [SerializeField] TextMeshProUGUI[] textMeshProUGUI;
    private void OnEnable()
    {
        for(int i = 0; i < profileImages.Length; i++)
        {
            profileImages[i].gameObject.SetActive(false);
        }
        string path = Application.persistentDataPath + "/Profile";
        if (false == Directory.Exists(path)) Directory.CreateDirectory(path);
        string[] allFiles = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);

        DirectoryInfo di = new DirectoryInfo(path);
 
        for (int i = 0; i < di.GetDirectories().Length; i++)
        {
            Debug.Log(di.GetDirectories().Length);
            if (allFiles[i].Contains(di.GetDirectories()[i].Name) == true)
            {
                byte[] byteTexture = File.ReadAllBytes(allFiles[i]);

                if (byteTexture.Length > 0)
                {
                    Texture2D texture = new Texture2D(0, 0);
                    texture.LoadImage(byteTexture);
                    profileImages[i].gameObject.SetActive(true);
                    profileImages[i].texture = texture;
                    textMeshProUGUI[i].text = di.GetDirectories()[i].Name;
                }
            }

        }
    }
}

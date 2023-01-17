using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class Func_LoadPngFile : MonoBehaviour
{
    UI_PictureDiary picture_Diary = null;
    
    // Start is called before the first frame update
    void Start()
    {
        FindAllPng();
    }
    
    //폴더에 있는 모든 파일 긁어오기
    public void FindAllPng()
    {
#if UNITY_EDITOR_WIN
        string path = "C:/Users/User/Desktop/Sticker/";
#else
        string path = Application.persistentDataPath + "/RecordSticker/";
#endif
        string[] allFiles = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
        for (int i = 0; i < allFiles.Length; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);


            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);


                if (allFiles[i].Contains("Waffle"))
                {
                    Manager_Main.Instance.Data_LocalSticker.AddInventory(texture, ItemType.Waffle);
                    //Debug.Log("와플");
                }
                else if (allFiles[i].Contains("Audio"))
                {
                    Manager_Main.Instance.Data_LocalSticker.AddInventory(texture, ItemType.Audio);
                    Debug.Log("오디오");
                }
                else if (allFiles[i].Contains("Weather"))
                {
                    Manager_Main.Instance.Data_LocalSticker.AddInventory(texture, ItemType.Weather);
                    Debug.Log("날씨");
                }
                else if (allFiles[i].Contains("Diary"))
                {
                    Manager_Main.Instance.Data_LocalSticker.AddInventory(texture, ItemType.Diary);
                    Debug.Log("일기");
                }
                else
                {
                    Debug.Log("나머지");
                }
            }          
        }
    }

    //바탕화면에 있는 해당 이미지 불러오기
    public Texture2D LoadTexture(string fileName)
    {
        Texture2D texture = new Texture2D(4, 4);
        string path = "C:/Users/User/Desktop/Sticker/";
        FileStream fs = new FileStream(path + fileName + ".png", FileMode.Open, FileAccess.Read);
        byte[] imageData = new byte[fs.Length];
        fs.Read(imageData, 0, (int)fs.Length);
        texture.LoadImage(imageData);

        return texture;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Func_LoadPngFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindAllPng();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //폴더에 있는 모든 파일 긁어오기
    public void FindAllPng()
    {
        string path = "C:/Users/User/Desktop/Sticker/";
        string[] allFiles = Directory.GetFiles(path, "*.png",SearchOption.TopDirectoryOnly);

        for(int i = 0; i < allFiles.Length; i++)
        {
            if (allFiles[i].Contains("Waffle"))
            {
                Debug.Log("와플");
            }
            else if (allFiles[i].Contains("Audio"))
            {
                Debug.Log("오디오");
            }
            else if (allFiles[i].Contains("Weather"))
            {
                Debug.Log("날씨");
            }
            else if (allFiles[i].Contains("Diary"))
            {
                Debug.Log("일기");
            }
            else
            {
                Debug.Log("나머지");
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

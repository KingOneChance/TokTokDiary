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
    //������ �ִ� ��� ���� �ܾ����
    public void FindAllPng()
    {
        string path = "C:/Users/User/Desktop/Sticker/";
        string[] allFiles = Directory.GetFiles(path, "*.png",SearchOption.TopDirectoryOnly);

        for(int i = 0; i < allFiles.Length; i++)
        {
            if (allFiles[i].Contains("Waffle"))
            {
                Debug.Log("����");
            }
            else if (allFiles[i].Contains("Audio"))
            {
                Debug.Log("�����");
            }
            else if (allFiles[i].Contains("Weather"))
            {
                Debug.Log("����");
            }
            else if (allFiles[i].Contains("Diary"))
            {
                Debug.Log("�ϱ�");
            }
            else
            {
                Debug.Log("������");
            }
        }
    }

    //����ȭ�鿡 �ִ� �ش� �̹��� �ҷ�����
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

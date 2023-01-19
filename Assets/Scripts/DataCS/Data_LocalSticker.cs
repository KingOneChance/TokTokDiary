using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data_LocalSticker : MonoBehaviour
{
    private List<Texture2D> audioList = new List<Texture2D>();
    private List<Texture2D> weatherList = new List<Texture2D>();
    private List<Texture2D> handMadeList = new List<Texture2D>();
    public List<Texture2D> waffleList = new List<Texture2D>();
    private List<Texture2D> diaryList = new List<Texture2D>();

    private void Awake()
    {
        ClearInventory();
    }

    private void Start()
    {
        //GetServerData();
    }

    public void AddInventory(Texture2D obj, ItemType state)
    {
        if (state == ItemType.Weather)
        {
            //Add Weather Sticker
            weatherList.Add(obj);
        }
        else if (state == ItemType.Handmade)
        {
            //Add Handmade Sticker
            handMadeList.Add(obj);
        }
        else if (state == ItemType.Waffle)
        {
            //Add Waffle Sticker
            waffleList.Add(obj);
        }
        else if (state == ItemType.Audio)
        {
            //Add Audio Sticker
            audioList.Add(obj);
        }
        else if (state == ItemType.Diary)
        {
            //Add Diary
            diaryList.Add(obj);
        }

        // Save At Server
        //UpdateToServer(obj);
    }

    //������
    public void GetServerData()
    {
        // Load saved items from the main manager
        // Returns the data for statement and puts it into a list by type
        //for(int i = 0; i < dataCount; i++){
        //    }

    }

    //public void RemoveInventory(Texture2D obj, ItemType state)
    //{
    //    // Remove the used stickers from the list
    //    if (state == ItemType.Weather)
    //    {
    //        // Remove Weather Sticker
    //        weatherList.Remove(obj);
    //        //�������� ����
    //       // UpdateToServer(obj);
    //    }
    //    else if (state == ItemType.Handmade)
    //    {
    //        //�������� ��ƼĿ �߰�
    //        handMadeList.Remove(obj);
    //        //�������� ����
    //        //UpdateToServer(obj);
    //    }
    //    else if (state == ItemType.Waffle)
    //    {
    //        //���� ��ƼĿ �߰�
    //        waffleList.Remove(obj);
    //        //�������� ���ش�.
    //        //UpdateToServer(obj);
    //    }
    //    else if (state == ItemType.Audio)
    //    {
    //        //�Ҹ����� ��ƼĿ �߰�
    //        audioList.Remove(obj);
    //        //�������� ���ش�.
    //        //UpdateToServer(obj);
    //    }
    //    else if (state == ItemType.Diary)
    //    {
    //        diaryList.Remove(obj);
    //        //�������� ���ش�.
    //        //UpdateToServer(obj);
    //    }
    //    //�������� ���ش�.
    //}

    ///// <summary>
    ///// Clear Before Add New Item into List
    ///// </summary>
    public void ClearInventory()
    {
        audioList.Clear();
        weatherList.Clear();
        handMadeList.Clear();
        waffleList.Clear();
        diaryList.Clear();
    }

    /* public void ShowInventory(ItemType state)
     {
         //����� �����۵��� �ҷ��� ������ ���Կ� ä���
     }

     public void UpdateToServer(Texture2D obj)
     {
         //��ƼĿ�� �߰�, ���� �� ��ȭ�������� ���� ������Ʈ���ִ��Լ�

         //��ƼĿ�� �߰�, ���� �� ��ȭ�������� ���� �Ŵ������� ����
     }
 */
}

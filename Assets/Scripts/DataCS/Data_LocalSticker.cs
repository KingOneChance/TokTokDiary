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

    //서버용
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
    //        //서버에도 저장
    //       // UpdateToServer(obj);
    //    }
    //    else if (state == ItemType.Handmade)
    //    {
    //        //직접만든 스티커 추가
    //        handMadeList.Remove(obj);
    //        //서버에도 저장
    //        //UpdateToServer(obj);
    //    }
    //    else if (state == ItemType.Waffle)
    //    {
    //        //와플 스티커 추가
    //        waffleList.Remove(obj);
    //        //서버에서 빼준다.
    //        //UpdateToServer(obj);
    //    }
    //    else if (state == ItemType.Audio)
    //    {
    //        //소리나는 스티커 추가
    //        audioList.Remove(obj);
    //        //서버에서 빼준다.
    //        //UpdateToServer(obj);
    //    }
    //    else if (state == ItemType.Diary)
    //    {
    //        diaryList.Remove(obj);
    //        //서버에서 빼준다.
    //        //UpdateToServer(obj);
    //    }
    //    //서버에서 빼준다.
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
         //저장된 아이템들을 불러와 아이템 슬롯에 채운다
     }

     public void UpdateToServer(Texture2D obj)
     {
         //스티커가 추가, 제거 등 변화가있을때 서버 업데이트해주는함수

         //스티커가 추가, 제거 등 변화가있을때 메인 매니저에게 전달
     }
 */
}

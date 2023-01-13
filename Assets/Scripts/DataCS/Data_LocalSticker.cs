using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    Weather,
    Handmade,
    Audio,
    Waffle,
    Diary
}
public class Data_LocalSticker : MonoBehaviour
{
    private ItemType type;

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
    //서버용
    public void GetServerData()
    {
        //메인 매니저에서 저장된 아이템들을 불러온다
        //데이터 for문을 돌아 type별 List에 넣어준다
        //for(int i = 0; i < 데이터개수; i++){
        //    }

    }
    public void AddInventory(Texture2D obj, ItemType state)
    {
        if (state == ItemType.Weather)
        {
            //날씨 스티커 추가
            weatherList.Add(obj);

            //서버에도 저장
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Handmade)
        {
            //직접만든 스티커 추가
            handMadeList.Add(obj);
            //서버에도 저장
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Waffle)
        {
            //와플 스티커 추가
            waffleList.Add(obj);
            Debug.Log("와플");
            //서버에도 저장
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Audio)
        {
            //소리나는 스티커 추가
            audioList.Add(obj);
            //서버에도 저장
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Diary)
        {
            //일기장 추가
            diaryList.Add(obj);
            //서버에 저장
           // UpdateToServer(obj);
        }
    }

    public void RemoveInventory(Texture2D obj, ItemType state)
    {
        //사용된 스티커들을 리스트에서 빼주고
        if (state == ItemType.Weather)
        {
            //날씨 스티커 추가
            weatherList.Remove(obj);
            //서버에도 저장
           // UpdateToServer(obj);
        }
        else if (state == ItemType.Handmade)
        {
            //직접만든 스티커 추가
            handMadeList.Remove(obj);
            //서버에도 저장
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Waffle)
        {
            //와플 스티커 추가
            waffleList.Remove(obj);
            //서버에서 빼준다.
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Audio)
        {
            //소리나는 스티커 추가
            audioList.Remove(obj);
            //서버에서 빼준다.
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Diary)
        {
            diaryList.Remove(obj);
            //서버에서 빼준다.
            //UpdateToServer(obj);
        }
        //서버에서 빼준다.
    }

    /// <summary>
    /// Clear Before Add New Item into List
    /// </summary>
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

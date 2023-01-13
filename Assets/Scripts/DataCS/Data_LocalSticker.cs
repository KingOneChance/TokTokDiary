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
    //������
    public void GetServerData()
    {
        //���� �Ŵ������� ����� �����۵��� �ҷ��´�
        //������ for���� ���� type�� List�� �־��ش�
        //for(int i = 0; i < �����Ͱ���; i++){
        //    }

    }
    public void AddInventory(Texture2D obj, ItemType state)
    {
        if (state == ItemType.Weather)
        {
            //���� ��ƼĿ �߰�
            weatherList.Add(obj);

            //�������� ����
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Handmade)
        {
            //�������� ��ƼĿ �߰�
            handMadeList.Add(obj);
            //�������� ����
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Waffle)
        {
            //���� ��ƼĿ �߰�
            waffleList.Add(obj);
            Debug.Log("����");
            //�������� ����
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Audio)
        {
            //�Ҹ����� ��ƼĿ �߰�
            audioList.Add(obj);
            //�������� ����
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Diary)
        {
            //�ϱ��� �߰�
            diaryList.Add(obj);
            //������ ����
           // UpdateToServer(obj);
        }
    }

    public void RemoveInventory(Texture2D obj, ItemType state)
    {
        //���� ��ƼĿ���� ����Ʈ���� ���ְ�
        if (state == ItemType.Weather)
        {
            //���� ��ƼĿ �߰�
            weatherList.Remove(obj);
            //�������� ����
           // UpdateToServer(obj);
        }
        else if (state == ItemType.Handmade)
        {
            //�������� ��ƼĿ �߰�
            handMadeList.Remove(obj);
            //�������� ����
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Waffle)
        {
            //���� ��ƼĿ �߰�
            waffleList.Remove(obj);
            //�������� ���ش�.
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Audio)
        {
            //�Ҹ����� ��ƼĿ �߰�
            audioList.Remove(obj);
            //�������� ���ش�.
            //UpdateToServer(obj);
        }
        else if (state == ItemType.Diary)
        {
            diaryList.Remove(obj);
            //�������� ���ش�.
            //UpdateToServer(obj);
        }
        //�������� ���ش�.
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
        //����� �����۵��� �ҷ��� ������ ���Կ� ä���
    }

    public void UpdateToServer(Texture2D obj)
    {
        //��ƼĿ�� �߰�, ���� �� ��ȭ�������� ���� ������Ʈ���ִ��Լ�

        //��ƼĿ�� �߰�, ���� �� ��ȭ�������� ���� �Ŵ������� ����
    }
*/
}

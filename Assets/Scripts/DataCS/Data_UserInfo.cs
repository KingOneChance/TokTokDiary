using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Weather,
    Handmade,
    Audio,
    Waffle,
    Diary
}
public class Data_UserInfo : MonoBehaviour
{
    private ItemType type;

    private List<GameObject> audioList = new List<GameObject>();
    private List<GameObject> weatherList = new List<GameObject>();
    private List<GameObject> handMadeList = new List<GameObject>();
    private List<GameObject> waffleList = new List<GameObject>();
    private List<GameObject> diaryList = new List<GameObject>();

    private void Start()
    {
        GetServerData();
    }
    //������
    public void GetServerData()
    {
        //���� �Ŵ������� ����� �����۵��� �ҷ��´�
        //������ for���� ���� type�� List�� �־��ش�
        //for(int i = 0; i < �����Ͱ���; i++){
        //    }

    }

    public void AddInventory(GameObject obj, ItemType state)
    {
        if (state == ItemType.Weather)
        {
            //���� ��ƼĿ �߰�
            weatherList.Add(obj);
            //�������� ����
            UpdateToServer(obj);
        }
        else if (state == ItemType.Handmade)
        {
            //�������� ��ƼĿ �߰�
            handMadeList.Add(obj);
            //�������� ����
            UpdateToServer(obj);
        }
        else if (state == ItemType.Waffle)
        {
            //���� ��ƼĿ �߰�
            waffleList.Add(obj);
            //�������� ����
            UpdateToServer(obj);
        }
        else if (state == ItemType.Audio)
        {
            //�Ҹ����� ��ƼĿ �߰�
            audioList.Add(obj);
            //�������� ����
            UpdateToServer(obj);
        }
        else if (state == ItemType.Diary)
        {
            //�ϱ��� �߰�
            diaryList.Add(obj);
            //������ ����
            UpdateToServer(obj);
        }
    }

    public void RemoveInventory(GameObject obj, ItemType state)
    {
        //���� ��ƼĿ���� ����Ʈ���� ���ְ�
        if (state == ItemType.Weather)
        {
            //���� ��ƼĿ �߰�
            weatherList.Remove(obj);
            //�������� ����
            UpdateToServer(obj);
        }
        else if (state == ItemType.Handmade)
        {
            //�������� ��ƼĿ �߰�
            handMadeList.Remove(obj);
            //�������� ����
            UpdateToServer(obj);
        }
        else if (state == ItemType.Waffle)
        {
            //���� ��ƼĿ �߰�
            waffleList.Remove(obj);
            //�������� ���ش�.
            UpdateToServer(obj);
        }
        else if (state == ItemType.Audio)
        {
            //�Ҹ����� ��ƼĿ �߰�
            audioList.Remove(obj);
            //�������� ���ش�.
            UpdateToServer(obj);
        }
        else if (state == ItemType.Diary)
        {
            diaryList.Remove(obj);
            //�������� ���ش�.
            UpdateToServer(obj);
        }
        //�������� ���ش�.
    }

    public void ShowInventory(ItemType state)
    {
        //����� �����۵��� �ҷ��� ������ ���Կ� ä���
    }

    public void UpdateToServer(GameObject obj)
    {
        //��ƼĿ�� �߰�, ���� �� ��ȭ�������� ���� ������Ʈ���ִ��Լ�

        //��ƼĿ�� �߰�, ���� �� ��ȭ�������� ���� �Ŵ������� ����
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_LinkedList : MonoBehaviour
{
    public Data_Node cur = null;
    public Data_Node head = null;
    public Data_Node tail = null;
    public SData_NodeData prevData;
    private Func_DIarySave func_SaveDiary = null;
    private Func_DiaryInventory func_DiaryInventory = null;
    public Data_LinkedList()
    {
        func_SaveDiary = FindObjectOfType<Func_DIarySave>();
        func_DiaryInventory = FindObjectOfType<Func_DiaryInventory>();
    }
    public void AddDragStart(SData_NodeData newData, GameObject myObj)
    {
        Data_Node newNode = new Data_Node();
        newNode.nodeData = newData;
        newNode.myObject = myObj;

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            newNode.prev = tail;
        }
        tail = newNode;
        prevData = newData;
    }
    public void AddInit(SData_NodeData newData, GameObject myObj)
    {
        Data_Node newNode = new Data_Node();
        newNode.nodeData = newData;
        newNode.myObject = myObj;
        newNode.isFirst = true;
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            newNode.prev = tail;
        }
        tail = newNode;
        prevData = newData;
    }

    public void BackDoor()
    {
        if (head == null) return;
        if (head == tail && head.isFirst == true) //리스트에 1개만 있고 생성된 오브젝트일때 삭제후 리턴
        {
            int num = 0;
            if (!tail.myObject.name.Contains("Line"))
            {
                num = int.Parse(tail.myObject.name.Split("(")[0]);
            }
            switch (tail.myObject.tag)
                {
                case "RecordSticker":
                    //제이슨에서 목록 지우기
                    Manager_Main.Instance.func_DiaryToJson.DeleteListNumber(num);
                    //다이어리세이브에서 지우기
                    func_SaveDiary.RemoveRecordAtList(num);
                    func_DiaryInventory.OnClick_RecordFileRepository();
                    break;
                case "FreeSticker":
                    func_SaveDiary.RemoveFreeAtList(num);
                    func_DiaryInventory.OnClick_BubbleFreeStickerRepository();
                    break;
                case "GunSticker":
                    func_SaveDiary.RemoveGunAtList(num);
                    func_DiaryInventory.OnClick_BubbleGunStickerRepository();
                    break;
                case "BubbleSticker":
                    func_SaveDiary.RemoveBubbleAtList(num);
                    func_DiaryInventory.OnClick_BubbleStickerRepository();
                    break;
                default:
                    break;
            }
            Manager_Main.Instance.manager_PictureDiary.DestroySomthing(head.myObject);
            head = null;
            tail = null;
            return;
        }
        else if (tail == head)
        {
            head = null;
            tail = null;
            return;
        }

        if (tail.isFirst == false)//처음 생긴 오브젝트인지 확인 
        {
            //이전과 같은 오브젝트인경우  (트랜스폼 변경)
            if (tail.myObject == tail.prev.myObject && tail.nodeData.position != tail.prev.nodeData.position)
            {
                RectTransform temp = tail.myObject.GetComponent<RectTransform>();
                temp.rotation = Quaternion.Euler(tail.prev.nodeData.rotation);
                temp.position = tail.prev.nodeData.position;
                temp.localScale = tail.prev.nodeData.scale;

                tail = tail.prev;
            }
            else if (tail.myObject == tail.prev.myObject && tail.nodeData.position == tail.prev.nodeData.position)
            {
                tail = tail.prev;
                if (tail.isFirst == true) Manager_Main.Instance.manager_PictureDiary.DestroySomthing(tail.myObject);
                else if (tail != head)
                {
                    RectTransform temp = tail.myObject.GetComponent<RectTransform>();
                    temp.rotation = Quaternion.Euler(tail.prev.nodeData.rotation);
                    temp.position = tail.prev.nodeData.position;
                    temp.localScale = tail.prev.nodeData.scale;
                }
            }
            //이전과 다른 오브젝트인 경우  (트랜스폼 변경)
            else if (tail.myObject != tail.prev.myObject && tail.prev.isFirst == false)
            {
                tail = tail.prev;
                if (tail != head)
                {
                    RectTransform temp = tail.myObject.GetComponent<RectTransform>();
                    temp.rotation = Quaternion.Euler(tail.prev.nodeData.rotation);
                    temp.position = tail.prev.nodeData.position;
                    temp.localScale = tail.prev.nodeData.scale;
                }
            }
            else if (tail.myObject != tail.prev.myObject && tail.prev.isFirst == true)
            {
                RectTransform temp = tail.myObject.GetComponent<RectTransform>();
                temp.rotation = Quaternion.Euler(tail.prev.nodeData.rotation);
                temp.position = tail.prev.nodeData.position;
                temp.localScale = tail.prev.nodeData.scale;
            }
        }
        else  //처음 생긴 오브젝트일 경우
        {
            int num = 0;
            if (!tail.myObject.name.Contains("Line"))
            {
                num = int.Parse(tail.myObject.name.Split("(")[0]);
            }

            switch (tail.myObject.tag)
            {
                case "RecordSticker":
                    //제이슨에서 목록 지우기
                    Manager_Main.Instance.func_DiaryToJson.DeleteListNumber(num);
                    //다이어리세이브에서 지우기
                    func_SaveDiary.RemoveRecordAtList(num);
                    func_DiaryInventory.OnClick_RecordFileRepository();
                    break;
                case "FreeSticker":
                    func_SaveDiary.RemoveFreeAtList(num);
                    func_DiaryInventory.OnClick_BubbleFreeStickerRepository();
                    break;
                case "GunSticker":
                    func_SaveDiary.RemoveGunAtList(num);
                    func_DiaryInventory.OnClick_BubbleGunStickerRepository();
                    break;
                case "BubbleSticker":
                    func_SaveDiary.RemoveBubbleAtList(num);
                    func_DiaryInventory.OnClick_BubbleStickerRepository();
                    break;
                default:
                    break;
            }
            Manager_Main.Instance.manager_PictureDiary.DestroySomthing(tail.myObject);
        }
        if (tail == head)
        {
            head = null;
            tail = null;
            return;
        }
        tail = tail.prev;
    }

    public void Print()
    {
        if (tail != null)
        {
            // cur 를 처음 노드(head)를 가리키게 하고
            cur = tail;
            // 다음 노드가 존재한다면 계속 반복
            do
            {
                cur = cur.prev;
            } while (cur != head);
        }
    }
}

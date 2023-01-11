using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_LinkedList : MonoBehaviour
{
    public Data_Node cur = null;
    public Data_Node head = null;
    public Data_Node tail = null;
    public SData_NodeData prevData;

    public void AddDragStart(SData_NodeData newData, GameObject myObj)
    {
        Data_Node newNode = new Data_Node();
        newNode.nodeData = newData;
        newNode.myObject = myObj;

        if (head == null)
        {
            head = newNode;
            //     Debug.Log("헤드 : " + head.myObject.name);
        }
        else
        {
            newNode.prev = tail;
            //      Debug.Log("드래그 비긴으로 추가된 오브젝트 : " + tail.myObject.name);
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
            //     Debug.Log("헤드 : " + head.myObject.name);
        }
        else
        {
            newNode.prev = tail;
            //      Debug.Log("드래그 비긴으로 추가된 오브젝트 : " + tail.myObject.name);
        }
        tail = newNode;
        prevData = newData;
    }

    public void BackDoor()
    {
        if (head == null) return;
        if (head == tail && head.isFirst == true)
        {
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
            if (tail.myObject == tail.prev.myObject && tail.nodeData.position != tail.prev.nodeData.position) //이전과 같은 오브젝트인경우  (트랜스폼 변경)
            {
                RectTransform temp = tail.myObject.GetComponent<RectTransform>();
                temp.rotation = Quaternion.Euler(tail.prev.nodeData.rotation);
                temp.position = tail.prev.nodeData.position;
                temp.localScale = tail.prev.nodeData.scale;

                tail = tail.prev;
                // Debug.Log("자리이동 + 처음?" + tail.isFirst+tail.myObject.name);
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
            else if (tail.myObject != tail.prev.myObject && tail.prev.isFirst == false)//이전과 다른 오브젝트인 경우  (트랜스폼 변경)
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
            Manager_Main.Instance.manager_PictureDiary.DestroySomthing(tail.myObject);
            Debug.Log("파괴 : " + tail.myObject.name);
        }
        Debug.Log("지운이름 : " + tail.myObject.name);
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
        if (tail == null)
            Debug.Log("자료가 없어요");
        else
        {
            // cur 를 처음 노드(head)를 가리키게 하고
            cur = tail;
            // 다음 노드가 존재한다면 계속 반복
            do
            {
                Debug.Log("현재 오브젝트 이름" + cur.myObject.name);
                Debug.Log("현재 오브젝트 이름" + cur.prev.myObject.name);
                Debug.Log("현재위치 : " + cur.nodeData.position);
                Debug.Log("이전위치 : " + cur.prev.nodeData.position);
                cur = cur.prev;
            } while (cur != head);
        }
    }
}

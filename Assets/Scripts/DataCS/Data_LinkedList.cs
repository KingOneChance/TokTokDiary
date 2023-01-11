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
            //     Debug.Log("��� : " + head.myObject.name);
        }
        else
        {
            newNode.prev = tail;
            //      Debug.Log("�巡�� ������� �߰��� ������Ʈ : " + tail.myObject.name);
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
            //     Debug.Log("��� : " + head.myObject.name);
        }
        else
        {
            newNode.prev = tail;
            //      Debug.Log("�巡�� ������� �߰��� ������Ʈ : " + tail.myObject.name);
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

        if (tail.isFirst == false)//ó�� ���� ������Ʈ���� Ȯ�� 
        {
            if (tail.myObject == tail.prev.myObject && tail.nodeData.position != tail.prev.nodeData.position) //������ ���� ������Ʈ�ΰ��  (Ʈ������ ����)
            {
                RectTransform temp = tail.myObject.GetComponent<RectTransform>();
                temp.rotation = Quaternion.Euler(tail.prev.nodeData.rotation);
                temp.position = tail.prev.nodeData.position;
                temp.localScale = tail.prev.nodeData.scale;

                tail = tail.prev;
                // Debug.Log("�ڸ��̵� + ó��?" + tail.isFirst+tail.myObject.name);
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
            else if (tail.myObject != tail.prev.myObject && tail.prev.isFirst == false)//������ �ٸ� ������Ʈ�� ���  (Ʈ������ ����)
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
        else  //ó�� ���� ������Ʈ�� ���
        {
            Manager_Main.Instance.manager_PictureDiary.DestroySomthing(tail.myObject);
            Debug.Log("�ı� : " + tail.myObject.name);
        }
        Debug.Log("�����̸� : " + tail.myObject.name);
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
            Debug.Log("�ڷᰡ �����");
        else
        {
            // cur �� ó�� ���(head)�� ����Ű�� �ϰ�
            cur = tail;
            // ���� ��尡 �����Ѵٸ� ��� �ݺ�
            do
            {
                Debug.Log("���� ������Ʈ �̸�" + cur.myObject.name);
                Debug.Log("���� ������Ʈ �̸�" + cur.prev.myObject.name);
                Debug.Log("������ġ : " + cur.nodeData.position);
                Debug.Log("������ġ : " + cur.prev.nodeData.position);
                cur = cur.prev;
            } while (cur != head);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Data_Node
{
    public Data_Node prev;
    public Data_Node next;
    public GameObject myObject;
    public Texture2D texture;
    public SData_NodeData nodeData;
    //���� ���� ������Ʈ�� ���
    public bool isFirst = false;
}

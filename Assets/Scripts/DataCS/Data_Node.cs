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
    //최초 생긴 오브젝트일 경우
    public bool isFirst = false;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Manager_PictureDiary : MonoBehaviour
{
    public Data_LinkedList conZ = new Data_LinkedList();


    #region These functions are for the purpose of controlling some specific behaviors. (like Control + Z)
    public void AddDragBegin(SData_NodeData nodata, GameObject gameObject) => conZ.AddDragStart(nodata, gameObject);
    public void AddDragInit(SData_NodeData nodata, GameObject gameObject) => conZ.AddInit(nodata, gameObject);
    public void AddDrangEnd(SData_NodeData nodata, GameObject gameObject) => conZ.AddDragStart(nodata, gameObject);
    public void OnClick_GoBack() => conZ.BackDoor();
    public void OnClick_Print() => conZ.Print();

    //되돌리기에서 오브젝트 삭제하기 위한 함수 
    public void DestroySomthing(GameObject gameObject) => Destroy(gameObject);
    #endregion
}
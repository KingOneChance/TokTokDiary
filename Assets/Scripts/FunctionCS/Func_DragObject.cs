using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FreeDraw;

public class Func_DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private SData_NodeData nodeData;
    private RectTransform rect;
    private Vector2 mousePos = Vector2.zero;
    [SerializeField] private Func_TodayFeelingImage drawObject = null;
    Func_HelperGuideDrag func_HelperGuideDrag;
    private void Start()
    {
        drawObject=FindObjectOfType<Func_TodayFeelingImage>();
        rect = GetComponent<RectTransform>();
      

    }
    public void RecordToJson()
    {
        //���ڵ� ��ƼĿ�� �ִٸ� (����)
        //���̽����� ���� ��ġ�� ���ڵ� ���� �̸� ������
        //���̾������ ��ü ���� �ʿ�
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (func_HelperGuideDrag.canDrag == true) return;
        drawObject.IsStickerMaking(true);//��ƼĿ �巡�׽� �������°� ����
        nodeData = new SData_NodeData();
        nodeData.position = rect.position;
        nodeData.rotation = rect.rotation.eulerAngles;
        nodeData.scale = rect.localScale;
        Manager_Main.Instance.manager_PictureDiary.AddDragBegin(nodeData, gameObject);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (func_HelperGuideDrag.canDrag == true) return;
        //rect.transform.position = eventData.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = mousePos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (func_HelperGuideDrag.canDrag == true) return;
        nodeData = new SData_NodeData();
        nodeData.position = rect.position;
        nodeData.rotation = rect.rotation.eulerAngles;
        nodeData.scale = rect.localScale;
        Manager_Main.Instance.manager_PictureDiary.AddDrangEnd(nodeData, gameObject);
        drawObject.IsStickerMaking(false);//��ƼĿ �巡�׽� �������°� ����
    }
}


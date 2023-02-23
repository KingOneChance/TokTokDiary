using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FreeDraw;

public class Func_HelperGuideDrag : MonoBehaviour
{
    [SerializeField] private GameObject DragLine = null;
    [SerializeField] private RectTransform 손가락 = null;
    [SerializeField] private RectTransform 출발지 = null;
    [SerializeField] private RectTransform 도착지 = null;
    [SerializeField] private GameObject Select1 = null;
    [SerializeField] private GameObject Select2 = null;
    [SerializeField] private GameObject Select3 = null;
    [SerializeField] private GameObject Select4 = null;
    [SerializeField] private GameObject HelperMenu = null;
    [SerializeField] private GameObject StopButton = null;
    [SerializeField] private Func_Draw DrawingObj;
    [SerializeField] private Func_Tutorial Tutorial;
    [SerializeField] private Func_TodayFeelingImage drawingFun = null;
   // public Func_StickersCopy[] Copyobjects;
    bool TurnPopWindows = false;

     public bool canDrag = false;
    

    void Start()
    {
       // Copyobjects = FindObjectsOfType<Func_StickersCopy>();

    }
    public void OnClick_Select1()
    {
     //   Tutorial.Obj_Smallar();
    //    DrawingObj.StopDraw(true);
        Select1.SetActive(true);
        Select2.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
        HelperMenu.SetActive(false);
        손가락.position = 출발지.position;
        StartCoroutine(Select_1());
        DragLine.SetActive(true);
        손가락.gameObject.SetActive(true); 
    }

    public void OnClick_Select2()
    {
    //    DrawingObj.StopDraw(true);
        Select2.SetActive(true);
        Select1.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
        HelperMenu.SetActive(false);
        손가락.position = 출발지.position;
        StartCoroutine(Select_2());
        손가락.gameObject.SetActive(true);
    }

    public void OnClick_Select3()
    {
    //    DrawingObj.StopDraw(true);
        Select3.SetActive(true);
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select4.SetActive(false);
        HelperMenu.SetActive(false);
        손가락.position = 출발지.position;
        StartCoroutine(Select_3());
        손가락.gameObject.SetActive(true);
    }

    public void OnClick_Select4()
    {
     //   DrawingObj.StopDraw(true);
        Select4.SetActive(true);
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(false);
        HelperMenu.SetActive(false);
        손가락.position = 출발지.position;
        StartCoroutine(Select_4());
        DragLine.SetActive(true);
        손가락.gameObject.SetActive(true);
    }

    public void OnClick_Stop()
    {
        canDrag = false;
        drawingFun.enabled = true;
     //   for (int i = 0; i < Copyobjects.Length; i++)
     //   {
     //       Copyobjects[i].enabled = true;
     //   }
    //    DrawingObj.StopDraw(true);
        StopAllCoroutines();
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
        TurnPopWindows = true;
        손가락.position = 출발지.position;
        DragLine.SetActive(false);
        손가락.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
        
        if (TurnPopWindows == true)
        {
            HelperMenu.SetActive(false);
            StopButton.SetActive(false);
        }

    }

    public void OnClick_HelpMenu()
    {
        canDrag = true;
        drawingFun.enabled = false;
       // DrawingObj.StopDraw(true);
      //  for (int i = 0; i < Copyobjects.Length; i++)
      //  {
      //      Copyobjects[i].enabled = false;
      //  }
        HelperMenu.SetActive(true);
        StopButton.SetActive(true);
        Debug.Log("헬프메뉴켜짐?");

      // if (!HelperMenu.activeSelf)
      // {
      //     HelperMenu.SetActive(false);
      //     StopButton.SetActive(false);
      //     DrawingObj.StopDraw(false);
      //     Debug.Log("꺼짐?");
      // }
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Select_1()
    {
        DrawingObj.StopDraw(true);
        손가락.position = 출발지.position;
        Debug.Log(손가락.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while( i<  50 )
        {
            손가락.position = new Vector2(손가락.position.x + 0.1f, 손가락.position.y);
            yield return time;
            i++;
        }
        DragLine.SetActive(false);
        손가락.gameObject.SetActive(false);
        HelperMenu.SetActive(true);

        StopCoroutine("Select_1");
    }

    IEnumerator Select_2()
    {
        DrawingObj.StopDraw(true);
        손가락.position = 출발지.position;
        Debug.Log(손가락.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while (i < 50)
        {
            손가락.position = new Vector2(손가락.position.x + 0.1f, 손가락.position.y + 0.1f);
            yield return time;
            i++;
        }
        // transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.7f, 0.7f), 0.1f);
        손가락.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }

    IEnumerator Select_3()
    {
        DrawingObj.StopDraw(true);
        손가락.position = 출발지.position;
        Debug.Log(손가락.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while (i < 50)
        {
            손가락.position = new Vector2(손가락.position.x + 0.1f, 손가락.position.y + 0.08f);
            yield return time;
            i++;
        }
        손가락.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }

    IEnumerator Select_4()
    {
        DrawingObj.StopDraw(true);
        손가락.position = 출발지.position;
        Debug.Log(손가락.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while (i < 50)
        {
            손가락.position = new Vector2(손가락.position.x - 0.1f, 손가락.position.y);
            yield return time;
            i++;
        }
        DragLine.SetActive(false);
        손가락.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }
}

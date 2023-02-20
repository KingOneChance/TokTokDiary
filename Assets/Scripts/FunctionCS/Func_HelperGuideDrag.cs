using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_HelperGuideDrag : MonoBehaviour
{
    [SerializeField] private GameObject Line = null;
    [SerializeField] private RectTransform 손가락 = null;
    [SerializeField] private RectTransform 출발지 = null;
    [SerializeField] private RectTransform 도착지 = null;
    [SerializeField] private GameObject Select1 = null;
    [SerializeField] private GameObject Select2 = null;
    [SerializeField] private GameObject Select3 = null;
    [SerializeField] private GameObject Select4 = null;
    [SerializeField] private GameObject HelperMenu = null;
    [SerializeField] private GameObject StopButton = null;
    bool TurnPopWindows = false;

    public void OnClick_Select1()
    {
        Select1.SetActive(true);
        Select2.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
        HelperMenu.SetActive(false);
        손가락.position = 출발지.position;
        StartCoroutine(Select_1());
        Line.SetActive(true);
        손가락.gameObject.SetActive(true);
    }

    public void OnClick_Select2()
    {
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
        Select4.SetActive(true);
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(false);
        HelperMenu.SetActive(false);
        손가락.position = 출발지.position;
        StartCoroutine(Select_4());
        Line.SetActive(true);
        손가락.gameObject.SetActive(true);
    }

    public void OnClick_Stop()
    {
        StopAllCoroutines();
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
        TurnPopWindows = true;
        손가락.position = 출발지.position;
        Line.SetActive(false);
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
        HelperMenu.SetActive(true);
        StopButton.SetActive(true);
        Debug.Log("헬프메뉴켜짐?");

        if (!HelperMenu.activeSelf)
        {
            HelperMenu.SetActive(false);
            StopButton.SetActive(false);
            Debug.Log("꺼짐?");
        }
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Select_1()
    {
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
        Line.SetActive(false);
        손가락.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }

    IEnumerator Select_2()
    {
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
        Line.SetActive(false);
        손가락.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }
}

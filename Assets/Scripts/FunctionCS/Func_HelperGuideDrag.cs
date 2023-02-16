using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_HelperGuideDrag : MonoBehaviour
{
    [SerializeField] private GameObject Line = null;
    [SerializeField] private RectTransform �հ��� = null;
    [SerializeField] private RectTransform ����� = null;
    [SerializeField] private RectTransform ������ = null;
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
        �հ���.position = �����.position;
        StartCoroutine(Select_1());
        Line.SetActive(true);
        �հ���.gameObject.SetActive(true);
    }

    public void OnClick_Select2()
    {
        Select2.SetActive(true);
        Select1.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
        HelperMenu.SetActive(false);
        �հ���.position = �����.position;
        StartCoroutine(Select_2());
        �հ���.gameObject.SetActive(true);
    }

    public void OnClick_Select3()
    {
        Select3.SetActive(true);
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select4.SetActive(false);
        HelperMenu.SetActive(false);
        �հ���.position = �����.position;
        StartCoroutine(Select_3());
        �հ���.gameObject.SetActive(true);
    }

    public void OnClick_Select4()
    {
        Select4.SetActive(true);
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(false);
        HelperMenu.SetActive(false);
        �հ���.position = �����.position;
        StartCoroutine(Select_4());
        Line.SetActive(true);
        �հ���.gameObject.SetActive(true);
    }

    public void OnClick_Stop()
    {
        StopAllCoroutines();
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
        TurnPopWindows = true;
        �հ���.position = �����.position;
        Line.SetActive(false);
        �հ���.gameObject.SetActive(false);
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
        Debug.Log("�����޴�����?");

        if (!HelperMenu.activeSelf)
        {
            HelperMenu.SetActive(false);
            StopButton.SetActive(false);
            Debug.Log("����?");
        }
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Select_1()
    {
        �հ���.position = �����.position;
        Debug.Log(�հ���.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while( i<  50 )
        {
            �հ���.position = new Vector2(�հ���.position.x + 0.1f, �հ���.position.y);
            yield return time;
            i++;
        }
        Line.SetActive(false);
        �հ���.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }

    IEnumerator Select_2()
    {
        �հ���.position = �����.position;
        Debug.Log(�հ���.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while (i < 50)
        {
            �հ���.position = new Vector2(�հ���.position.x + 0.1f, �հ���.position.y + 0.1f);
            yield return time;
            i++;
        }
        // transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.7f, 0.7f), 0.1f);
        �հ���.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }

    IEnumerator Select_3()
    {
        �հ���.position = �����.position;
        Debug.Log(�հ���.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while (i < 50)
        {
            �հ���.position = new Vector2(�հ���.position.x + 0.1f, �հ���.position.y + 0.08f);
            yield return time;
            i++;
        }
        �հ���.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }

    IEnumerator Select_4()
    {
        �հ���.position = �����.position;
        Debug.Log(�հ���.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while (i < 50)
        {
            �հ���.position = new Vector2(�հ���.position.x - 0.1f, �հ���.position.y);
            yield return time;
            i++;
        }
        Line.SetActive(false);
        �հ���.gameObject.SetActive(false);
        HelperMenu.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_GuideDrag : MonoBehaviour
{
    [SerializeField] private GameObject Line = null;
    [SerializeField] private RectTransform �հ��� = null;
    [SerializeField] private RectTransform ����� = null;
    [SerializeField] private RectTransform ������ = null;
    [SerializeField] private float �հ����̵��ӵ� = 0f;
    [SerializeField] private int runCount = 0;

    private void OnEnable()
    {
        �հ���.position = �����.position;
        StartCoroutine(Co_GuideDrag(runCount));
        Line.SetActive(true);
        �հ���.gameObject.SetActive(true);
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_GuideDrag(int runCnt)
    {
        �հ���.position = �����.position;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < runCnt; i++)
        {
            while (�հ���.position.x > ������.position.x)//i<  50 )
            {
                �հ���.position = new Vector2(�հ���.position.x - 0.1f * �հ����̵��ӵ�, �հ���.position.y);
                yield return time;
            }
            �հ���.position = �����.position;
        }
        Line.SetActive(false);
        �հ���.gameObject.SetActive(false);
    }
}

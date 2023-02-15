using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_GuideDrag : MonoBehaviour
{
    [SerializeField] private GameObject Line = null;
    [SerializeField] private RectTransform 손가락 = null;
    [SerializeField] private RectTransform 출발지 = null;
    [SerializeField] private RectTransform 도착지 = null;
    [SerializeField] private float 손가락이동속도 = 0f;
    [SerializeField] private int runCount = 0;

    private void OnEnable()
    {
        손가락.position = 출발지.position;
        StartCoroutine(Co_GuideDrag(runCount));
        Line.SetActive(true);
        손가락.gameObject.SetActive(true);
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_GuideDrag(int runCnt)
    {
        손가락.position = 출발지.position;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < runCnt; i++)
        {
            while (손가락.position.x > 도착지.position.x)//i<  50 )
            {
                손가락.position = new Vector2(손가락.position.x - 0.1f * 손가락이동속도, 손가락.position.y);
                yield return time;
            }
            손가락.position = 출발지.position;
        }
        Line.SetActive(false);
        손가락.gameObject.SetActive(false);
    }
}

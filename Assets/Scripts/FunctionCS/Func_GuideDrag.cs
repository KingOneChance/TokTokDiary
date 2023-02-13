using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_GuideDrag : MonoBehaviour
{
    [SerializeField] private GameObject Line = null;
    [SerializeField] private RectTransform 손가락 = null;
    [SerializeField] private RectTransform 출발지 = null;
    [SerializeField] private RectTransform 도착지 = null;


    private void OnEnable()
    {
        손가락.position = 출발지.position;
        StartCoroutine(Co_GuideDrag());
        Line.SetActive(true);
        손가락.gameObject.SetActive(true);
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_GuideDrag()
    {
        손가락.position = 출발지.position;
        Debug.Log(손가락.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while( i<  50 )
        {
            
            손가락.position = new Vector2(손가락.position.x - 0.1f, 손가락.position.y);
            yield return time;
            i++;
        }
        Line.SetActive(false);
        손가락.gameObject.SetActive(false);
    }
}

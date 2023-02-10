using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_GuideDrag : MonoBehaviour
{
    [SerializeField] private GameObject Line = null;
    [SerializeField] private RectTransform �հ��� = null;
    [SerializeField] private RectTransform ����� = null;
    [SerializeField] private RectTransform ������ = null;


    private void OnEnable()
    {
        �հ���.position = �����.position;
        StartCoroutine(Co_GuideDrag());
        Line.SetActive(true);
        �հ���.gameObject.SetActive(true);
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_GuideDrag()
    {
        �հ���.position = �����.position;
        Debug.Log(�հ���.transform.position);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while( i<  50 )
        {
            
            �հ���.position = new Vector2(�հ���.position.x - 0.1f, �հ���.position.y);
            yield return time;
            i++;
        }
        Line.SetActive(false);
        �հ���.gameObject.SetActive(false);
    }
}

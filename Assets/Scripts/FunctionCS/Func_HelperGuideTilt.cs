using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_HelperGuideTilt : MonoBehaviour
{
    [SerializeField] private GameObject wayOutGroup = null;
    [SerializeField] private GameObject guideObj = null;
    [SerializeField] private RectTransform guideRect = null;
    [SerializeField] private List<RectTransform> wayOutPoints = null;
    [SerializeField] private GameObject explain = null;
    private WaitForFixedUpdate delay = new WaitForFixedUpdate();
    private WaitForSeconds time = new WaitForSeconds(0.03f);

    private void Awake()
    {
        wayOutPoints = new List<RectTransform>();
        for (int i = 0; i < wayOutGroup.transform.childCount; ++i)
        {
            wayOutPoints.Add(wayOutGroup.transform.GetChild(i).GetComponent<RectTransform>());
        }
    }

    private void OnEnable()
    {
        explain.SetActive(true);
        StartCoroutine(MoveGuide());
    }

    private IEnumerator MoveGuide()
    {
        for (int count = 0; count < 2; count++)
        {
            for (int i = 0; i < wayOutPoints.Count; i++)
            {
                guideRect.position = wayOutPoints[i].position;
                yield return time;
            }
            guideRect.position = wayOutPoints[0].position;
        }
        guideObj.SetActive(false);
    }
}

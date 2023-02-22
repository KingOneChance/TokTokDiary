using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_bathLoadingSpread : MonoBehaviour
{
    [SerializeField] RectTransform myRect = null;
    private Vector2 myRandomDir = Vector2.zero;
    private WaitForFixedUpdate deltaTime = new WaitForFixedUpdate();
    private WaitForSeconds delay = new WaitForSeconds(0.3f);

    private void Start()
    {
        myRect = GetComponent<RectTransform>();
        myRandomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        StartCoroutine(Co_Move());
    }

    private IEnumerator Co_Move()
    {
        float moveTime = 0f;
        while (moveTime < 1f)
        {
            moveTime += Time.fixedDeltaTime;
            myRect.Translate(myRandomDir * 6f);
            yield return deltaTime;
        }
        InitMyRect();
    }

    private void InitMyRect()
    {
        myRect.localPosition = Vector3.zero;
        StartCoroutine(Co_Move());
    }
}

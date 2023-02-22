using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_labLoadingSpread : MonoBehaviour
{
    [SerializeField] RectTransform myRect = null;
    private Vector2 myRandomDir = Vector2.zero;
    private WaitForFixedUpdate deltaTime = new WaitForFixedUpdate();
    private float speed = 1f;

    private void Start()
    {
        speed = Random.Range(1f, 2f);
        myRect = GetComponent<RectTransform>();
        myRandomDir = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(Mathf.Tan(40 * (Mathf.PI / 180)), 1f)).normalized;
        StartCoroutine(Co_Move());
    }

    private IEnumerator Co_Move()
    {
        float moveTime = 0f;
        while (moveTime < 5f)
        {
            moveTime += Time.fixedDeltaTime;
            myRect.Translate(myRandomDir * 6f * speed);
            yield return deltaTime;
        }
    }
}

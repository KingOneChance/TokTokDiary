using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_RandomMoveBubble : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;

    private float speed = 300;
    float startTime;
    float distance;
 
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(Random.Range(0, 960), Random.Range(0, 540), 0);

        startTime = Time.time;
        distance = Vector3.Distance(startPos, endPos);
        Debug.Log(endPos);
    }

    // Update is called once per frame
    void Update()
    {
        float dc = (Time.time - startTime) * speed;
        float fj = dc / distance;
        transform.position = Vector3.Lerp(startPos, endPos, fj);
    }

}

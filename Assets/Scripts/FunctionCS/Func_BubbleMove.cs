using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_BubbleMove : MonoBehaviour
{
    public float speed = 20f;
    private Vector2 direction;
    private float screenWidth, screenHeight;

    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        direction = Random.insideUnitCircle.normalized;
        transform.position = new Vector3(Random.Range(0, screenWidth), Random.Range(0, screenHeight), 0f);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * Time.deltaTime * speed);

        if (transform.position.x < 0 || transform.position.x > screenWidth ||
            transform.position.y < 0 || transform.position.y > screenHeight)
        {
            direction = -direction;
        }
    }
}

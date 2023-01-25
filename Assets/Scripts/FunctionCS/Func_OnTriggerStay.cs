using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_OnTriggerStay : MonoBehaviour
{
    private float flowTime = 0;
    private Color mat;

    private void Start()
    {
        mat = new Color(0, 0, 0,0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Image")
        {

            flowTime += Time.deltaTime;
            if (flowTime > 2.0f)
            {
                flowTime = 0;
                Debug.Log("Æã");
                collision.GetComponent<Image>().color = mat;
            }
        }
        else
        {
            flowTime = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Image")
        {
            flowTime = 0;
        }
    }
}

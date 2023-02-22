using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;

public class Func_Tutorial : MonoBehaviour
{
    private float time;

    public void Obj_Smallar()
    {
        transform.localScale = Vector3.one * (1 - time);
        if (time > 1f)
        {
            time = 0;
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }
}

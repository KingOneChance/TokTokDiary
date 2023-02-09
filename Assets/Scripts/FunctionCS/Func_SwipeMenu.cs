using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Func_SwipeMenu : MonoBehaviour
{
    [SerializeField] private float[] pos;
    [SerializeField] private Scrollbar scroll;
    

    void Update()
    {
        
        pos = new float[transform.childCount];
        float distacne = 1f / (pos.Length - 1);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distacne * i;
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll.value < pos[i] + (distacne / 2) && scroll.value > pos[i] - (distacne / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.7f, 0.7f), 0.1f);
                    }
                }
            }
        }
    }
}

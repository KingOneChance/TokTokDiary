using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Func_SwipeMenu : MonoBehaviour
{
    [SerializeField] private float[] pos;
    [SerializeField] private Scrollbar scroll;
    [SerializeField] Func_ButtonEnabled[] childButtons = null;

    private Vector2 bigScale = new Vector2(1f, 1f);
    private Vector2 smallScale = new Vector2(0.7f, 0.7f);
    private float distacne = 0f;

    private void Start()
    {
        pos = new float[transform.childCount];
        distacne = 1f / (pos.Length - 1);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distacne * i;
            childButtons[i].enabled = false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll.value < pos[i] + (distacne / 2) && scroll.value > pos[i] - (distacne / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, bigScale, 0.1f);
                childButtons[i].enabled = true;

                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        childButtons[j].enabled = false;
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, smallScale, 0.1f);
                    }
                }
            }
        }
    }
}

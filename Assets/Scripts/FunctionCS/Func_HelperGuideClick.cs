using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_HelperGuideClick : MonoBehaviour
{
    [SerializeField] private RectTransform hand = null;

    private void OnEnable()
    {
        hand.gameObject.SetActive(true);
        StartCoroutine(Co_Poping());
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_Poping()
    {
        int i = 0;
        while (i < 60)
        {
            if (i < 10 || (20 <= i && i < 30) || (40 <= i && i < 50))
                hand.localScale = new Vector2(hand.localScale.x + 0.1f, hand.localScale.y + 0.1f);
            else if (10 <= i && i < 20 || (30 <= i && i < 40) || (50 <= i && i < 60))
                hand.localScale = new Vector2(hand.localScale.x - 0.1f, hand.localScale.y - 0.1f);

            yield return time;
            i++;
        }
        hand.gameObject.SetActive(false);
    }
}

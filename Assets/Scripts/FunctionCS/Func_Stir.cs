using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_Stir : MonoBehaviour
{
    [SerializeField] private float radius = 0;
    [SerializeField] private float deg = 0;
    [SerializeField] private float stirSpeed = 0;
    [SerializeField] private RectTransform myRect = null;

    [SerializeField] private Button stickButton = null;

    [SerializeField] private int stirCount = 0;

    public void OnClick_BubbleStick() => StartCoroutine(Stir());

    private void OnDisable()
    {
        stirCount = 0;
    }

    private IEnumerator Stir()
    {
        stickButton.enabled = false;
        while (true)
        {
            deg -= Time.deltaTime * stirSpeed;
            if (deg > -360)
            {
                float rad = Mathf.Deg2Rad * (deg);
                float x = radius * Mathf.Sin(rad);
                float y = radius * Mathf.Cos(rad);
                myRect.transform.position = transform.position + new Vector3(x, y);
            }
            else
            {
                deg = 0;
                stickButton.enabled = true;
                stirCount++;
                yield break;
            }
            yield return null;
        }
    }
}

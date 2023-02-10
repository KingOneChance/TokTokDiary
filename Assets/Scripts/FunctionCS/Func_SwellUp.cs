using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_SwellUp : MonoBehaviour
{
    [SerializeField] RawImage SwellUpImg = null;
    [SerializeField] Button SwellUpButton = null;
    [SerializeField] private Button NextButton = null;
    [SerializeField] private Button SkipButton = null;
    

    private bool isProgress = false;

    private int curIdx = 0;

    private void OnEnable()
    {
        isProgress = false;
        SkipButton.gameObject.SetActive(true);
        NextButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        curIdx = 0;
        SwellUpImg.rectTransform.localScale = Vector3.zero;
        SwellUpButton.interactable = true;
    }

    public void OnClick_SwellUp()
    {
        if(curIdx == 3)
        {
            Invoke(nameof(Bomb), 3f);
            return;
        }
        curIdx++;
        StartCoroutine(SwellUp());
    }

    private void Bomb()
    {
        Debug.Log("ลอมü");
        SwellUpImg.rectTransform.localScale = Vector3.zero;
        SwellUpButton.interactable = false;
    }

    private IEnumerator SwellUp()
    {
        SwellUpButton.interactable = false;
        while (true)
        {
            if (SwellUpImg.rectTransform.localScale.x >= curIdx)
            {
                SwellUpButton.interactable = true;
                yield break;
            }
            SwellUpImg.rectTransform.localScale += 0.02f * curIdx * Vector3.one;
            yield return null;
        }
    }

    public void OnClick_SkipButton()
    {
        Bomb();
        SwellUpButton.interactable = false;
    }
}

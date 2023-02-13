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
    [SerializeField] private Manager_BubbleSticker manager_bs;
    [SerializeField] private GameObject GetBubbleStickerPanel = null;
    [SerializeField] private GameObject curPanel = null;
    [SerializeField] private ParticleSystem[] eff_GetBubbleSticker = null;

    private int curIdx = 0;

    private void OnEnable()
    {
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
            SwellUpImg.rectTransform.localScale = Vector3.zero;
            StartCoroutine(CO_Bomb());
            return;
        }
        curIdx++;
        StartCoroutine(SwellUp());
    }

    public void Bomb()
    {
        SwellUpButton.interactable = false;
        curPanel.SetActive(false);
        GetBubbleStickerPanel.SetActive(true);
        manager_bs.SaveBubbleSticker();
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
        SwellUpImg.rectTransform.localScale = Vector3.zero;
        StartCoroutine(CO_Bomb());
    }


    private IEnumerator CO_Bomb()
    {
        for (int i = 0; i < eff_GetBubbleSticker.Length; ++i)
        {
            eff_GetBubbleSticker[i].Play();
        }
        yield return new WaitUntil(() => !eff_GetBubbleSticker[0].isPlaying);
        Bomb();
    }
}

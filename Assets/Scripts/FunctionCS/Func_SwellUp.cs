using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Func_SwellUp : MonoBehaviour
{
    [SerializeField] private Func_HelperGuideClick helperGuideClick = null;
    [SerializeField] private RectTransform helperGuideClickMovePos = null;
    [SerializeField] private RectTransform helperGuideClicInitPos = null;
    [SerializeField] private RawImage swellUpImg = null;
    [SerializeField] private RawImage stickerInBubbleImg = null;
    [SerializeField] private Button swellUpButton = null;
    [SerializeField] private Button skipButton = null;
    [SerializeField] private Button backButton = null;
    [SerializeField] private Manager_BubbleSticker manager_bs;
    [SerializeField] private GameObject getBubbleStickerPanel = null;
    [SerializeField] private GameObject curPanel = null;
    [SerializeField] private ParticleSystem[] eff_GetBubbleSticker = null;

    private int curIdx = 0;

    private void OnEnable()
    {
        skipButton.gameObject.SetActive(true);
        stickerInBubbleImg.gameObject.SetActive(false);
        helperGuideClick.gameObject.SetActive(true);
        helperGuideClick.GetComponent<RectTransform>().position = helperGuideClicInitPos.position;
        swellUpButton.interactable = true;
        curIdx = 0;
        swellUpImg.rectTransform.localScale = Vector3.zero;
    }

    public void OnClick_SwellUp()
    {
        if(curIdx == 3)
        {
            swellUpButton.interactable = false;
            return;
        }
        curIdx++;
        StartCoroutine(CO_SwellUp(curIdx));
    }

    public void SaveProcess()
    {
        StartCoroutine(CO_Bomb());
        swellUpButton.interactable = false;
        curPanel.SetActive(false);
        getBubbleStickerPanel.SetActive(true);
        manager_bs.SaveBubbleSticker();
    }

    private IEnumerator CO_SwellUp(int idx)
    {
        helperGuideClick.gameObject.SetActive(false);
        swellUpButton.interactable = false;
        while (true)
        {
            if (swellUpImg.rectTransform.localScale.x >= idx)
            {
                if (idx == 3)
                {
                    stickerInBubbleImg.texture = manager_bs.BubbleSticker.texture;
                    stickerInBubbleImg.gameObject.SetActive(true);
                    helperGuideClick.GetComponent<RectTransform>().position = helperGuideClickMovePos.position;
                    yield break;
                }
                swellUpButton.interactable = true;
                helperGuideClick.gameObject.SetActive(true);
                yield break;
            }
            swellUpImg.rectTransform.localScale += 0.02f * idx * Vector3.one;
            yield return null;
        }
    }

    public void OnClick_SkipButton()
    {
        swellUpButton.interactable = false;
        StartCoroutine(CO_SwellUp(3));
    }

    public void OnClick_SwellUpBubble()
    {
        backButton.gameObject.SetActive(false);
        swellUpImg.rectTransform.localScale = Vector3.zero;
        StartCoroutine(CO_Bomb());
    }

    private IEnumerator CO_Bomb()
    {
        for (int i = 0; i < eff_GetBubbleSticker.Length; ++i)
        {
            eff_GetBubbleSticker[i].Play();
        }

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < eff_GetBubbleSticker.Length; ++i)
        {
            eff_GetBubbleSticker[i].Clear(true);
        }

        SaveProcess();
    }

    private IEnumerator Co_Fluffy()
    {
        yield return null;
    }
}

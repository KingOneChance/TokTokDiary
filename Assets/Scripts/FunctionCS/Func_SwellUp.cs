using System.Collections;
using Unity.VisualScripting;
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

    [SerializeField] private ParticleSystem eff_BubblePop = null;

    [SerializeField] private GameObject explain1 = null;
    [SerializeField] private GameObject explain2 = null;

    [SerializeField] private GameObject homeButton = null;

    private void PlayBubblePop(Vector2 myPosInScreen)
    {
        Manager_Main.Instance.GetAudio().PlaySound("PopBubble", SoundType.Common, manager_bs.gameObject, false, true);
        eff_BubblePop.transform.position = new Vector3(myPosInScreen.x, myPosInScreen.y, eff_BubblePop.transform.position.z);
        eff_BubblePop.Play();
    }

    private int curIdx = 0;

    private void OnEnable()
    {
        explain1.SetActive(true);
        explain2.SetActive(false);
        homeButton.SetActive(false);
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
            explain1.SetActive(false);
            explain2.SetActive(true);
            swellUpButton.interactable = false;
            return;
        }
        curIdx++;
        StartCoroutine(CO_SwellUp(curIdx));
    }

    public void SaveProcess()
    {
        swellUpButton.interactable = false;
        curPanel.SetActive(false);
        getBubbleStickerPanel.SetActive(true);
        homeButton.SetActive(false);
        manager_bs.SaveBubbleSticker();
    }

    private IEnumerator CO_SwellUp(int idx)
    {
        Manager_Main.Instance.GetAudio().PlaySound("GettingBigger", SoundType.BubbleSticker, gameObject, false, true);
        helperGuideClick.gameObject.SetActive(false);
        swellUpButton.interactable = false;
        while (true)
        {
            if (swellUpImg.rectTransform.localScale.x >= idx)
            {
                if (idx == 3)
                {
                    swellUpImg.GetComponent<Button>().onClick.AddListener(OnClick_SwellUpBubble);
                    stickerInBubbleImg.texture = manager_bs.BubbleSticker.texture;
                    stickerInBubbleImg.gameObject.SetActive(true);
                    helperGuideClick.GetComponent<RectTransform>().position = helperGuideClickMovePos.position;
                    helperGuideClick.gameObject.SetActive(true);
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
        explain1.SetActive(false);
        explain2.SetActive(true);
        swellUpButton.interactable = false;
        StartCoroutine(CO_SwellUp(3));
    }

    public void OnClick_SwellUpBubble()
    {
        backButton.gameObject.SetActive(false);
        swellUpImg.rectTransform.localScale = Vector3.zero;
        PlayBubblePop(new Vector2(stickerInBubbleImg.transform.position.x, stickerInBubbleImg.transform.position.y));
        // 터지는 파티클 추가해주기
        Invoke(nameof(SaveProcess), 1.0f);
    }

    private IEnumerator Co_Fluffy()
    {
        yield return null;
    }
}

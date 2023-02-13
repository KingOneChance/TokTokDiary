using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_GunCollision : MonoBehaviour
{
    //°ËÀº ºñ´°¹æ¿ïµé
    [SerializeField] private RawImage[] blacks = null;
    //
    [SerializeField] private RawImage stickerBackGround = null;
    [SerializeField] private RawImage BackGroundSticker = null;
    [SerializeField] private Button skipOneRoundBtn = null;
    [SerializeField] private Button skipTwoRoundBtn = null;

    private Color dirtyColor = new Color(0, 0, 0, 255 / 255);
    private Color middleColor = new Color(0, 0, 0, 180f / 255f);
    private Color cleanColor = new Color(255/255, 255 / 255, 255 / 255, 255 / 255);
    private Color Nothing = new Color(255 / 255, 255 / 255, 255 / 255, 0);

    public bool allClear = false;
    public bool allPop = false;

    List<RawImage> blackList = new List<RawImage>();
    public List<RawImage> whiteList = new List<RawImage>();
    public List<RawImage> cleanList = new List<RawImage>();

    Func_BubbleGunSave func_BubbleGunSave = null;
    UI_BubbleBubbleGun ui_BubbleBubbleGun = null;

    private void Start()
    {
        for(int i = 0; i < blacks.Length; i++)
        {
            whiteList.Add(blacks[i]);
            cleanList.Add(blacks[i]);
            blackList.Add(blacks[i]);
        }

        func_BubbleGunSave = FindObjectOfType<Func_BubbleGunSave>();
        ui_BubbleBubbleGun = FindObjectOfType<UI_BubbleBubbleGun>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<RawImage>().color == dirtyColor)
        {
            collision.gameObject.GetComponent<RawImage>().color = middleColor;
            blackList.Remove(collision.gameObject.GetComponent<RawImage>());
            if (blackList.Count == 0)
            {
                allClear = true;
            }
        }
        else if (collision.gameObject.GetComponent<RawImage>().color == middleColor && allClear == true)
        {
            collision.gameObject.GetComponent<RawImage>().color = cleanColor;
            whiteList.Remove(collision.gameObject.GetComponent<RawImage>());
            if(whiteList.Count <= 14 && skipOneRoundBtn.gameObject.activeSelf == false)
            {
                skipOneRoundBtn.gameObject.SetActive(true);
            }
        }
        else if(collision.gameObject.GetComponent<RawImage>().color == cleanColor && allPop == true)
        {
            collision.gameObject.GetComponent<RawImage>().color = Nothing;
            collision.gameObject.GetComponent<RawImage>().texture = null;
            cleanList.Remove(collision.gameObject.GetComponent<RawImage>());
            if (cleanList.Count <= 14 && skipTwoRoundBtn.gameObject.activeSelf == false)
            {
                skipTwoRoundBtn.gameObject.SetActive(true);
            }
            if (cleanList.Count == 0)
            {
                RoundFinish();
            }
        }

    }
    //½ºÅµ

    public void OnClickSticker()
    {
        stickerBackGround.gameObject.SetActive(true);
        StartCoroutine(DisappearSticker());
        func_BubbleGunSave.SaveBubbleGun();
    }
    IEnumerator DisappearSticker()
    {
        skipTwoRoundBtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(10f);
        stickerBackGround.gameObject.SetActive(false);
    }

    public void RoundFinish()
    {
        BackGroundSticker.gameObject.SetActive(false);
        ui_BubbleBubbleGun.TobeSticker();
        skipTwoRoundBtn.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (whiteList.Count == 0)
        {
            allPop = true;
            skipOneRoundBtn.gameObject.SetActive(false);
        }
        if (cleanList.Count == 0)
        {
            RoundFinish();
        }
    }
}

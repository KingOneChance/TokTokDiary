using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_GunCollision : MonoBehaviour
{
    //°ËÀº ºñ´°¹æ¿ïµé
    [SerializeField] private RawImage[] blacks = null;
    //´õ·¯¿î ºñ´°¹æ¿ïµé
    [SerializeField] private Sprite[] dirtyImages = null;
    //Áß°£ ºñ´°¹æ¿ïµé
    [SerializeField] private Sprite[] middleImages = null;
    //±ú²ýÇÑ ºñ´°¹æ¿ïµé
    [SerializeField] private Sprite[] claenImages = null;

    //
    [SerializeField] private RawImage stickerBackGround = null;
    [SerializeField] private Image stickerBackGroundImage = null;
    [SerializeField] private Image Restart = null;
    [SerializeField] private Image sticerRepository = null;

    [SerializeField] private RawImage BackGroundSticker = null;
    [SerializeField] private Button skipOneRoundBtn = null;
    [SerializeField] private Button skipTwoRoundBtn = null;

    private Color Nothing = new Color(255 / 255, 255 / 255, 255 / 255, 0);

    public bool allClear = false;
    public bool allPop = false;

    List<RawImage> blackList = new List<RawImage>();
    public List<RawImage> whiteList = new List<RawImage>();
    public List<RawImage> cleanList = new List<RawImage>();

    Func_BubbleGunSave func_BubbleGunSave = null;
    UI_BubbleBubbleGun ui_BubbleBubbleGun = null;

    private int randNum = 0;
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

        for(int i = 0; i < blacks.Length; i++)
        {
            randNum = Random.Range(0, dirtyImages.Length);
            blacks[i].texture = dirtyImages[randNum].texture;
        }
    }
    public void AllPopChange()
    {
        allPop = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<RawImage>().texture.name.Contains("dirty"))
        {
            Debug.Log(collision.gameObject.GetComponent<RawImage>().texture.name);
            ChangeImage(collision.gameObject.GetComponent<RawImage>().texture.name, collision.gameObject);
            blackList.Remove(collision.gameObject.GetComponent<RawImage>());
            if (blackList.Count == 0)
            {
                allClear = true;
            }
        }
        else if (collision.gameObject.GetComponent<RawImage>().texture.name.Contains("messy") && allClear == true)
        {
            ChangeImage(collision.gameObject.GetComponent<RawImage>().texture.name, collision.gameObject);
            whiteList.Remove(collision.gameObject.GetComponent<RawImage>());
            if(whiteList.Count <= 10 && skipOneRoundBtn.gameObject.activeSelf == false)
            {
                skipOneRoundBtn.gameObject.SetActive(true);
            }
        }
        else if(collision.gameObject.GetComponent<RawImage>().texture.name.Contains("clean") && allPop == true)
        {
            Debug.Log(collision.gameObject.GetComponent<RawImage>().texture.name);
            Debug.Log(collision.gameObject);
            ChangeImage(collision.gameObject.GetComponent<RawImage>().texture.name, collision.gameObject);
            
            cleanList.Remove(collision.gameObject.GetComponent<RawImage>());
            collision.gameObject.GetComponent<RawImage>().color = Nothing;
            if (cleanList.Count <= 10 && skipTwoRoundBtn.gameObject.activeSelf == false)
            {
                skipTwoRoundBtn.gameObject.SetActive(true);
            }
            if (cleanList.Count == 0)
            {
                RoundFinish();
            }
        }

    }
    public void ChangeImage(string textureName, GameObject go)
    {
        string bubbleState = textureName.Split("_")[0];
        string bubbleColor = textureName.Split("_")[1];

        switch (bubbleState)
        {
            case "dirty":
                Debug.Log(middleImages.Length);
                for(int i =0; i< middleImages.GetLength(0); i++)
                {
                    if (middleImages[i].name.Contains(bubbleColor))
                    { 
                        go.GetComponent<RawImage>().texture = middleImages[i].texture;
                        break;
                    }
                }
                break;
            case "messy":
                for (int i = 0; i < claenImages.Length; i++)
                {
                    if (claenImages[i].name.Contains(bubbleColor))
                    {                     
                        go.GetComponent<RawImage>().texture = claenImages[i].texture;
                        break;
                    }
                }
                break;

        }
    }
    public void RoundFinish()
    {
        ui_BubbleBubbleGun.TobeSticker();
        BackGroundSticker.gameObject.SetActive(false);
        skipTwoRoundBtn.gameObject.SetActive(false);
        Debug.Log(ui_BubbleBubbleGun.randImages.Length);
    }
    public void OnClickSticker()
    {
        for(int i =0; i < ui_BubbleBubbleGun.randImages.Length; i++)
        {
            ui_BubbleBubbleGun.randImages[i].gameObject.SetActive(false);
        }
        
        stickerBackGround.gameObject.SetActive(true);
        StartCoroutine(DisappearSticker());
        func_BubbleGunSave.SaveBubbleGun();
    }
    IEnumerator DisappearSticker()
    {
        yield return new WaitForSeconds(10f);
        stickerBackGroundImage.gameObject.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_GunCollision : MonoBehaviour
{
    //검은 비눗방울들
    [SerializeField] private RawImage[] blacks = null;
    //더러운 비눗방울들
    [SerializeField] private Sprite[] dirtyImages = null;
    //중간 비눗방울들
    [SerializeField] private Sprite[] middleImages = null;
    //깨끗한 비눗방울들
    [SerializeField] private Sprite[] claenImages = null;
    //캡쳐후 끌 이미지
    [SerializeField] public Image offImage = null;
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
        
        for (int i = 0; i < blacks.Length; i++)
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
            
            ChangeImage(collision.gameObject.GetComponent<RawImage>().texture.name, collision.gameObject);
            blackList.Remove(collision.gameObject.GetComponent<RawImage>());
            //닦이는 사운드 및 이펙트
            Manager_Main.Instance.GetAudio().PlaySound("Mop", SoundType.Diary, collision.gameObject, false, true);
            if (blackList.Count == 0)
            {
                allClear = true;
            }
        }
        else if (collision.gameObject.GetComponent<RawImage>().texture.name.Contains("messy") && allClear == true)
        {
            ChangeImage(collision.gameObject.GetComponent<RawImage>().texture.name, collision.gameObject);
            whiteList.Remove(collision.gameObject.GetComponent<RawImage>());
            //닦이는 사운드 및 이펙트
            Manager_Main.Instance.GetAudio().PlaySound("Mop", SoundType.Diary, collision.gameObject, false, true);
            if (whiteList.Count <= 10 && skipOneRoundBtn.gameObject.activeSelf == false)
            {
                skipOneRoundBtn.gameObject.SetActive(true);
            }
        }
        else if(collision.gameObject.GetComponent<RawImage>().texture.name.Contains("clean") && allPop == true)
        {
            ChangeImage(collision.gameObject.GetComponent<RawImage>().texture.name, collision.gameObject);
            
            cleanList.Remove(collision.gameObject.GetComponent<RawImage>());
            collision.gameObject.GetComponent<RawImage>().color = Nothing;
            //이펙트 및 터지는 사운드
            Manager_Main.Instance.GetAudio().PlaySound("PopBubble", SoundType.Common, collision.gameObject, false, true);
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

    }
    public void OnClickSticker()
    {
        for(int i =0; i < ui_BubbleBubbleGun.randImages.Length; i++)
        {
            ui_BubbleBubbleGun.randImages[i].gameObject.SetActive(false);
        }
        
        stickerBackGround.gameObject.SetActive(true);
        StartCoroutine(DisappearSticker());

        string fileName = ui_BubbleBubbleGun.sticker.sprite.name;
            
        func_BubbleGunSave.SaveBubbleGun(fileName);

    }
    IEnumerator DisappearSticker()
    {
        Manager_Main.Instance.GetAudio().PlaySound("Fanfare", SoundType.Common, gameObject, false, true);
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

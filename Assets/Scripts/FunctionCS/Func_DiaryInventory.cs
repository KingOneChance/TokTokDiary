using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Func_DiaryInventory : MonoBehaviour
{
    [Header("===StickerButtons===")]
    // [SerializeField] private Button[] ui_StickerBtns = null;
    [Header("===Stickers===")]
    // 기본으로 깔리는 이미지
    [SerializeField] private List<RawImage> mainStickers = null;
    [SerializeField] private List<RawImage> signStickers = null;
    //부모 위치
    [SerializeField] private GameObject ui_myParent = null;
    //비눗방을 프리팹 
    [SerializeField] private GameObject stickerPrefab = null;

    public List<string> bubbleStickerList = new List<string>();
    public List<string> recordingStickerList = new List<string>();
    public List<string> recordingSignList = new List<string>();
    public List<string> bubbleGunStickerList = new List<string>();
    public List<string> bubbleFreeStickerList = new List<string>();

    string path = "";

    private void Start()
    {
        path = Application.persistentDataPath;
        RepositoryListOpen(); //그림일기씬 오픈시 실행
    }

    public void OnClick_BubbleStickerRepository()
    {
        LoadLocalSticker(bubbleStickerList);
    }
    /* public void OnClick_RecordingStickerRepository() //사실상 쓸일 없음
     {
         LoadLocalSticker(recordingStickerList);
     }*/
    public void OnClick_RecordFileRepository()
    {
        LoadLocalSticker(recordingStickerList, recordingSignList);
    }
    public void OnClick_BubbleGunStickerRepository()
    {
        LoadLocalSticker(bubbleGunStickerList);
    }
    public void OnClick_BubbleFreeStickerRepository()
    {
        LoadLocalSticker(bubbleFreeStickerList);
    }
    //리스트 채우기
    public void RepositoryListOpen()
    {
        string[] allFiles = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
        if (allFiles.Length == bubbleStickerList.Count + recordingStickerList.Count + recordingSignList.Count + bubbleGunStickerList.Count + bubbleFreeStickerList.Count)
        {
            return;
        }
        //List Initiate for rearrange;
        bubbleStickerList.Clear();
        recordingStickerList.Clear();
        recordingSignList.Clear();
        bubbleGunStickerList.Clear();
        bubbleFreeStickerList.Clear();
        for (int i = 0; i < allFiles.Length; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);


                if (allFiles[i].Contains("BubbleSticker"))
                {
                    bubbleStickerList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("RecordingSticker"))
                {
                    recordingStickerList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("RecordingSign"))
                {
                    recordingSignList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("BubbleGun"))
                {
                    bubbleGunStickerList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("BubbleFree"))
                {
                    bubbleFreeStickerList.Add(allFiles[i]);
                }
                else
                {
                    Debug.Log("It is not my Sticker");
                }
            }
        }

    }

    RawImage newMainSticker, newSignSticker;
    List<RawImage> MakingObj = new List<RawImage>();

    /// <summary>
    /// This function is purpose to fill Repository 
    /// </summary>
    /// <param name="anyList">Insert anyList without signlist</param>
    /// <param name="anyList2">Insert signList</param>
    private void LoadLocalSticker(List<string> anyList, List<string> anyList2 = null)
    {
        for (int i = 0; i < mainStickers.Count;i++)
        {
            if (mainStickers[i].gameObject.activeSelf == false) break; //메인스티커나 서명 스티커 하나만 꺼져도 꺼져있는 상태
            mainStickers[i].texture = null;
            signStickers[i].texture = null;
            mainStickers[i].color = new Color(255, 255, 255, 0);
            signStickers[i].color = new Color(255, 255, 255, 0); //메인, 서명 스티커 텍스쳐 빼고 투명화
        }


        //12이상 채워진 텍스쳐들 삭제하는 로직
        if (anyList.Count < 12)
        {
            for (int i = mainStickers.Count - 1; i > 11; --i)
            {

                mainStickers[i].texture = null;
                signStickers[i].texture = null;
                mainStickers[i].color = new Color(255, 255, 255, 0);
                signStickers[i].color = new Color(255, 255, 255, 0); //메인, 서명 스티커 텍스쳐 빼고 투명화

                mainStickers[i].gameObject.SetActive(false); //끄기
            }
        }
        if (anyList.Count - 10 > 0) //12개 이상시 채워두기 
        {
            int makingRawImage = anyList.Count - 11;
            int makingLine = makingRawImage / 2 + 1;
            for (int k = 0; k < (makingLine); k++)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject temp = Instantiate(stickerPrefab, ui_myParent.transform);
                    temp.transform.GetChild(1).TryGetComponent<RawImage>(out newMainSticker);
                    temp.transform.GetChild(1).transform.GetChild(0).TryGetComponent<RawImage>(out newSignSticker);
                    mainStickers.Add(newMainSticker);
                    signStickers.Add(newSignSticker);
                }
            }
        }
        //Fill in the raw image's texture
        for (int i = 0; i < anyList.Count; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(anyList[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);

                mainStickers[i].texture = texture;
                mainStickers[i].color = new Color(255, 255, 255, 255);
            }
            if (anyList2 != null)
            {
                byte[] byteTexture2 = File.ReadAllBytes(anyList2[i]);
                if (byteTexture2.Length > 0)
                {
                    Texture2D texture2 = new Texture2D(0, 0);
                    texture2.LoadImage(byteTexture2);

                    signStickers[i].texture = texture2;
                    signStickers[i].color = new Color(255, 255, 255, 255);
                }
            }
        }
    }
}

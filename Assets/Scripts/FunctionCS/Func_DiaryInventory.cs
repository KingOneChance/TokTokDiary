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
    [SerializeField] private List<RawImage> mainStickersBack = null;
    [SerializeField] private List<RawImage> signStickersBack = null;
    [SerializeField] private List<string> recordFileName = null;
    //부모 위치
    [SerializeField] private GameObject ui_myParent = null;
    //비눗방을 프리팹 
    [SerializeField] private GameObject stickerPrefab = null;

    public List<string> bubbleStickerList = new List<string>();
    public List<string> recordingStickerList = new List<string>();
    public List<string> recordingSignList = new List<string>();
    public List<string> recordingFileList = new List<string>();
    public List<string> bubbleGunStickerList = new List<string>();
    public List<string> bubbleFreeStickerList = new List<string>();

    [SerializeField] private StickerType nowStickerType = StickerType.None;

    string path = "";

    private void Start()
    {
        path = Application.persistentDataPath;
        RepositoryListOpen(); //그림일기씬 오픈시 실행
        OnClick_BubbleFreeStickerRepository(); //처음 인벤토리 오픈
    }

    public StickerType GetNowType()
    {
        return nowStickerType;
    }
    public string GetRecordName(int i)
    {
        return recordFileName[i];
    }
    public string GetRecordingStickerList(int i)
    {
        return recordingStickerList[i];
    }
    public string GetRecordingSignList(int i)
    {
        return recordingSignList[i];
    }
    public string GetFreeSignList(int i)
    {
        return recordingSignList[i];
    }


    public void OnClick_BubbleStickerRepository()
    {
        nowStickerType = StickerType.BubbleSticker;
        LoadLocalSticker(bubbleStickerList);
    }
    public void OnClick_RecordFileRepository()
    {
        nowStickerType = StickerType.RecordSticker;
        LoadLocalSticker(recordingStickerList, recordingSignList);
    }
    public void OnClick_BubbleGunStickerRepository()
    {
        nowStickerType = StickerType.BubbleGunSticker;
        LoadLocalSticker(bubbleGunStickerList);
    }
    public void OnClick_BubbleFreeStickerRepository()
    {
        nowStickerType = StickerType.FreeSticker;
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
        string[] allRecord = Directory.GetFiles(path, "*.wav", SearchOption.AllDirectories);
        if(allRecord.Length!=0)
        {
            foreach (string file in allRecord)
            {
                recordFileName.Add(file);
                Debug.Log(file);
            }
        }
        //List Initiate for rearrange;
        bubbleStickerList.Clear();
        recordingStickerList.Clear();
        recordingSignList.Clear();
        bubbleGunStickerList.Clear();
        bubbleFreeStickerList.Clear();
        recordingFileList.Clear(); //wav file
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

    RawImage newMainSticker, newSignSticker, newMainStickerBack, newSignStickerBack;
    List<RawImage> MakingObj = new List<RawImage>();

    /// <summary>
    /// This function is purpose to fill Repository 
    /// </summary>
    /// <param name="anyList">Insert anyList without signlist</param>
    /// <param name="anyList2">Insert signList</param>
    private void LoadLocalSticker(List<string> anyList, List<string> anyList2 = null)
    {
        for (int i = 0; i < mainStickers.Count;i++) //다 끄기
        {
            if (mainStickers[i].gameObject.activeSelf == false) break; //메인스티커나 서명 스티커 하나만 꺼져도 꺼져있는 상태

            mainStickers[i].texture = null;
            signStickers[i].texture = null;
            mainStickers[i].color = new Color(255, 255, 255, 0);
            signStickers[i].color = new Color(255, 255, 255, 0); //메인, 서명 스티커 텍스쳐 뺀 후, 투명화
            mainStickersBack[i].texture = null;
            signStickersBack[i].texture = null;
            mainStickersBack[i].color = new Color(255, 255, 255, 0);
            signStickersBack[i].color = new Color(255, 255, 255, 0);

            mainStickers[i].gameObject.SetActive(false);
            mainStickersBack[i].gameObject.SetActive(false);
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
                mainStickersBack[i].texture = null;
                signStickersBack[i].texture = null;
                mainStickersBack[i].color = new Color(255, 255, 255, 0);
                signStickersBack[i].color = new Color(255, 255, 255, 0);

                mainStickers[i].gameObject.SetActive(false); //끄기
                mainStickersBack[i].gameObject.SetActive(false);//
            }
        }
        if (anyList.Count - 10 > 0 && anyList.Count > mainStickers.Count) //12개 이상시 채워두기 
        {
            int makingRawImage = anyList.Count - 11;
            int makingLine = makingRawImage / 2 + 1;
            for (int k = 0; k < (makingLine); k++)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject temp = Instantiate(stickerPrefab, ui_myParent.transform);
                    temp.transform.GetChild(1).TryGetComponent<RawImage>(out newMainSticker);
                    temp.transform.GetChild(0).TryGetComponent<RawImage>(out newMainStickerBack);
                    temp.transform.GetChild(1).transform.GetChild(0).TryGetComponent<RawImage>(out newSignSticker);
                    temp.transform.GetChild(0).transform.GetChild(0).TryGetComponent<RawImage>(out newSignStickerBack);
                    mainStickers.Add(newMainSticker);
                    signStickers.Add(newSignSticker);
                    mainStickersBack.Add(newMainStickerBack);
                    signStickersBack.Add(newSignStickerBack);
                }
            }
        }
        //Fill in the raw image's texture
        for (int i = 0; i < anyList.Count; i++)
        {
            mainStickers[i].gameObject.name = i.ToString();
            byte[] byteTexture = File.ReadAllBytes(anyList[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);

                mainStickers[i].gameObject.SetActive(true);
                mainStickersBack[i].gameObject.SetActive(true);

                mainStickers[i].texture = texture;
                mainStickersBack[i].texture = texture;
                mainStickers[i].color = new Color(255, 255, 255, 255);
                mainStickersBack[i].color = new Color(255, 255, 255, 255);
            }
            if (anyList2 != null)
            {
                byte[] byteTexture2 = File.ReadAllBytes(anyList2[i]);
                if (byteTexture2.Length > 0)
                {
                    Texture2D texture2 = new Texture2D(0, 0);
                    texture2.LoadImage(byteTexture2);

                    signStickers[i].texture = texture2;
                    signStickersBack[i].texture = texture2; 
                    signStickers[i].color = new Color(255, 255, 255, 255);
                    signStickersBack[i].color = new Color(255, 255, 255, 255);
                }
            }
        }
    }
}

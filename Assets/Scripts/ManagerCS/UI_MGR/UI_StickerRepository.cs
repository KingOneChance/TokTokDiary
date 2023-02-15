using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UI_StickerRepository : MonoBehaviour
{
    [SerializeField] Button on = null;
    [Header("===StickerButtons===")]
    // [SerializeField] private Button[] ui_StickerBtns = null;
    //토글 버튼들
    [SerializeField] private Toggle[] ui_StickerBtns = null;
    [Header("===Stickers===")]
    // 기본으로 깔리는 비눗방울
    [SerializeField] private List<RawImage> basicStickers = null;
    [SerializeField] private Texture basicTextrue = null;
    //부모 위치
    [SerializeField] private GameObject ui_myParent = null;
    //비눗방을 프리팹 
    [SerializeField] GameObject stickerPrefab = null;
    [SerializeField] private RawImage[] ui_RecordSubStickers = null;

    public List<string> bubbleStickerList = new List<string>();
    public List<string> recordingStickerList = new List<string>();
    public List<string> recordingSignList = new List<string>();
    public List<string> bubbleGunStickerList = new List<string>();
    public List<string> bubbleFreeStickerList = new List<string>();

    string path = "";

    private void Start()
    {
        path = Application.persistentDataPath;
    }

    public void OnClick_BubbleStickerRepository()
    {
        LoadLocalSticker(bubbleStickerList);
    }
    public void OnClick_RecordingStickerRepository()
    {
        LoadLocalSticker(recordingStickerList);
    }
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

    public void OnClick_RepositoryOpen()
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
    RawImage go;
    List<RawImage> MakingObj = new List<RawImage>();
    /// <summary>
    /// This function is purpose to fill Repository 
    /// </summary>
    /// <param name="anyList">Insert anyList without signlist</param>
    /// <param name="anyList2">Insert signList</param>
    private void LoadLocalSticker(List<string> anyList, List<string> anyList2 = null)
    {
        
        if (anyList.Count < 12)
        {
            for (int i = basicStickers.Count-1; i > 11; --i)
            {
                basicStickers[i].texture = basicTextrue;
                Destroy(basicStickers[i].gameObject);
                basicStickers.RemoveAt(i);
            }
            for (int i = 0; i < MakingObj.Count; i++)
            {
                Destroy(MakingObj[i].gameObject);
                MakingObj.RemoveAt(i);
            }

        }
        
        MakingObj.Clear();
        if (anyList.Count - 8 > 0)
        {
            Debug.Log(anyList.Count);
            int makingRawImage = anyList.Count - 9;
            int makingLine = makingRawImage / 4 + 1;
            for (int k = 0; k < (makingLine); k++)
            {
                for(int i= 0; i< 4; i++)
                {
                    go = Instantiate(stickerPrefab, ui_myParent.transform).GetComponent<RawImage>();
                    MakingObj.Add(go);
                    basicStickers.Add(go);
                }
            }

        }

        //initiate raw images's texture

        for (int i = 0; i < basicStickers.Count; i++)
        {
            basicStickers[i].texture = basicTextrue;
            /*if (anyList2 != null)
            {
                ui_RecordSubStickers[i].gameObject.SetActive(true);
                ui_RecordSubStickers[i].texture = null;
            }*/
        }

        //Fill in the raw image's texture
        for (int i = 0; i < anyList.Count; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(anyList[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);

                basicStickers[i].texture = texture;
            }
            if (anyList2 != null)
            {
                byte[] byteTexture2 = File.ReadAllBytes(anyList2[i]);
                if (byteTexture2.Length > 0)
                {
                    Texture2D texture2 = new Texture2D(0, 0);
                    texture2.LoadImage(byteTexture2);

                    ui_RecordSubStickers[i].texture = texture2;
                }
            }
        }
    }
}

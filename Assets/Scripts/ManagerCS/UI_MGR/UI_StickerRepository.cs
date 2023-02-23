using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class UI_StickerRepository : MonoBehaviour
{
    [SerializeField] Button on = null;
    [Header("===StickerButtons===")]

    [SerializeField] private Toggle[] ui_StickerBtns = null;
    [Header("===Stickers===")]
    // 기본으로 깔리는 비눗방울
    [SerializeField] private List<RawImage> basicStickers = null;
    [SerializeField] private Texture basicTextrue = null;
    //부모 위치
    [SerializeField] private GameObject ui_myParent = null;
    //사인 프리팹 
    [SerializeField] private GameObject stickerPrefab = null;
    [SerializeField] private List<RawImage> ui_RecordSubStickers = null;

    public List<string> bubbleStickerList = new List<string>();
    public List<string> recordingStickerList = new List<string>();
    public List<string> recordingSignList = new List<string>();
    public List<string> bubbleGunStickerList = new List<string>();
    public List<string> bubbleFreeStickerList = new List<string>();
    string path = "";

    [Header("DeleteWarning")]
    [SerializeField] public TextMeshProUGUI stickerCountText = null;
    [SerializeField] private Image trashCan = null;
    [SerializeField] private Sprite closeTrashCan = null;
    [SerializeField] private Image deletePopUp = null;
  
    private int presentNum = 0;

    private void Start()
    {
        path = Application.persistentDataPath;
    }
    private void OnEnable()
    {
        LoadLocalSticker(bubbleStickerList);
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
            Debug.Log(allFiles[i]);
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
                else if (allFiles[i].Contains("BubbleGunSticker"))
                {
                    bubbleGunStickerList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("BubbleFreeSticker"))
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
                    ui_RecordSubStickers.Add(go.transform.GetChild(0).GetComponent<RawImage>());
                }
            }
        }

        //initiate raw images's texture

        for (int i = 0; i < basicStickers.Count; i++)
        {
            basicStickers[i].texture = basicTextrue;
            basicStickers[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
          /*  if (anyList2 != null)
            {
                ui_RecordSubStickers[i].color = new Color(255, 255, 255, 255);
                ui_RecordSubStickers[i].texture = null;
            }
            else
            {
                //ui_RecordSubStickers[i].color = new Color(255, 255, 255, 0);
                //ui_RecordSubStickers[i].texture = null;
            }*/
        }
        for(int i = 0; i < recordingSignList.Count; i++)
        {
            ui_RecordSubStickers[i].color = new Color(255, 255, 255, 0);
        }

        //Fill in the raw image's texture
        for (int i = 0; i < anyList.Count; i++)
        {
            Debug.Log(anyList[i]);
            byte[] byteTexture = File.ReadAllBytes(anyList[i]);
#if UNITY_ANDROID
            string filename = anyList[i].Split('/')[9].Split('.')[0];
#else
            string filename = anyList[i].Split('/')[6].Split('\\')[2].Split('.')[0];
#endif

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);
                texture.name = filename;
                basicStickers[i].texture = texture;
                basicStickers[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Manager_Main.Instance.GetCurStickerUserCount(basicStickers[i].texture.name);
                if(basicStickers[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "0")
                {
                    basicStickers[i].GetComponent<Button>().interactable = false;
                }
            }
            if (anyList2 != null)
            {
                byte[] byteTexture2 = File.ReadAllBytes(anyList2[i]);
                if (byteTexture2.Length > 0)
                {
                    Texture2D texture2 = new Texture2D(0, 0);
                    texture2.LoadImage(byteTexture2);

                    ui_RecordSubStickers[i].texture = texture2;
                    ui_RecordSubStickers[i].color = new Color(255, 255, 255, 255);
                }
            }
        }
    }

    private GameObject sticker = null;
    public void CheckStickerCount(GameObject go)
    {
        sticker = go;
        sticker.name = go.GetComponent<RawImage>().texture.name;

        presentNum = int.Parse(go.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text);
        stickerCountText.text = go.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;

    }
    public void CloseTrashCan()
    {
        trashCan.sprite = closeTrashCan;
    }
    public void Onclick_PlusBtn()
    {
        presentNum++;
        stickerCountText.text = presentNum.ToString();
    }
    public void Onclick_MinusBtn()
    {
        presentNum--;
        if(presentNum < 0) presentNum = 0;
        stickerCountText.text = presentNum.ToString();
    }

    public void Onclick_TransferDelete()
    {
        if(presentNum > int.Parse(Manager_Main.Instance.GetCurStickerUserCount(sticker.name)))
        {
            Debug.Log("스티커보다 수량보다 많습니다");
        }
        else if(presentNum == 0)
        {
            Debug.Log("삭제될것이 없습니다");
           
        }
        else if (presentNum == int.Parse(Manager_Main.Instance.GetCurStickerUserCount(sticker.name)))
        {
            PlayerPrefs.DeleteKey(sticker.name);
            PlayerPrefs.Save();
            sticker.GetComponent<RawImage>().texture = basicTextrue;
        }
        else if(presentNum < int.Parse(Manager_Main.Instance.GetCurStickerUserCount(sticker.name)))
        {
            for(int i = 0; i< presentNum; i++)
            {
                Manager_Main.Instance.UseSticker(sticker.name);
                
            }
            sticker.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = Manager_Main.Instance.GetCurStickerUserCount(sticker.name);
         }
        else
        {
            Debug.Log("이상한 그림 가져왔음ㅋㅋ");
            return;
        }

        deletePopUp.gameObject.SetActive(false);
        CloseTrashCan();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static NativeCamera;

public class UI_PictureDiary : MonoBehaviour
{
    //그림판
    [Header("그림판")]
    [SerializeField] private Image ui_DrawBackground = null;
    [SerializeField] Button[] ui_WeatherBtn = null;

    [Header("카메라")]
    //카메라 on - off, 불러오기
    [SerializeField] Button ui_CameraOnBtn = null;
    //[SerializeField] Button cameraOffBtn = null;
    [SerializeField] Button ui_LoadBtn = null;


    [Header("스티커")]
    [SerializeField] RawImage[] ui_Stickers = null;
    [SerializeField] Button[] ui_StickersBtns = null;

    [Header("바늘")]
    [SerializeField] Button ui_NiddleBtn = null;
    [SerializeField] Texture2D ui_NiddleImage = null;

    [SerializeField] Texture2D ui_StickImage = null;

    [Header("프로필")]
    //프로필 메인
    [SerializeField] Image ui_ProfileBackGround = null;
    [SerializeField] RawImage ui_ProfileMain = null;
    [SerializeField] RawImage ui_ProfilePlus = null;
    [SerializeField] RawImage ui_ProfilePick = null;
    [SerializeField] RawImage ui_ProfileOverWrite = null;
    [SerializeField] RawImage ui_ProfileDelete = null;

    //프로필 추가
    [SerializeField] RawImage plusProfileImage = null;
    [SerializeField] TMP_InputField newNickName = null;

    //프로필 수정
    [SerializeField] RawImage overWriteImage = null;
    [SerializeField] TMP_InputField overWriteNickName = null;
    [Header("기타")]
    //홈버튼
    [SerializeField] Button ui_HomeBtn = null;
    [SerializeField] private RawImage loadImage;

    private TextMeshProUGUI before = null;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private bool isNiddleClicked = false;
    private bool isStickClicked = false;

    [SerializeField] GameObject soupImage = null;

    private void Start()
    {

        hotSpot.x = ui_NiddleImage.width / 2;
        hotSpot.y = ui_NiddleImage.height / 2;

        Manager_Main.Instance.UI_StickerRepository.OnClick_RepositoryOpen();

        //바늘 상태
        NiddleState = NiddleType.None;
        //비눗방울 스티커 상태
        SoupState = SoupBubbleType.Soap;

        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
        before = new TextMeshProUGUI();
    }

    #region Road Sticker Button
    public void OnClick_WaffleRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.bubbleStickerList);
    }
    public void OnClick_AudioRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.recordingStickerList);
    }
    public void OnClick_WeatherRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.bubbleGunStickerList);
    }
    public void OnClick_DiaryRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.bubbleFreeStickerList);
    }
    #endregion
    public void LoadLocalStickerInventory(List<string> anyList)
    {
        //initiate raw images's texture
        for (int i = 0; i < ui_Stickers.Length; i++)
        {
            ui_Stickers[i].texture = null;
        }
        //Fill in the raw image's texture
        for (int i = 0; i < anyList.Count; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(anyList[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);

                ui_Stickers[i].texture = texture;
            }
        }
    }

    public void OnClick_NativeCameraOnBtn()
    {
        TakePicture(CallBack);
    }

    private void CallBack(string path)
    {
        Byte[] bytes = File.ReadAllBytes(path);
        string fileName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
        NativeGallery.SaveImageToGallery(bytes, "DiaryPictureAlbum", fileName + ".jpg");
        StartCoroutine(LoadImage(path, loadImage));
    }

    public void Onclick_LoadImage()
    {
        Debug.Log("file Path : ");
        NativeGallery.GetImageFromGallery((file) =>
        {
            //용량제한
            Debug.Log("file Path : 1번째줄" + file);
            FileInfo selected = new FileInfo(file);
            //용량제한
            Debug.Log("file Path : 2번째줄" + file);
            if (selected.Length > 50000000)
            {
                Debug.Log("file Path 없음");
                return;
            }
            if (!string.IsNullOrEmpty(file))
            {
                Debug.Log("file Path 있음");

                //불러와라
                StartCoroutine(LoadImage(file, loadImage));
            }
        });
        Debug.Log("file Path 탐색끝 ");
        if (loadImage.rectTransform.rotation != Quaternion.identity)
            loadImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
    }
    public void Onclick_LoadImage(RawImage raw)
    {
        //Debug.Log("file Path : ");
        NativeGallery.GetImageFromGallery((file) =>
        {
            //용량제한
            //Debug.Log("file Path : 1번째줄" + file);
            FileInfo selected = new FileInfo(file);
            //용량제한
            //Debug.Log("file Path : 2번째줄" + file);
            if (selected.Length > 50000000)
            {
                Debug.Log("file Path 없음");
                return;
            }
            if (!string.IsNullOrEmpty(file))
            {
                Debug.Log("file Path 있음");

                //불러와라
                StartCoroutine(LoadImage(file, raw));
            }
        });
        //Debug.Log("file Path 탐색끝 ");
        if (loadImage.rectTransform.rotation != Quaternion.identity)
            loadImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
    }
    IEnumerator LoadImage(string path, RawImage loadImage)
    {
        yield return null;
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path).Split('.')[0];
        // string savePath = Application.persistentDataPath + "/TestImage/";

        //if (!Directory.Exists(savePath))
        //{
        //    Directory.CreateDirectory(savePath);
        //}

        File.WriteAllBytes(path + fileName + ".png", fileData);

        var temp = File.ReadAllBytes(path + fileName + ".png");

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);
        loadImage.texture = tex;
        loadImage.rectTransform.rotation = Quaternion.identity;
    }

    #region NiddleBtn , StickBtn
    public void OnClick_NiddleBtn()
    {
        if (!isNiddleClicked)
        {
            Cursor.SetCursor(ui_NiddleImage, hotSpot, cursorMode);
            isNiddleClicked = true;
            NiddleState = NiddleType.Niddle;
        }
        else
        {
            Cursor.SetCursor(default, hotSpot, cursorMode);
            isNiddleClicked = false;
            NiddleState = NiddleType.None;
        }
    }
    public void OnClick_BubbleStick()
    {
        if (!isStickClicked)
        {
            Cursor.SetCursor(ui_StickImage, hotSpot, cursorMode);
            isStickClicked = true;
            StickState = StickType.Stick;
        }
        else
        {
            Cursor.SetCursor(default, hotSpot, cursorMode);
            isStickClicked = false;
            StickState = StickType.None;
        }
    }


    private NiddleType NiddleState;
    public NiddleType NiddleStateInfo { get { return NiddleState; } }
    private SoupBubbleType SoupState;
    public SoupBubbleType SoupStateInfo { get { return SoupState; } }

    private StickType StickState;
    public StickType StickStateInfo { get { return StickState; } }
    public void OnClick_Pop()
    {
        if (NiddleState == NiddleType.Niddle && SoupState == SoupBubbleType.Soap)
        {
            Debug.Log("펑");
            soupImage.GetComponent<Func_StickerDrag>().enabled = false;
            SoupState = SoupBubbleType.Attached;
        }
        if (SoupState == SoupBubbleType.Attached && StickState == StickType.Stick)
        {
            soupImage.GetComponent<Func_StickerDrag>().enabled = true;
            SoupState = SoupBubbleType.Soap;
            Debug.Log("떼졌쥬?");
        }
    }
    #endregion

    #region Profile
    // 메인화면 x 버튼, 메인화면 취소버튼 

    public void OnClick_ExitProfile()
    {
        ui_ProfileBackGround.gameObject.SetActive(false);
    }

    //프로필 메인
    public void OnClick_OpenProfileButton()
    {
        ui_ProfileBackGround.gameObject.SetActive(true);
        ui_ProfileMain.gameObject.SetActive(true);
        ui_ProfilePlus.gameObject.SetActive(false);
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfileOverWrite.gameObject.SetActive(false);

    }

    public void OnClick_OpenPlusProfile()
    {
        ui_ProfilePlus.gameObject.SetActive(true);
        ui_ProfileMain.gameObject.SetActive(false);
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfileOverWrite.gameObject.SetActive(false);
    }

    public void OnClick_OpenOverWriteProfile()
    {
        ui_ProfilePick.gameObject.SetActive(true);
        ui_ProfilePlus.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(false);
        ui_ProfileOverWrite.gameObject.SetActive(false);
    }

    public void OnClick_SaveProfile()
    {

    }
    //프로필 추가부분
    public void OnClick_ExitPlusProfile()
    {
        ui_ProfilePlus.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(true);
    }
    public void OnClick_LoadProfilePicture()
    {
        Onclick_LoadImage(plusProfileImage);
    }
    public void OnClick_SaveNewProfile()
    {
        string savePath = Application.persistentDataPath + "/Profile/" + newNickName.text + "/";
        SaveTextureToPng(plusProfileImage.texture, savePath, newNickName.text);

        ui_ProfileMain.gameObject.SetActive(true);
        ui_ProfilePlus.gameObject.SetActive(false);
    }

    private void SaveTextureToPng(Texture texture, string directoryPath, string fileName)
    {
        if (true == string.IsNullOrEmpty(directoryPath)) return;
        if (false == Directory.Exists(Application.persistentDataPath + "/" + "Profile")) Directory.CreateDirectory(Application.persistentDataPath + "/Profile");
        if (false == Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        int widthValue = texture.width;
        int heightValue = texture.height;

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture copiedRenderTexture = new RenderTexture(texture.width, texture.height, 0);

        Graphics.Blit(texture, copiedRenderTexture);
        RenderTexture.active = copiedRenderTexture;

        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRenderTexture;

        byte[] texturePNGBytes = texture2D.EncodeToPNG();
        string filePath = directoryPath + fileName + ".png";

        File.WriteAllBytes(filePath, texturePNGBytes);
    }

    //수정할 프로필 고르기
    public void OnClick_ExitPickProfile()
    {
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(true);
    }

    public void OnClick_PickToOverWriteImage(RawImage picked)
    {
        ui_ProfileOverWrite.gameObject.SetActive(true);
        ui_ProfilePick.gameObject.SetActive(false);
        overWriteImage.texture = picked.texture;

    }
    public void OnClick_PickToOverWriteText(TextMeshProUGUI beforeNickName)
    {
        overWriteNickName.text = beforeNickName.text;

        before.text = beforeNickName.text;
    }

    //프로필 수정부분
    public void OnClick_ExitOverWriteProfile()
    {
        overWriteImage.texture = null;
        overWriteNickName.text = null;
        ui_ProfileOverWrite.gameObject.SetActive(false);
        ui_ProfilePick.gameObject.SetActive(true);
    } // x 버튼 , 취소버튼
    public void OnClick_ChangeProfilePicture()//rawImage 사진나오는곳
    {
        Onclick_LoadImage(overWriteImage);
    }
    public void OnClick_OverWriteDirectory()
    {

        string path = Application.persistentDataPath + "/Profile/";
        
        if (false == Directory.Exists(path)) Directory.CreateDirectory(Application.persistentDataPath + overWriteNickName.text);

        
        Rename(path);
        TobeEmpty(path);
        string savePath = Application.persistentDataPath + "/Profile/" + overWriteNickName.text + "/";
        SaveTextureToPng(overWriteImage.texture, savePath, overWriteNickName.text);

    }
    private void TobeEmpty(string path)
    {
        string fileName = path + overWriteNickName.text;
        Debug.Log(fileName);
        Debug.Log(before.text);

        if (File.Exists(fileName + "/" + before.text + ".png"))
        {
            try
            {
                Debug.Log(fileName + "/" + before.text + ".png");
                File.Delete(fileName + "/" + before.text + ".png");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

        }

    }

    private void Rename(string filepath)
    {
        string oldFile = filepath + "/" + before.text;
        string newFile = filepath + "/" + overWriteNickName.text;

        Directory.Move(oldFile, newFile);

    }

    public void OnClick_DeleteProfile()
    {
        ui_ProfileDelete.gameObject.SetActive(true);
    }
    private void DeleteDirectory()
    {
        string path = Application.persistentDataPath + "/Profile/" + overWriteNickName.text;
        Directory.Delete(path, true);
    }
    public void OnClick_RealDelete()
    {
        DeleteDirectory();
        ui_ProfileDelete.gameObject.SetActive(false);
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfilePlus.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(true);
        ui_ProfileOverWrite.gameObject.SetActive(false);
    }
    #endregion
}

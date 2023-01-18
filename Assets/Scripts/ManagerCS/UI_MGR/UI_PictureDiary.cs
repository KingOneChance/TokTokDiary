using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class UI_PictureDiary : MonoBehaviour
{
    //그림판
    [Header("그림판")]
    [SerializeField] Image ui_DrawBackground = null;
    [SerializeField] Button ui_ShinyWeatherBtn = null;
    [SerializeField] Image ui_ShinyImage = null;
    [Header("카메라")]
    //카메라 on - off, 불러오기
    [SerializeField] Image ui_CameraArea = null;
    [SerializeField] Button ui_CameraOnBtn = null;
    //[SerializeField] Button cameraOffBtn = null;
    [SerializeField] Button ui_LoadBtn = null;
    [Header("기타")]
    //홈버튼
    [SerializeField] Button ui_HomeBtn = null;
    [Header("스티커")]
    [SerializeField] RawImage[] ui_Stickers = null;
    [SerializeField] Button[] ui_StickersBtns = null;

    private Func_Camera func_Camera = null;

    private void Start()
    {
        func_Camera = FindObjectOfType<Func_Camera>();
        Manager_Main.Instance.UI_StickerRepository.OnClick_RepositoryOpen();
    }
    public void OnClick_WaffleRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.waffleList);
    }
    public void OnClick_AudioRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.audioList);
    }
    public void OnClick_WeatherRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.weatherList);
    }
    public void OnClick_DiaryRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.diaryList);
    }
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
    //카메라 켜기
    public void OnClick_CameraOnBtn()
    {
        ui_CameraArea.gameObject.SetActive(true);
        ui_CameraOnBtn.gameObject.SetActive(false);

        func_Camera.CameraOn();
    }
    //카메라 끄기
    public void OnClick_CameraOffBtn()
    {
        ui_CameraArea.gameObject.SetActive(false);
        ui_CameraOnBtn.gameObject.SetActive(true);
    }

    public void OnClick_Weather()
    {
        ui_ShinyImage.color = Color.red;
        
    }

}

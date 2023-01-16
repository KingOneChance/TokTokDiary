using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField] RawImage[] ui_WaffleSticker = null;
    [SerializeField] RawImage test = null;

    private Func_Camera func_Camera = null;
    private Data_LocalSticker data_LocalSticker = null;

    private void Start()
    {
        func_Camera = FindObjectOfType<Func_Camera>();
        data_LocalSticker = Manager_Main.Instance.Data_LocalSticker;
        UpdateStickerInventoryFromList();
    }

    public void UpdateStickerInventoryFromList()
    {
        for (int i = 0; i < data_LocalSticker.waffleList.Count; i++)
        {
            ui_WaffleSticker[i].texture = data_LocalSticker.waffleList[i];
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

    //홈화면
    public void OnClick_Home()
    {
        //SceneManager.LoadScene("Main");
    }

    public void OnClick_Weather()
    {
        ui_ShinyImage.color = Color.red;
        
    }

}

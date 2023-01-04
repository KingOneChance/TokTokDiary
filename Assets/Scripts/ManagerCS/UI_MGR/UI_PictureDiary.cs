using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_PictureDiary : MonoBehaviour
{
    //그림판
    [Header("그림판")]
    [SerializeField] Image drawBackground = null;
    [SerializeField] Button shinyWeatherBtn = null;
    [SerializeField] Image shinyImage = null;
    [Header("카메라")]
    //카메라 on - off, 불러오기
    [SerializeField] Image cameraArea = null;
    [SerializeField] Button cameraOnBtn = null;
    [SerializeField] Button cameraOffBtn = null;
    [SerializeField] Button loadBtn = null;
    [Header("기타")]
    //홈버튼
    [SerializeField] Button homeBtn = null;

    //카메라 켜기
    public void OnClick_CameraOnBtn()
    {
        cameraArea.gameObject.SetActive(true);
        cameraOnBtn.gameObject.SetActive(false);
    }
    //카메라 끄기
    public void OnClick_CameraOffBtn()
    {
        cameraArea.gameObject.SetActive(false);
        cameraOnBtn.gameObject.SetActive(true);
    }

    //사진 불러오기
    public void OnClick_Load()
    {

    }
    //홈화면
    public void OnClick_Home()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClick_Weather()
    {
        shinyImage.color = Color.red;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_PictureDiary : MonoBehaviour
{
    //�׸���
    [Header("�׸���")]
    [SerializeField] Image ui_DrawBackground = null;
    [SerializeField] Button ui_ShinyWeatherBtn = null;
    [SerializeField] Image ui_ShinyImage = null;
    [Header("ī�޶�")]
    //ī�޶� on - off, �ҷ�����
    [SerializeField] Image ui_CameraArea = null;
    [SerializeField] Button ui_CameraOnBtn = null;
    //[SerializeField] Button cameraOffBtn = null;
    [SerializeField] Button ui_LoadBtn = null;
    [Header("��Ÿ")]
    //Ȩ��ư
    [SerializeField] Button ui_HomeBtn = null;

    private CameraMGR camMGR = null;
    private void Start()
    {
        camMGR = FindObjectOfType<CameraMGR>();
    }
    //ī�޶� �ѱ�
    public void OnClick_CameraOnBtn()
    {
        ui_CameraArea.gameObject.SetActive(true);
        ui_CameraOnBtn.gameObject.SetActive(false);

        camMGR.CameraOn();
    }
    //ī�޶� ����
    public void OnClick_CameraOffBtn()
    {
        ui_CameraArea.gameObject.SetActive(false);
        ui_CameraOnBtn.gameObject.SetActive(true);
    }

    //Ȩȭ��
    public void OnClick_Home()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClick_Weather()
    {
        ui_ShinyImage.color = Color.red;
        
    }

}

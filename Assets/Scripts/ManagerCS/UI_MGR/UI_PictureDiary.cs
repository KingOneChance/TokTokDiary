using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_PictureDiary : MonoBehaviour
{
    //�׸���
    [Header("�׸���")]
    [SerializeField] Image drawBackground = null;
    [SerializeField] Button shinyWeatherBtn = null;
    [SerializeField] Image shinyImage = null;
    [Header("ī�޶�")]
    //ī�޶� on - off, �ҷ�����
    [SerializeField] Image cameraArea = null;
    [SerializeField] Button cameraOnBtn = null;
    [SerializeField] Button cameraOffBtn = null;
    [SerializeField] Button loadBtn = null;
    [Header("��Ÿ")]
    //Ȩ��ư
    [SerializeField] Button homeBtn = null;

    //ī�޶� �ѱ�
    public void OnClick_CameraOnBtn()
    {
        cameraArea.gameObject.SetActive(true);
        cameraOnBtn.gameObject.SetActive(false);
    }
    //ī�޶� ����
    public void OnClick_CameraOffBtn()
    {
        cameraArea.gameObject.SetActive(false);
        cameraOnBtn.gameObject.SetActive(true);
    }

    //���� �ҷ�����
    public void OnClick_Load()
    {

    }
    //Ȩȭ��
    public void OnClick_Home()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClick_Weather()
    {
        shinyImage.color = Color.red;
    }

}

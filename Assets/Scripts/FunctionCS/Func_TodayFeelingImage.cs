using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_TodayFeelingImage : MonoBehaviour
{
    [SerializeField] Sprite Change_Image_Excited;
    [SerializeField] Sprite Change_Image_Happy;
    [SerializeField] Sprite Change_Image_Calm;
    [SerializeField] Sprite Change_Image_Sad;
    [SerializeField] Sprite Change_Image_Angry;
    [SerializeField] GameObject TodayFeelingMenu = null;
    Image ThisImage;

    private void Start()
    {
        ThisImage = GetComponent<Image>();
    }

    public void Image_Excited()
    {
        ThisImage.sprite = Change_Image_Excited;
        TodayFeelingMenu.SetActive(false);
    }

    public void Image_Happy()
    {
        ThisImage.sprite = Change_Image_Happy;
        TodayFeelingMenu.SetActive(false);
    }

    public void Image_Calm()
    {
        ThisImage.sprite = Change_Image_Calm;
        TodayFeelingMenu.SetActive(false);
    }

    public void Image_Sad()
    {
        ThisImage.sprite = Change_Image_Sad;
        TodayFeelingMenu.SetActive(false);
    }

    public void Image_Angry()
    {
        ThisImage.sprite = Change_Image_Angry;
        TodayFeelingMenu.SetActive(false);
    }

    public void TodayFeeling_Menu()
    {
        TodayFeelingMenu.SetActive(true);
    }
}

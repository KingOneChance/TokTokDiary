using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_Weather : MonoBehaviour
{
    [Header("날씨 버튼")]
    [SerializeField] public Toggle Button_Shiny;
    [SerializeField] public Toggle Button_Cloudy;
    [SerializeField] public Toggle Button_Rainy;
    [SerializeField] public Toggle Button_Snowy;

    [Header("기본 이미지")]
    [SerializeField] public Image Image_Shiny;
    [SerializeField] public Image Image_Cloudy;
    [SerializeField] public Image Image_Rainy;
    [SerializeField] public Image Image_Snowy;

    [Header("변경 이미지")]
    [SerializeField] public Sprite Sprite_Shiny;
    [SerializeField] public Sprite Sprite_Cloudy;
    [SerializeField] public Sprite Sprite_Rainy;
    [SerializeField] public Sprite Sprite_Snowy;

    [Header("오리지날 이미지")]
    [SerializeField] public Sprite Origin_Shiny;
    [SerializeField] public Sprite Origin_Cloudy;
    [SerializeField] public Sprite Origin_Rainy;
    [SerializeField] public Sprite Origin_Snowy;
    
    private void Update()
    {
         Button_Shiny.onValueChanged.AddListener(ChangeImage_Shiny);
         Button_Cloudy.onValueChanged.AddListener(ChangeImage_Cloudy);
         Button_Rainy.onValueChanged.AddListener(ChangeImage_Rainy);
         Button_Snowy.onValueChanged.AddListener(ChangeImage_Snowy);
    }

   public void ChangeImage_Shiny(bool isOn)
    {
        if (isOn)
        {
            Image_Shiny.sprite = Sprite_Shiny;
            Image_Cloudy.sprite = Origin_Cloudy;
            Image_Rainy.sprite = Origin_Rainy;
            Image_Snowy.sprite = Origin_Snowy;
        }

        else
        {
            Image_Shiny.sprite = Origin_Shiny;
        }
    }
   public void ChangeImage_Cloudy(bool isOn)
   {
        if (isOn)
        {
            Image_Cloudy.sprite = Sprite_Cloudy;
            Image_Shiny.sprite = Origin_Shiny;
            Image_Rainy.sprite = Origin_Rainy;
            Image_Snowy.sprite = Origin_Snowy;
        }

        else
        {
            Image_Cloudy.sprite = Origin_Cloudy;
        }    
   }
   public void ChangeImage_Rainy(bool isOn)
   {
        if (isOn)
        {
            Image_Rainy.sprite = Sprite_Rainy;
            Image_Shiny.sprite = Origin_Shiny;
            Image_Cloudy.sprite = Origin_Cloudy;
            Image_Snowy.sprite = Origin_Snowy;
        }

        else
        {
            Image_Rainy.sprite = Origin_Rainy;
        }
    }
   public void ChangeImage_Snowy(bool isOn)
   {
        if (isOn)
        {
            Image_Snowy.sprite = Sprite_Snowy;
            Image_Shiny.sprite = Origin_Shiny;
            Image_Cloudy.sprite = Origin_Cloudy;
            Image_Rainy.sprite = Origin_Rainy;
        }

        else
        {
            Image_Snowy.sprite = Origin_Snowy;
        }
    }
}

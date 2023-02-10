using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_Weather : MonoBehaviour
{
    [Header("날씨 버튼")]
    [SerializeField] public Button Button_Shiny;
    [SerializeField] public Button Button_Cloudy;
    [SerializeField] public Button Button_Rainy;
    [SerializeField] public Button Button_Snowy;

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

    private bool ImageChanged = false;
    
    private void Update()
    {
         Button_Shiny.onClick.AddListener(ChangeImage_Shiny);
         Button_Cloudy.onClick.AddListener(ChangeImage_Cloudy);
         Button_Rainy.onClick.AddListener(ChangeImage_Rainy);
         Button_Snowy.onClick.AddListener(ChangeImage_Snowy);
    }

   public void ChangeImage_Shiny()
    {
        if (!ImageChanged)
        {
            Image_Shiny.sprite = Sprite_Shiny;
            ImageChanged = true;
        }

        else
        {
            Image_Shiny.sprite = Origin_Shiny;
            ImageChanged = false;
        }
        
    }
   public void ChangeImage_Cloudy()
   {
        if (!ImageChanged)
        {
            Image_Cloudy.sprite = Sprite_Cloudy;
            ImageChanged = true;
        }

        else
        {
            Image_Cloudy.sprite = Origin_Cloudy;
            ImageChanged = false;
        }
        
   }
   public void ChangeImage_Rainy()
   {
        if (!ImageChanged)
        {
            Image_Rainy.sprite = Sprite_Rainy;
            ImageChanged = true;
        }

        else
        {
            Image_Rainy.sprite = Origin_Rainy;
            ImageChanged = false;
        }
    }
   public void ChangeImage_Snowy()
   {
        if (!ImageChanged)
        {
            Image_Snowy.sprite = Sprite_Snowy;
            ImageChanged = true;
        }

        else
        {
            Image_Snowy.sprite = Origin_Snowy;
            ImageChanged = false;
        }
    }
}

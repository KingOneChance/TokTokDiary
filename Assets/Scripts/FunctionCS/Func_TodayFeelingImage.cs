using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FreeDraw
{
    public class Func_TodayFeelingImage : Func_Draw
    {
        [SerializeField] public Sprite Change_Image_Excited;
        [SerializeField] public Sprite Change_Image_Happy;
        [SerializeField] public Sprite Change_Image_Calm;
        [SerializeField] public Sprite Change_Image_Sad;
        [SerializeField] public Sprite Change_Image_Angry;
        [SerializeField] public GameObject TodayFeelingMenu = null;
        [SerializeField] public GameObject Helper;
        [SerializeField] private bool stickerMaking = false;

        static Image ThisImage;
     
        private new void Start()
        {
            ThisImage = GetComponent<Image>();
            base.Start();
        }

        private void Update()
        {
            if (!ActiveTodayFeelingMenu() || Helper.activeSelf)
            {
                if (CheckArea() == true && stickerMaking == false)
                {
                    Draw();
                }

                else
                {
                    internalClick = false;
                }
            }
        }
        public void IsStickerMaking(bool state)
        {
            if (state == true) 
                stickerMaking = true;
            else
                stickerMaking = false;
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
        public bool ActiveTodayFeelingMenu()
        {
            return TodayFeelingMenu.activeSelf;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    public class Func_TodayFeelingImage : MonoBehaviour
    {
        [SerializeField] public Sprite Change_Image_Excited;
        [SerializeField] public Sprite Change_Image_Happy;
        [SerializeField] public Sprite Change_Image_Calm;
        [SerializeField] public Sprite Change_Image_Sad;
        [SerializeField] public Sprite Change_Image_Angry;
        [SerializeField] public GameObject TodayFeelingMenu = null;
        static Image ThisImage;
    
        private void Start()
        {
            ThisImage = GetComponent<Image>();
        }
    
        public void Update()
        {
            if (Input.touchCount > 0)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return;
                else
                {
                    Image_Excited();
                    Image_Happy();
                    Image_Calm();
                    Image_Sad();
                    Image_Angry();
                    TodayFeeling_Menu();
                }
            }
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
}

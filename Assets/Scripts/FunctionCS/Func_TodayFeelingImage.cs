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
        [SerializeField] public LineRenderer lineRenderer; // 라인 렌더러 컴포넌트를 가리키는 변수
        [SerializeField] private Button btn_Eraser = null;
        [SerializeField] private Func_DrawingSettings func_DrawingSettings = null;
        [SerializeField] private GameObject Tutorial = null;
        private bool FeelingMenuOff = false;
        private bool TutorialImage = false;
        
        public void OnButtonClick()
        {
            lineRenderer.startWidth = 1.0f; // 라인 렌더러의 시작 너비를 1로 설정
            lineRenderer.endWidth = 1.0f; // 라인 렌더러의 끝 너비를 1로 설정
        }

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
                    Vector3 curTouchPos_FreeSticker = mainCam.ScreenToWorldPoint(Input.mousePosition);

                    if (FeelingMenuOff && Input.GetMouseButtonUp(0))
                    {
                         
                         if (ThisImage.rectTransform.rect.Contains(curTouchPos_FreeSticker - ThisImage.rectTransform.position))
                         {
                             if (ThisImage.sprite == Change_Image_Excited)
                            {
                                Image_Excited();

                            }
                            else if (ThisImage.sprite == Change_Image_Happy)
                                 Image_Happy();
                             else if (ThisImage.sprite == Change_Image_Calm)
                                 Image_Calm();
                             else if (ThisImage.sprite == Change_Image_Sad)
                                 Image_Sad();
                             else if (ThisImage.sprite == Change_Image_Angry)
                                 Image_Angry();
                         }
                        TodayFeelingMenu.SetActive(false);
                        FeelingMenuOff = false;
                    }
                    else if (!ActiveTodayFeelingMenu() && TutorialImage == false)
                    {
                        Draw();
                    }

                    if (Input.GetMouseButtonUp(0) && curTouchPos_FreeSticker.x < drawingAreaMaxX && curTouchPos_FreeSticker.y < drawingAreaMaxY &&
                       curTouchPos_FreeSticker.x > drawingAreaMinX && curTouchPos_FreeSticker.y > drawingAreaMinY)
                    {
                        // 기존에 쓰는 지우개 함수를 쓰기전용 지우개 함수로 교체한다.
                        btn_Eraser.onClick.RemoveAllListeners();
                        btn_Eraser.onClick.AddListener(func_DrawingSettings.SetEraser);
                    }
                    else if (Input.GetMouseButtonUp(0) && curTouchPos_FreeSticker.x < writingAreaMaxX && curTouchPos_FreeSticker.y < writingAreaMaxY &&
                             curTouchPos_FreeSticker.x > writingAreaMinX && curTouchPos_FreeSticker.y > writingAreaMinY)
                    {
                        btn_Eraser.onClick.RemoveAllListeners();
                        btn_Eraser.onClick.AddListener(func_DrawingSettings.OnClick_WritingEraser);
                        
                    }
                    else if (Input.GetMouseButtonUp(0) && curTouchPos_FreeSticker.x < titleAreaMaxX && curTouchPos_FreeSticker.y < titleAreaMaxY &&
                             curTouchPos_FreeSticker.x > titleAreaMinX && curTouchPos_FreeSticker.y > titleAreaMinY)
                    {
                        btn_Eraser.onClick.RemoveAllListeners();
                        btn_Eraser.onClick.AddListener(func_DrawingSettings.OnClick_TitleEraser);
                       
                    }
                }
                else
                {
                    Manager_Main.Instance.GetAudio().StopPlaySound(gameObject);
                    internalClick = false;
                }
            }
        }

        public void OnClick_Tutorial()
        {
            Tutorial.SetActive(true);
            TutorialImage = true;
        }

        public void OnClick_TutorialMenualOff()
        {
            Tutorial.SetActive(false);
            TutorialImage = false;
        }

        public void IsStickerMaking(bool state)
        {
            if (state == true) 
                stickerMaking = true;
            else
                stickerMaking = false;
            TodayFeelingMenu.SetActive(false);
        }

        public void SetPenWidth(float width)
        {
            base.thinPenWidth = width;
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
            FeelingMenuOff = true;
        }
        public bool ActiveTodayFeelingMenu()
        {
            return TodayFeelingMenu.activeSelf;
        }
    }
}

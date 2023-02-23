using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace FreeDraw
{
    public class Manager_FreeSticker : Func_SaveSticker
    {
        [SerializeField] public RawImage rawImage;
        [SerializeField] public Button SaveButton = null;
        [SerializeField] public GameObject Result = null;
        [SerializeField] public GameObject lineRendererPrefab;
        [SerializeField] private GameObject Lines = null;
        [SerializeField] private MouseType MouseState;
        [SerializeField] Texture2D ui_NiddleImage = null;
        [SerializeField] Texture2D ui_StickImage = null;
        [SerializeField] bool isNiddleClicked = false;
        [SerializeField] private bool isStickClicked = false;
        [SerializeField] private Func_Draw freeStickerDraw;
        [SerializeField] private Camera mainCam = null;
        private Vector2 hotSpot = Vector2.zero;
        private CursorMode cursorMode = CursorMode.Auto;
        public MouseType MouseStateInfo { get { return MouseState; } }
        public float minX, maxX, minY, maxY;

        private void Update()
        {
            saveFileName = DateTime.Now.ToString("yyyy_MM_dd");
        }

        protected override void Start()
        {
            savePath = Application.persistentDataPath;
            //calculate all position
            saveImageRect = saveImage.GetComponent<RectTransform>();
            startXPos = 293f;
            //saveImageRect.rect.position.x + 960 + 73;
            startYPos = 226.5f;
            //saveImageRect.rect.position.y + 540 + 23;
            widthValue = 1290;
            //(int)saveImageRect.rect.width;
            heightValue = 740;
            //(int)saveImageRect.rect.height;
            MouseState = MouseType.None;
        }
    
    //   public void CreateLine()
    //   {
    //       Instantiate(lineRendererPrefab);
    //   }
        public void OnClick_SavefileControl(float scale)
        {
            StartCoroutine(CO_FreeStickerSave(scale));
        }
    
        private IEnumerator CO_FreeStickerSave(float scale)
        {
            isSaveDone = false;
            base.OnClick_SaveImgae(StickerType.FreeSticker);
            yield return new WaitUntil(() => isSaveDone == true);
            // raw 이미지의 스케일 값을 조절
            Lines.SetActive(false);
            rawImage.transform.localPosition = new Vector3(0, 0, 0);
            rawImage.transform.localScale = new Vector3(scale, scale, 1f);
            Invoke("OnClick_Result", 0.1f);
        } 

        public void OnClick_Result()
        {
            freeStickerDraw.StopDraw(true);
            Debug.Log("스톱");
            // 이미지의 SetActive 값을 반전시킴
            Result.SetActive(!Result.activeSelf);

            //Destroy(Func_Draw.Draw());
        }

        public void OnClick_NiddleButton()
        {
            if (MouseState != MouseType.Niddle)
            {
                Cursor.SetCursor(ui_NiddleImage, hotSpot, cursorMode);
                isNiddleClicked = true;
                MouseState = MouseType.Niddle;
            }
            else
            {
                Cursor.SetCursor(default, hotSpot, cursorMode);
                isNiddleClicked = false;
                MouseState = MouseType.None;
            }
        }
        public void OnClick_BubbleStick()
        {
            if (MouseState != MouseType.BubbleStick)
            {
                Cursor.SetCursor(ui_StickImage, hotSpot, cursorMode);
                isStickClicked = true;
                MouseState = MouseType.BubbleStick;
            }
            else
            {
                Cursor.SetCursor(default, hotSpot, cursorMode);
                isStickClicked = false;
                MouseState = MouseType.None;
            }
        }
    }
}

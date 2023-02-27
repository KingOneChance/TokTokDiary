using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FreeDraw
{   
    public class Func_DetectInFreeSticker : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Func_DragObject_FreeSticker func_DragObject_FreeSticker = null; //드래그할거 만들고
        [SerializeField] private Manager_FreeSticker manager_FreeSticker = null;
        [SerializeField] private RawImage sticker = null;
        [SerializeField] private RawImage bubble = null;
        [SerializeField] private RawImage sign = null;

        [SerializeField] private ParticleSystem eff_BubblePop = null;

        private void PlayBubblePop(Vector2 myPosInScreen)
        {
            Manager_Main.Instance.GetAudio().PlaySound("PopBubble", SoundType.Common, gameObject, false, true);
            eff_BubblePop.transform.position = new Vector3(myPosInScreen.x, myPosInScreen.y, eff_BubblePop.transform.position.z);
            eff_BubblePop.Play();
        }

        private void Start()
        {
            manager_FreeSticker = FindObjectOfType<Manager_FreeSticker>();
    
            sticker = gameObject.GetComponent<RawImage>();
            sign = gameObject.transform.GetChild(0).GetComponent<RawImage>();
            bubble = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>();
            if (sign.texture == null)
                sign.color = new Color(255, 255, 255, 0);
        }
        public void OnClick_MouseType()
        {

            Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
            if (worldpos.x < 0f) worldpos.x = 0f;
            if (worldpos.y < 0f) worldpos.y = 0f;
            if (worldpos.x > 1f) worldpos.x = 1f;
            if (worldpos.y > 1f) worldpos.y = 1f;
            
            this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);

            if (manager_FreeSticker.MouseStateInfo == MouseType.Niddle)
            {
                func_DragObject_FreeSticker.enabled = false; //드래그할 스크립트 켜고 끄면서 움직임 제어
                PlayBubblePop(new Vector2(bubble.transform.position.x, bubble.transform.position.y));
                bubble.gameObject.SetActive(false);
            }
            else if (manager_FreeSticker.MouseStateInfo == MouseType.BubbleStick)
            {
                func_DragObject_FreeSticker.enabled = true;
                bubble.gameObject.SetActive(true);
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick_MouseType();
        }
    }
}
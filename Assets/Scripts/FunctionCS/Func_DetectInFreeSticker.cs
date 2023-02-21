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
                Debug.Log("바늘상태");
                func_DragObject_FreeSticker.enabled = false; //드래그할 스크립트 켜고 끄면서 움직임 제어
                bubble.gameObject.SetActive(false);
            }
            else if (manager_FreeSticker.MouseStateInfo == MouseType.BubbleStick)
            {
                Debug.Log("방울상태");
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
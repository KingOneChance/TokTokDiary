using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class UI_CreateSticker : MonoBehaviour
{
    //그림판
    [Header("그림판")]
    [SerializeField] Image ui_DrawBackground = null;
    [Header("기타")]
    //홈버튼
    [SerializeField] Button ui_HomeBtn = null;
    [Header("스티커")]
    [SerializeField] RawImage[] ui_Stickers = null;
    [SerializeField] Button[] ui_StickersBtns = null;
}

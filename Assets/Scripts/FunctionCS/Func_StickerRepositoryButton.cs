using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_StickerRepositoryButton : MonoBehaviour
{
    public void OnClick_ButtonOn() 
    {
        Manager_Main.Instance.OnClick_StickerRepositoryOn();
        //Manager_Main.Instance.UI_StickerRepository.OnClick_RepositoryOpen();
    }
}

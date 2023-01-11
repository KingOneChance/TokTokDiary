using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Main : MonoBehaviour
{
    [SerializeField] Button ui_toPictureDiary = null;
    [SerializeField] Button ui_toStudio = null;
   
    public void OnClick_ToPictureDiary()
    {
        SceneManager.LoadScene("PictureDiary");
    }
    public void OnClick_ToStudio()
    {
        SceneManager.LoadScene("CreateSticker");
      
    }


}

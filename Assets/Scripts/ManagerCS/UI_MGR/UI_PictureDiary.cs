using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_PictureDiary : MonoBehaviour
{
    //Ä«¸Þ¶ó on - off
    [SerializeField] Image cameraArea = null;
    [SerializeField] Button cameraOnBtn = null;
    [SerializeField] Button cameraOffBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        //cameraArea.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick_CameraOnBtn()
    {
        cameraArea.gameObject.SetActive(true);
        cameraOnBtn.gameObject.SetActive(false);
    }

    public void OnClick_CameraOffBtn()
    {
        cameraArea.gameObject.SetActive(false);
        cameraOnBtn.gameObject.SetActive(true);
    }
}

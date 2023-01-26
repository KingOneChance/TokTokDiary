using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_OnTriggerStay : MonoBehaviour
{
    private float flowTime = 0;

    [SerializeField] RectTransform[] positions = null;
    [SerializeField] private Sprite image = null;

     private GameObject go = null;
    private Color hideImgColor = new Color(0, 0, 0, 0);
    private Color showImgColor = new Color(255, 255, 255, 255);
    public int ranNum = 0;
    private void Start()
    {
        ranNum = Random.Range(0, positions.Length);
        go = new GameObject();
        go.name = "HideSticker";
        go.tag = "Find";

        go.AddComponent<RawImage>().texture = image.texture;
        go.GetComponent<RawImage>().color = hideImgColor;
        go.transform.SetParent(positions[ranNum]);
        go.transform.position = positions[ranNum].transform.position;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Image") || collision.CompareTag("Find"))
        {
            flowTime += Time.deltaTime;
            if (flowTime > 2.0f)
            {
                go.GetComponent<RawImage>().color = showImgColor;
                if(go.GetComponent<RawImage>().color == showImgColor)
                {
                    //이미지 인벤토리 추가
                    Debug.Log("펑");
                }
                flowTime = 0;
                
                collision.GetComponent<Image>().color = hideImgColor;
            }
        }
        else
        {
            flowTime = 0;
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Image")
        {
            flowTime = 0;
        }
    }
}

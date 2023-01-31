using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_OnTriggerStay : MonoBehaviour
{
    //¸ÞÀÎ Äµ¹ö½º, ¼­ºê Äµ¹ö½º
    [SerializeField] private Canvas mainCanvas = null;
    [SerializeField] private Canvas subCanvas = null;

    private float flowTime = 0;

    [SerializeField] RectTransform[] positions = null;
    [SerializeField] private Sprite[] bubbleGunImage = null;
    [SerializeField] Image[] boards = null;
    

    public GameObject go = null;
    private Color hideImgColor = new Color(0, 0, 0, 0);
    private Color showImgColor = new Color(255, 255, 255, 255);
    private int ranPosNum = 0;
    private int ranImageNum = 0;

    private void OnEnable()
    {
        for(int i = 0; i < boards.Length; i++)
        {
            boards[i].color = showImgColor;
        }
        ranImageNum = Random.Range(0, bubbleGunImage.Length);
        ranPosNum = Random.Range(0, positions.Length);
        go = new GameObject();
        go.name = "HideSticker";
        go.tag = "Find";

        go.AddComponent<RawImage>().texture = bubbleGunImage[ranImageNum].texture;
        go.GetComponent<RawImage>().color = hideImgColor;
        go.transform.SetParent(positions[ranPosNum]);
        go.transform.position = positions[ranPosNum].transform.position;
    }
    private void OnDisable()
    {
        Destroy(go);
    }
    private void Start()
    {
        subCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
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
                    Debug.Log("Æã");

                    subCanvas.gameObject.SetActive(true);
                    mainCanvas.gameObject.SetActive(false);
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

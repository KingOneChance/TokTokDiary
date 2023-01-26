using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_BubbleBubble : MonoBehaviour
{
    [SerializeField] RectTransform[] positions = null;
    [SerializeField] private Sprite image = null;

    GameObject go = null;
    Color hideImgColor = new Color(0, 0, 0, 0);
    Color showImgColor = new Color(255, 255, 255, 255);
    public int ranNum = 0;
    void Start()
    {
        ranNum = Random.Range(0, positions.Length);
        go = new GameObject();
        go.name = "HideSticker";
        go.AddComponent<RawImage>().texture = image.texture;
        go.GetComponent<RawImage>().color = hideImgColor;
        go.transform.parent = positions[ranNum];
        go.transform.position = positions[ranNum].transform.position;
    
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_GunCollision : MonoBehaviour
{
    [SerializeField] private RawImage[] blacks = null;
    [SerializeField] private RawImage backGroundSticker = null;
    [SerializeField] private RawImage stickerBackGround = null;


    private Color dirtyColor = new Color(0, 0, 0, 255 / 255);
    private Color middleColor = new Color(0, 0, 0, 180f / 255f );
    private Color cleanColor = new Color(1, 1, 1, 1);

    bool allClear = false;

    List<RawImage> blackList = new List<RawImage>();
    List<RawImage> whiteList = new List<RawImage>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<RawImage>().color == dirtyColor)
        {
            collision.gameObject.GetComponent<RawImage>().color = middleColor;
            blackList.Add(collision.gameObject.GetComponent<RawImage>());
            if (blackList.Count == 28)
            {
                allClear = true;
            }
        }
        else if (collision.gameObject.GetComponent<RawImage>().color == middleColor && allClear == true)
        {
            collision.gameObject.GetComponent<RawImage>().color = cleanColor;
            whiteList.Add(collision.gameObject.GetComponent<RawImage>());
            if (whiteList.Count == 28)
            {
                for (int i = 0; i < blacks.Length; i++)
                {
                    blacks[i].gameObject.SetActive(false);
                    stickerBackGround.gameObject.SetActive(true);

                    StartCoroutine(DisappearSticker());
                }
            }
        }

    }

    IEnumerator DisappearSticker()
    {
        yield return new WaitForSeconds(10f);
        stickerBackGround.gameObject.SetActive(false);
    }
}

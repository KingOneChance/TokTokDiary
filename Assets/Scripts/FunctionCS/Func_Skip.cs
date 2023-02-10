using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_Skip : MonoBehaviour
{
    [SerializeField] RawImage[] checkImages = null;

    private Color middleColor = new Color(0, 0, 0, 180f / 255f);
    private Color cleanColor = new Color(1, 1, 1, 1);
    private Color Nothing = new Color(255 / 255, 255 / 255, 255 / 255, 0);

    Func_GunCollision func_GunCollision = null;
    private void Start()
    {
        func_GunCollision = FindObjectOfType<Func_GunCollision>();
    }

    public void OnClick_SkipRoundOne()
    {
        for(int i = 0; i < checkImages.Length; i++)
        {
            if(checkImages[i].color == middleColor)
            { 
                func_GunCollision.whiteList.Add(checkImages[i]);
            }
        }
        StartCoroutine(WaitlittleTime());
        
    }

    IEnumerator WaitlittleTime()
    {
        for (int i = 0; i < func_GunCollision.whiteList.Count; i++)
        {
            func_GunCollision.whiteList[i].color = cleanColor;
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void OnClick_SkipRoundTwo()
    {
        for (int i = 0; i < func_GunCollision.whiteList.Count; i++)
        {
            if (checkImages[i].color == cleanColor) 
            {
                func_GunCollision.cleanList.Add(checkImages[i]);
            }
        }
        StartCoroutine(WaitlittleTimePop());

    }
    IEnumerator WaitlittleTimePop()
    {
        for (int i = 0; i < func_GunCollision.cleanList.Count; i++)
        { yield return new WaitForSeconds(0.3f);
            func_GunCollision.cleanList[i].color = Nothing ;
            func_GunCollision.cleanList[i].gameObject.GetComponent<RawImage>().texture = null;
           
        }
        func_GunCollision.RoundFinish();
    }
}

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
        StartCoroutine(WaitlittleTime());
        
    }

    IEnumerator WaitlittleTime()
    {
        for (int i = 0; i < func_GunCollision.whiteList.Count; i++)
        {
            if (func_GunCollision.whiteList[i].color == middleColor)
            {
                func_GunCollision.whiteList[i].color = cleanColor;
            }
            yield return new WaitForSeconds(0f);
        }

        func_GunCollision.whiteList.Clear();
        func_GunCollision.allPop = true;
    }
    public void OnClick_SkipRoundTwo()
    {
        StartCoroutine(WaitlittleTimePop()); 
    }
    IEnumerator WaitlittleTimePop()
    {
        for (int i = 0; i < func_GunCollision.cleanList.Count; i++)
        {
            if (func_GunCollision.cleanList[i].color == cleanColor)
            {
                func_GunCollision.cleanList[i].color = Nothing;
            }
            yield return new WaitForSeconds(0.2f);
        }

        func_GunCollision.cleanList.Clear();         
        func_GunCollision.RoundFinish();
        
    }
}

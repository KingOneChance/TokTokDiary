using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_Skip : MonoBehaviour
{
    [SerializeField] RawImage[] checkImages = null;


    private Color Nothing = new Color(255 / 255, 255 / 255, 255 / 255, 0);

    private Func_GunCollision func_GunCollision = null;
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
        for (int i = 0; i < checkImages.Length; i++)
        {
            if (checkImages[i].texture.name.Contains("messy"))
            {
                func_GunCollision.ChangeImage(checkImages[i].texture.name, checkImages[i].gameObject);
                //이펙트 및 스킵 사운드
            }
            yield return new WaitForSeconds(0f);
        }

        func_GunCollision.whiteList.Clear();
        func_GunCollision.AllPopChange();

    }
    public void OnClick_SkipRoundTwo()
    {
        StartCoroutine(WaitlittleTimePop()); 
    }
    IEnumerator WaitlittleTimePop()
    {
        while(func_GunCollision.cleanList.Count != 0)
        {
            int randnum =Random.Range(0, func_GunCollision.cleanList.Count);
            
            if (func_GunCollision.cleanList[randnum].texture.name.Contains("clean") && func_GunCollision.cleanList[randnum].gameObject.activeSelf== true)
            {
                func_GunCollision.cleanList[randnum].color = Nothing;
                func_GunCollision.cleanList.RemoveAt(randnum);
                //이펙트 및 비눗방울 터지는 사운드
            }
            yield return new WaitForSeconds(0.2f);
        }
        func_GunCollision.cleanList.Clear();         
        func_GunCollision.RoundFinish();
        
    }
}

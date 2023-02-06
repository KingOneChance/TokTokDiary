using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_Skip : MonoBehaviour
{
    [SerializeField] RawImage[] checkImages = null;

    private List<RawImage> checkList = new List<RawImage>();

    private Color middleColor = new Color(0, 0, 0, 180f / 255f);
    private Color cleanColor = new Color(1, 1, 1, 1);

    Func_GunCollision func_GunCollision = null;
    private void Start()
    {
        func_GunCollision = FindObjectOfType<Func_GunCollision>();
    }

    public void OnClick_SkipRound()
    {
        for(int i = 0; i < checkImages.Length; i++)
        {
            if(checkImages[i].color == middleColor)
            {
                checkList.Add(checkImages[i]);
            }
        }
        StartCoroutine(WaitlittleTime());
        
    }

    IEnumerator WaitlittleTime()
    {
        for (int i = 0; i < checkList.Count; i++)
        {
            checkList[i].color = cleanColor;
            yield return new WaitForSeconds(0.3f);
        }
        func_GunCollision.RoundFinish();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_PlayEff : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] eff_GetBubbleSticker = null;

    private void OnEnable()
    {
        StartCoroutine(CO_Bomb());
    }

    private IEnumerator CO_Bomb()
    {
        Manager_Main.Instance.GetAudio().PlaySound("Fanfare", SoundType.Common, gameObject, false, true);
        for (int i = 0; i < eff_GetBubbleSticker.Length; ++i)
        {
            eff_GetBubbleSticker[i].Play();
        }

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < eff_GetBubbleSticker.Length; ++i)
        {
            eff_GetBubbleSticker[i].Clear(true);
        }
    }
}

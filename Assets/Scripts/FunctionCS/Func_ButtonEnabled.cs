using UnityEngine;
using UnityEngine.UI;

public class Func_ButtonEnabled : MonoBehaviour
{
    private Button myButton = null;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        myButton.interactable = true;
        if (Manager_Main.Instance.GetAudio() == null) return;
        Manager_Main.Instance.GetAudio().PlaySound("ComeBack", SoundType.Touch, gameObject, false, true);
    }

    private void OnDisable()
    {
        myButton.interactable = false;
    }
}

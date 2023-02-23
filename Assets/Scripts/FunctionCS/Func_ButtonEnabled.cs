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
        Manager_Main.Instance.GetAudio().PlaySound("ComeBack", SoundType.Touch, gameObject, false, true);
    }

    private void OnDisable()
    {
        myButton.interactable = false;
    }
}

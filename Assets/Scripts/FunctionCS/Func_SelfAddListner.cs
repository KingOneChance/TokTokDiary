using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Func_SelfAddListner : MonoBehaviour
{
    [SerializeField] private Button myButton = null;
    public void Start()
    {
        myButton = GetComponent<Button>();
    }
    public void SetLissner(string myPath)
    {
        if (myButton == null)
            myButton = GetComponent<Button>();
        if (myButton.onClick != null)
            myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(() =>
             Manager_Main.Instance.GetAudio().PlayLocalSound(myPath, gameObject, false, false)
         );
    }
}

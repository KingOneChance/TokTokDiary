using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_BubbleBubble : MonoBehaviour
{
    //Ȩ��ư
    [SerializeField] Button ui_HomeBtn = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Ȩȭ��
    public void OnClick_Home()
    {
        SceneManager.LoadScene("Main");
    }
}

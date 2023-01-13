using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_BubbleBubble : MonoBehaviour
{
    //홈버튼
    [SerializeField] Button ui_HomeBtn = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //홈화면
    public void OnClick_Home()
    {
        SceneManager.LoadScene("Main");
    }
}

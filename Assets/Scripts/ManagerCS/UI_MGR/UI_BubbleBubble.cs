using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_BubbleBubble : MonoBehaviour
{
    //홈버튼
    [SerializeField] Button ui_HomeBtn = null;
    //BubbleGun Image
    [SerializeField] RectTransform ui_BubbleGunRect = null;
    [SerializeField] Button ui_BubbleGunBtn = null;

    Vector2 startPos = Vector3.zero;
    private bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = ui_BubbleGunRect.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked == false) return;
        Update_MousePosition();
    }
    
    //홈화면
    public void OnClick_Home()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnClick_BubbleGun()
    {
        if(isClicked == false)
        {
            isClicked = true;
        }
        else
        {
            ui_BubbleGunRect.transform.position = startPos;
            isClicked = false;
        }
        Init_Cursor();
       
    }
    public void Init_Cursor()
    {
        //Cursor.visible = false;

        if (ui_BubbleGunRect.GetComponent<Graphic>())
            ui_BubbleGunRect.GetComponent<Graphic>().raycastTarget = false;
    }

    public void Update_MousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        float w = ui_BubbleGunRect.rect.width;
        float h = ui_BubbleGunRect.rect.height;
        ui_BubbleGunRect.position = mousePos /*+ (new Vector2(w, h) * 0.5f)*/;

    }

}

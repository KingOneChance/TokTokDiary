using UnityEngine;
using UnityEngine.UI;

public class Manager_BubbleSticker : MonoBehaviour
{
    [SerializeField] RawImage SelectedDesign = null;
    [SerializeField] RawImage SelectedColor = null;

    [SerializeField] Texture[] Designs = null;
    [SerializeField] Color[] paletteColors = null;

    [SerializeField] Button DecideBtn = null;

    private bool isClickedDesign = false;
    private bool isClickedColor = false;

    public void OnClick_ColorBtn(int idx)
    {
        switch (idx)
        {
            case 0:
                SelectedColor.color = paletteColors[0];
                break;
            case 1:
                SelectedColor.color = paletteColors[1];
                break;
            case 2:
                SelectedColor.color = paletteColors[2];
                break;
            case 3:
                SelectedColor.color = paletteColors[3];
                break;
            case 4:
                SelectedColor.color = paletteColors[4];
                break;
            case 5:
                SelectedColor.color = paletteColors[5];
                break;
            case 6:
                SelectedColor.color = paletteColors[6];
                break;
            case 7:
                SelectedColor.color = paletteColors[7];
                break;
            case 8:
                SelectedColor.color = paletteColors[8];
                break;
            case 9:
                SelectedColor.color = paletteColors[9];
                break;
        }
        if (isClickedColor == false)
            isClickedColor = true;
    }

    public void OnClick_DesignBtn(int idx)
    {
        switch (idx)
        {
            case 0:
                SelectedDesign.texture = Designs[0];
                break;
            case 1:
                SelectedDesign.texture = Designs[1];
                break;
            case 2:
                SelectedDesign.texture = Designs[2];
                break;
            case 3:
                SelectedDesign.texture = Designs[3];
                break;
            case 4:
                SelectedDesign.texture = Designs[4];
                break;
        }
        if (isClickedDesign == false)
            isClickedDesign = true;
    }

    public void OnClick_DecideBtn()
    {
        if(isClickedColor == true && isClickedDesign == true)
        {
            Debug.Log("Create Sticker");
        }
        else
        {
            Debug.Log("There are some that haven't been selected yet, so choose them");
        }
    }
}

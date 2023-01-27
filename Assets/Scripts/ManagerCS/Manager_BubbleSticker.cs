using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Manager_BubbleSticker : MonoBehaviour
{
    [SerializeField] private Func_Tilt Bucket = null;
    private CreateStickerState _state;

    public void OnClick_NextBtn(CreateStickerState nextState)
    {
        Debug.Log("Back!! Back!!");
    }

    public void OnClick_BackBtn(CreateStickerState prevState)
    {
        Debug.Log("Next!! Next!!");
    }

    private void Update()
    {
        switch (Manager_UserInput.curInputState)
        {
            case UserInputState.TouchStationary:
                Bucket.StartTilt(true);
                break;

            case UserInputState.TouchCanceled:
            case UserInputState.TouchEnded:
                Bucket.StartTilt(false);
                break;
        }
    }

    //[SerializeField] RectTransform DesignPosAfterClickDecide = null;
    //[SerializeField] RectTransform ColorPosAfterClickDecide = null;

    //[SerializeField] RawImage SelectedDesign = null;
    //[SerializeField] RawImage SelectedColor = null;

    //[SerializeField] Texture[] Designs = null;
    //[SerializeField] Color[] paletteColors = null;
    //[SerializeField] Animator DesignAnim = null;
    //[SerializeField] Animator ColorAnim = null;

    //private bool isClickedDesign = false;
    //private bool isClickedColor = false;

    //private Color showColor = new Color(255,255,255,255);

    //private CreateStickerState curState;

    //private void Start()
    //{
    //    curState = CreateStickerState.Select;
    //}

    //public void OnClick_ColorBtn(int idx)
    //{
    //    switch (idx)
    //    {
    //        case 0:
    //            SelectedColor.color = paletteColors[0];
    //            break;
    //        case 1:
    //            SelectedColor.color = paletteColors[1];
    //            break;
    //        case 2:
    //            SelectedColor.color = paletteColors[2];
    //            break;
    //        case 3:
    //            SelectedColor.color = paletteColors[3];
    //            break;
    //        case 4:
    //            SelectedColor.color = paletteColors[4];
    //            break;
    //        case 5:
    //            SelectedColor.color = paletteColors[5];
    //            break;
    //        case 6:
    //            SelectedColor.color = paletteColors[6];
    //            break;
    //        case 7:
    //            SelectedColor.color = paletteColors[7];
    //            break;
    //        case 8:
    //            SelectedColor.color = paletteColors[8];
    //            break;
    //        case 9:
    //            SelectedColor.color = paletteColors[9];
    //            break;
    //    }
    //    if (isClickedColor == false)
    //        isClickedColor = true;
    //}

    //public void OnClick_DesignBtn(int idx)
    //{
    //    switch (idx)
    //    {
    //        case 0:
    //            SelectedDesign.texture = Designs[0];
    //            break;
    //        case 1:
    //            SelectedDesign.texture = Designs[1];
    //            break;
    //        case 2:
    //            SelectedDesign.texture = Designs[2];
    //            break;
    //        case 3:
    //            SelectedDesign.texture = Designs[3];
    //            break;
    //        case 4:
    //            SelectedDesign.texture = Designs[4];
    //            break;
    //    }
    //    SelectedDesign.color = showColor;
    //    if (isClickedDesign == false)
    //        isClickedDesign = true;
    //}

    //public void OnClick_DecideBtn()
    //{
    //    if(isClickedColor == true && isClickedDesign == true)
    //    {
    //        DesignAnim.enabled = true;
    //        ColorAnim.enabled = true;
    //        Debug.Log("Create Sticker");
    //    }
    //    else
    //    {
    //        Debug.Log("There are some that haven't been selected yet, so choose them");
    //    }
    //}

    //private void ChangeState(CreateStickerState state)
    //{
    //    curState = state;
    //    StartCoroutine("CO_" + state.ToString());
    //}

    //IEnumerator CO_Select()
    //{
    //    yield return null;
    //}

    //IEnumerator CO_Decide()
    //{
    //    yield return null;
    //}

    //IEnumerator CO_Create()
    //{
    //    yield return null;
    //}
}

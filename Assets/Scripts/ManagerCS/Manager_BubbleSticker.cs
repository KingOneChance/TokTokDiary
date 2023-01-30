using UnityEngine;
using UnityEngine.UI;

public class Manager_BubbleSticker : MonoBehaviour
{
    private CreateStickerState _state;

    [SerializeField] private bool isDefaultBottleSelected = false;

    [SerializeField] private RectTransform[] bucketPos = null;

    [SerializeField] private RectTransform myPos = null;

    [SerializeField] private RawImage[] ColorBuckets = null;

    [SerializeField] private Button[] ColorButtons = null;

    [SerializeField] private RawImage myColor = null;

    [SerializeField] private Func_Tilt myTilt = null;

    private void Start()
    {
        myTilt.enabled = false;
        myPos.transform.position = bucketPos[0].transform.position;
        for (int i = 0; i < ColorButtons.Length; i++)
        {
            ColorButtons[i].interactable = false;
        }
    }

    public void OnClick_NextBtn(CreateStickerState nextState)
    {
        Debug.Log("Back!! Back!!");
    }

    public void OnClick_BackBtn(CreateStickerState prevState)
    {
        Debug.Log("Next!! Next!!");
    }

    public void OnClick_DefaultBottle()
    {
        isDefaultBottleSelected = !isDefaultBottleSelected;
        if(isDefaultBottleSelected == true)
        {
            myPos.transform.position = bucketPos[1].transform.position;
            myTilt.enabled = true;
            for (int i = 0; i < ColorButtons.Length; i++)
            {
                ColorButtons[i].interactable = true;
            }
        }
        else
        {
            myPos.transform.position = bucketPos[0].transform.position;
            myTilt.enabled = false;
            for (int i = 0; i < ColorButtons.Length; i++)
            {
                ColorButtons[i].interactable = false;
            }
        }
    }

    public void OnClick_SelectColor(string color)
    {
        switch (color)
        {
            case "Yellow":
                myColor.color = ColorBuckets[0].color;
                break;

            case "Orange":
                myColor.color = ColorBuckets[1].color;
                break;

            case "Purple":
                myColor.color = ColorBuckets[2].color;
                break;
        }
    }
}

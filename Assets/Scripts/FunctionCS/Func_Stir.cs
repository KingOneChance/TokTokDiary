using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Func_Stir : MonoBehaviour
{
    [SerializeField] private RectTransform myRect = null;
    [SerializeField] private RectTransform stickInitPos = null;
    [SerializeField] private Button stickButton = null;
    [SerializeField] private Button NextButton = null;
    [SerializeField] private Button SkipButton = null;
    private float radius = 0;
    private float deg = 0;
    private float stirSpeed = 0;
    private int stirCount = 0;

    public void OnClick_BubbleStick() => StartCoroutine(Stir());

    private void Start()
    {
        radius = 30f;
        stirSpeed = 500f;
    }

    private void OnEnable()
    {
        stickButton.enabled = true;
        myRect.position = stickInitPos.position;
        SkipButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        stirCount = 0;
    }

    private IEnumerator Stir()
    {
        stickButton.enabled = false;
        float rad = 0f;
        float xCoord = 0f;
        float yCoord = 0f;
        while (true)
        {
            deg -= Time.deltaTime * stirSpeed;
            if (deg > -360)
            {
                rad = Mathf.Deg2Rad * (deg);
                xCoord = radius * Mathf.Cos(rad);
                yCoord = radius * Mathf.Sin(rad);
                myRect.anchoredPosition = myRect.anchoredPosition + new Vector2(-1 * xCoord, yCoord);
            }
            else
            {
                deg = 0;
                stickButton.enabled = true;
                stirCount++;
                if(stirCount == 3)
                {
                    NextButton.interactable = true;
                    stickButton.enabled = false;
                }
                yield break;
            }
            yield return null;
        }
    }

    public void OnClick_SkipButton()
    {
        NextButton.interactable = true;
        stickButton.enabled = false;
    }
}

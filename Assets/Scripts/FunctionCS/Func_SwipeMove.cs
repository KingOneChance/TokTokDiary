using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Func_SwipeMove : MonoBehaviour
{
    [SerializeField] private RawImage twinkleImg = null;
    [SerializeField] private Scrollbar scrollBar = null;
    [SerializeField] private float swipeTime = 0.2f;
    [SerializeField] private float swipeDistance = 50.0f;
    [SerializeField] private int currentSticker = 0;

    public int CurrentSticker { get { return currentSticker; } private set { } }
    private int maxPage = 0;
    private float[] scrollpageValues = null;
    private float valueDistance = 0;
    private float startTouchX = 0f;
    private float endTouchX = 0f;
    private bool isSwipeMode = false;

    private void Awake()
    {
        scrollpageValues = new float[transform.childCount];
        valueDistance = 1f / (scrollpageValues.Length - 1f);

        for(int i = 0; i < scrollpageValues.Length; ++i)
        {
            scrollpageValues[i] = valueDistance * i;
        }

        maxPage = transform.childCount;
    }

    private void OnEnable()
    {
        SetScrollBarValue(0);
        StartCoroutine(OnSwipeOneStep(currentSticker));
        StartCoroutine(Twinkle());
    }

    public void SetScrollBarValue(int index)
    {
        currentSticker = index;
        scrollBar.value = scrollpageValues[index];
    }

    private void Update()
    {
        if (isSwipeMode == true) return;

        if (Input.GetMouseButtonDown(0))
            startTouchX = Input.mousePosition.x;
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }
    }

    private void UpdateSwipe()
    {
        if(Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentSticker));
            return;
        }

        bool isLeft = startTouchX < endTouchX ? true : false;

        if( isLeft == true)
        {
            if (currentSticker == 0) return;
            currentSticker--;
        }

        else
        {
            if (currentSticker == maxPage - 1) return;
            currentSticker++;
        }

        StartCoroutine(OnSwipeOneStep(currentSticker));
    }

    private IEnumerator OnSwipeOneStep(int index)
    {
        float start = scrollBar.value;
        float current = 0;
        float percent = 0;

        isSwipeMode = true;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;
            scrollBar.value = Mathf.Lerp(start, scrollpageValues[index], percent);
            yield return null;
        }

        isSwipeMode = false;
    }

    private IEnumerator Twinkle()
    {
        float fadeAmount = 0;
        while (true)
        {
            while (fadeAmount > 0f)
            {
                fadeAmount -= 0.1f;
                yield return new WaitForFixedUpdate();
                twinkleImg.color = new Color(1, 1, 1, fadeAmount);
            }
            while (fadeAmount < 1.0f)
            {
                fadeAmount += 0.1f;
                yield return new WaitForFixedUpdate();
                twinkleImg.color = new Color(1, 1, 1, fadeAmount);
            }
        }
    }
}
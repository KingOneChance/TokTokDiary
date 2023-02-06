using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_SwipeMove : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollBar = null;
    [SerializeField] private float swipeTime = 0.2f;
    [SerializeField] private float swipeDistance = 50.0f;

    private float[] scrollpageValues = null;
    private float valueDistance = 0;
    private int currentSticker = 0;
    public int CurrentSticker { get { return currentSticker; } private set { } }
    private int maxPage = 0;
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

    private void Start()
    {
        SetScrollBarValue(0);
    }

    public void SetScrollBarValue(int index)
    {
        currentSticker = index;
        scrollBar.value = scrollpageValues[index];
    }

    private void Update()
    {
        if (isSwipeMode == true) return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            startTouchX = Input.mousePosition.x;
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }
#endif

#if UNITY_ANDROID
        if (Manager_UserInput.touchCount == 1)
        {
            if (Manager_UserInput.touches[0].phase == TouchPhase.Began)
                startTouchX = Manager_UserInput.touches[0].position.x;
            if (Manager_UserInput.touches[0].phase == TouchPhase.Ended)
            {
                endTouchX = Manager_UserInput.touches[0].position.x;
                UpdateSwipe();
            }
        }
#endif
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
}
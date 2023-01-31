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
    private int currentPage = 0;
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
        currentPage = index;
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
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                startTouchX = touch.position.x;
            else if ( touch.phase == TouchPhase.Ended)
            {
                endTouchX = touch.position.x;
                UpdateSwipe();
            }
        }
#endif
    }

    private void UpdateSwipe()
    {
        if(Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }

        bool isLeft = startTouchX < endTouchX ? true : false;

        if( isLeft == true)
        {
            if (currentPage == 0) return;
            currentPage--;
        }

        else
        {
            if (currentPage == maxPage - 1) return;
            currentPage++;
        }

        StartCoroutine(OnSwipeOneStep(currentPage));
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
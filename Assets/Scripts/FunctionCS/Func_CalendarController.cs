using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Func_CalendarController : MonoBehaviour
{
    const int _totalDateNum = 42;

    [SerializeField] private GameObject _item;
    [SerializeField] private GameObject _itemSticker;
    [SerializeField] private TextMeshProUGUI _yearNumText;
    [SerializeField] private TextMeshProUGUI _monthNumText;
    [SerializeField] private TextMeshProUGUI _monthStrText;
    [SerializeField] private List<GameObject> _dateItems = new List<GameObject>();

    private DateTime _dateTime;

    private string[] monthStr = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    private string myName = "";

    private List<int> days = null;

    Manager_DiaryCase manager_DiaryCase;

    private void OnEnable()
    {
        Init();
        manager_DiaryCase = FindObjectOfType<Manager_DiaryCase>();

    }

    private void Init()
    {
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);
        _dateTime = DateTime.Now;

        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = Instantiate(_item);
            item.name = "Date" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 36 + startPos.x, startPos.y - (i / 7) * 30, startPos.z);
        
            _dateItems.Add(item);
        }

        CreateCalendar();
    }
    TextMeshProUGUI label = null;
    //date = 0;
    private void CreateCalendar()
    {
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);
        bool checkToday = false;
        int date = 0;

        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString() + " 월 ";

        switch (_dateTime.Month)
        {
            case 1:
                _monthStrText.text = monthStr[0];
                break;
            case 2:
                _monthStrText.text = monthStr[1];
                break;
            case 3:
                _monthStrText.text = monthStr[2];
                break;
            case 4:
                _monthStrText.text = monthStr[3];
                break;
            case 5:
                _monthStrText.text = monthStr[4];
                break;
            case 6:
                _monthStrText.text = monthStr[5];
                break;
            case 7:
                _monthStrText.text = monthStr[6];
                break;
            case 8:
                _monthStrText.text = monthStr[7];
                break;
            case 9:
                _monthStrText.text = monthStr[8];
                break;
            case 10:
                _monthStrText.text = monthStr[9];
                break;
            case 11:
                _monthStrText.text = monthStr[10];
                break;
            case 12:
                _monthStrText.text = monthStr[11];
                break;
        }

        if (_yearNumText.text == DateTime.Now.Year.ToString() &&
           _monthNumText.text == DateTime.Now.Month.ToString() + " 월 ") checkToday = true;

        for (int i = 0; i < _totalDateNum; i++)
        {
            /*TextMeshProUGUI*/ label = _dateItems[i].GetComponentInChildren<TextMeshProUGUI>();
            GameObject dateObj = _dateItems[i].gameObject;
            _dateItems[i].SetActive(false);

            if (i >= index)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true);

                    label.text = (date + 1).ToString();

                    if (dateObj.transform.localPosition.x == 106.5) // SaturDay
                    {
                        label.color = Color.blue;
                    }

                    if (dateObj.transform.localPosition.x == -109.5) // SunDay
                    {
                        label.color = Color.red;
                    }

                    if (checkToday == true && label.text == _dateTime.Day.ToString())
                        _dateItems[i].GetComponent<Button>().Select();

                    days.Add(date);
                    date++;
                }
            }
        }
    }

    private int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Sunday: return 0;
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
        }

        return 0;
    }

    public void OnClick_YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void OnClick_YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void OnClick_MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void OnClick_MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    public void OnClick_Date()
    {
        // If there is a diary for that date, the corresponding diary file is displayed in preview.
        Debug.Log(_yearNumText.text + "" +_monthNumText.text +" "+ days);
        

    }
}

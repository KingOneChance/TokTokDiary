using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Func_CalendarController : MonoBehaviour
{
    const int _totalDateNum = 42;

    [SerializeField] private GameObject _item;
    public List<GameObject> stickers = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI _yearNumText;
    [SerializeField] private TextMeshProUGUI _monthNumText;
    [SerializeField] private TextMeshProUGUI _monthStrText;
    [SerializeField] private List<GameObject> _dateItems = new List<GameObject>();

    [SerializeField] public TextMeshProUGUI[] preViewDate = null;

    private DateTime _dateTime;

    private string[] monthStr = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    private string myName = "";

    private List<int> days = new List<int>();

    Manager_DiaryCase manager_DiaryCase;


    private void OnEnable()
    {
        manager_DiaryCase = FindObjectOfType<Manager_DiaryCase>();
        Init();

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
        stickers.AddRange(GameObject.FindGameObjectsWithTag("DiarySticker"));
        CreateCalendar();

        DateTime today = DateTime.Today;

        string todayMonth = today.Month.ToString();
        string todayYear = today.Year.ToString();
        string todayDay = today.Day.ToString();
        string year = manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[0];
        string month = manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[1];
        string day = manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[2];
        if (todayYear == year && todayMonth == month && todayDay == day)
        {
            ShowPreviewDate(year, month, day );
        }
        
    }
    
    TextMeshProUGUI label = null;
    //date = 0;
    private void CreateCalendar()
    {
        for (int i = 0; i < stickers.Count; i++)
        {
            stickers[i].gameObject.SetActive(false);
        }
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);
        bool checkToday = false;
        int date = 0;

        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString()/* + " 월 "*/;

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
           _monthNumText.text == DateTime.Now.Month.ToString() /*+ " 월 "*/) checkToday = true;

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
       

        TurnOnSticker();
 
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
        string nowDay = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        manager_DiaryCase.previewText.gameObject.SetActive(false);

        string fileName = _yearNumText.text + "_" + _monthNumText.text + "_" + nowDay;

        if (manager_DiaryCase.allFilesDictionary.ContainsKey(fileName+"-1"))
        {
            manager_DiaryCase.previewImg.texture = manager_DiaryCase.allFilesDictionary[fileName + "-1"];
        }
        else
        {
            manager_DiaryCase.previewImg.texture = null;
            manager_DiaryCase.previewText.gameObject.SetActive(true);
        }
        for(int i=0; i < manager_DiaryCase.allFilesTexture.Count; i++)
        {
            if (manager_DiaryCase.previewImg.texture == manager_DiaryCase.allFilesTexture[i])
            {
                manager_DiaryCase.presentNum = i;
            }
        }
    }
    //일기가 있는 날짜에 스티커 켜주기
    private void TurnOnSticker()
    {
        string fileName = "";

        for (int k = 0; k < manager_DiaryCase.allFiles.Count; k++)
        {
            fileName = manager_DiaryCase.allFiles[k].Split("\\")[1].Split(".")[0].Split("-")[0];

            string year = fileName.Split("_")[0];
            string month = fileName.Split("_")[1];
            string day = fileName.Split("_")[2];

            for (int i = 0; i < stickers.Count; i++)
            {
                if (day == stickers[i].transform.parent.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text &&
                    _yearNumText.text == year && _monthNumText.text == month)
                {
                    stickers[i].gameObject.SetActive(true);
                }
            }

        }
    }
    //preview에있는 이미지
    public void ShowPreviewDate(string year, string month, string day)
    {
      /*  year = manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[0];
        month = manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[1];
        day = manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[2];
*/
        preViewDate[0].text = year;
        preViewDate[1].text = month;
        preViewDate[2].text = day;
        

    }
}

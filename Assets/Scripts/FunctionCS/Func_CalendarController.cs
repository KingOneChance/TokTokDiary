using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Func_CalendarController : MonoBehaviour
{
    const int _totalDateNum = 42;

    [SerializeField] Button goToMain = null;

    [SerializeField] private GameObject _item;
    [SerializeField] private List<GameObject> _dateItems = new List<GameObject>();

    [SerializeField] private GameObject _itemParent;
    //해당 날짜에 일기장이 있을때 TURN ON되는 스티커들 
    public List<GameObject> stickers = new List<GameObject>();

    //달력에 년도, 월 일
    [SerializeField] public TextMeshProUGUI _yearNumText;
    [SerializeField] public TextMeshProUGUI _monthNumText;
    [SerializeField] public TextMeshProUGUI _monthStrText;

    //프리뷰 년도 월 일
    [SerializeField] public TextMeshProUGUI[] preViewDate = null;

    private DateTime _dateTime;

    private string[] monthStr = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    [SerializeField] private RawImage[] monthImages = null;
    private string myName = "";

    private List<int> days = new List<int>();

    [SerializeField] Manager_DiaryCase manager_DiaryCase;



    private void OnEnable()
    {
        manager_DiaryCase = FindObjectOfType<Manager_DiaryCase>();
        Init();
        goToMain.gameObject.gameObject.SetActive(false);
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
            //item.GetComponent<Button>().onClick.AddListener(() => OnClick_Date());
            item.name = "Date" + (i + 1).ToString();
            item.transform.SetParent(_itemParent.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 37 + startPos.x, startPos.y - (i / 7) * 32, startPos.z);

            _dateItems.Add(item);
        }

        CreateCalendar();

        string year = manager_DiaryCase.allFilesTexture[manager_DiaryCase.allFiles.Count - 1].name.Split("-")[0].Split("_")[0];
        string month = manager_DiaryCase.allFilesTexture[manager_DiaryCase.allFiles.Count - 1].name.Split("-")[0].Split("_")[1];
        string day = manager_DiaryCase.allFilesTexture[manager_DiaryCase.allFiles.Count - 1].name.Split("-")[0].Split("_")[2];
        ShowPreviewDate(year, month, day);
        //마지막 파일의 날짜에 검은색 테두리만들기
        for (int i = _dateItems.Count; i > 0; --i)
        {
            if (_dateItems[i - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == manager_DiaryCase.allFilesTexture[manager_DiaryCase.allFiles.Count - 1].name.Split("-")[0].Split("_")[2]
                && _dateItems[i - 1].transform.GetChild(1).GetChild(0).gameObject.activeSelf == true)
            {
                _dateItems[i - 1].GetComponent<Button>().Select();
            }

        }
    }

    TextMeshProUGUI label = null;
    //date = 0;
    private void CreateCalendar()
    {
        for(int i = 0; i < monthImages.Length; i++)
        {
            monthImages[i].gameObject.SetActive(false);
        }
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);
        int date = 0;

        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString();//* + " 월 "*//*;

        switch (_dateTime.Month)
        {
            case 1:
                _monthStrText.text = monthStr[0];
                monthImages[0].gameObject.SetActive(true);
                break;
            case 2:
                _monthStrText.text = monthStr[1];
                monthImages[1].gameObject.SetActive(true);
                break;
            case 3:
                _monthStrText.text = monthStr[2];
                monthImages[2].gameObject.SetActive(true);
                break;
            case 4:
                _monthStrText.text = monthStr[3];
                monthImages[3].gameObject.SetActive(true);
                break;
            case 5:
                _monthStrText.text = monthStr[4];
                monthImages[4].gameObject.SetActive(true);
                break;
            case 6:
                _monthStrText.text = monthStr[5];
                monthImages[5].gameObject.SetActive(true);
                break;
            case 7:
                _monthStrText.text = monthStr[6];
                monthImages[6].gameObject.SetActive(true);
                break;
            case 8:
                _monthStrText.text = monthStr[7];
                monthImages[7].gameObject.SetActive(true);
                break;
            case 9:
                _monthStrText.text = monthStr[8];
                monthImages[8].gameObject.SetActive(true);
                break;
            case 10:
                _monthStrText.text = monthStr[9];
                monthImages[9].gameObject.SetActive(true);
                break;
            case 11:
                _monthStrText.text = monthStr[10];
                monthImages[10].gameObject.SetActive(true);
                break;
            case 12:
                _monthStrText.text = monthStr[11];
                monthImages[11].gameObject.SetActive(true);
                break;
        }

        for (int i = 0; i < _totalDateNum; i++)
        {
            label = _dateItems[i].GetComponentInChildren<TextMeshProUGUI>();
            GameObject dateObj = _dateItems[i].gameObject;
            _dateItems[i].SetActive(false);

            if (i >= index)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true);

                    label.text = (date + 1).ToString();

                    days.Add(date);
                    date++;
                }
            }
        }
        stickers.AddRange(GameObject.FindGameObjectsWithTag("DiarySticker"));

        for (int i = 0; i < stickers.Count; i++)
        {
            stickers[i].gameObject.SetActive(false);
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
 
        int m = int.Parse(_monthNumText.text);

        string mm = "";

        if (m < 10) mm = "0" + m;
        else mm = m.ToString();

        if (int.Parse(nowDay) < 10) nowDay = "0" + nowDay;

        string fileName = int.Parse(_yearNumText.text) + "_" + mm + "_" + nowDay;

        for (int i = 0; i < manager_DiaryCase.allFilesTexture.Count; i++)
        {
            if (manager_DiaryCase.allFilesTexture[i].name == fileName + "-1")
            {
                manager_DiaryCase.previewText.gameObject.SetActive(false);
                // manager_DiaryCase.previewImg.texture = manager_DiaryCase.allFilesDictionary[fileName + "-1"];
                manager_DiaryCase.previewImg.texture = manager_DiaryCase.allFilesTexture[i];
                int year = int.Parse(manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[0]);
                int month = int.Parse(manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[1]);
                int day = int.Parse(manager_DiaryCase.previewImg.texture.name.Split("-")[0].Split("_")[2]);
                Debug.Log(year +" "+ month + " " + day);
                ShowPreviewDate(year.ToString(), month.ToString(), day.ToString());
                break;
            }
            else
            {
                manager_DiaryCase.previewImg.texture = null;
                manager_DiaryCase.previewText.gameObject.SetActive(true);
                ShowPreviewDate("", "", "");
            }
        }

        for (int i = 0; i < manager_DiaryCase.allFilesTexture.Count; i++)
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
                if (int.Parse(day) == int.Parse(stickers[i].transform.parent.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text) &&
                    int.Parse(_yearNumText.text) == int.Parse(year) && int.Parse(_monthNumText.text) == int.Parse(month))
                {
                    stickers[i].gameObject.SetActive(true);
                }
            }
        }
    }
    //preview에있는 이미지의 이름을 가지고 날짜를 바꿔준다
    public void ShowPreviewDate(string year, string month, string day)
    {
        preViewDate[0].text = year;
        preViewDate[1].text = month;
        preViewDate[2].text = day;

    }

    public void ChangeCalender()
    {
        //연도가 같을때
        if (int.Parse(preViewDate[0].text) == int.Parse(_yearNumText.text))
        {
            //연도가 같고 프리뷰 월이 달력 월보다 작을때
            if (int.Parse(preViewDate[1].text) < int.Parse(_monthNumText.text))
            {
                while (int.Parse(preViewDate[1].text) != int.Parse(_monthNumText.text))
                {

                    OnClick_MonthPrev();
                }
            }
            //연도가 같고 프리뷰 월이 달력 월보다 클때
            else if (int.Parse(preViewDate[1].text) > int.Parse(_monthNumText.text))
            {
                while (int.Parse(preViewDate[1].text) != int.Parse(_monthNumText.text))
                {

                    OnClick_MonthNext();
                }
            }

        }
        //프리뷰 연도가 달력 연도보다 낮을때
        else if (int.Parse(preViewDate[0].text) < int.Parse(_yearNumText.text))
        {
            while (int.Parse(preViewDate[0].text) != int.Parse(_yearNumText.text))
            {
                OnClick_YearPrev();
            }
            while (int.Parse(preViewDate[1].text) != int.Parse(_monthNumText.text))
            {
                OnClick_MonthNext();
            }
        }
        //프리뷰 연도가 달력 연도보다 높을때
        else if (int.Parse(preViewDate[0].text) > int.Parse(_yearNumText.text))
        {
            while (int.Parse(preViewDate[0].text) != int.Parse(_yearNumText.text))
            {
                OnClick_YearNext();
            }
            while (int.Parse(preViewDate[1].text) != int.Parse(_monthNumText.text))
            {
                OnClick_MonthPrev();
            }
        }

        for (int i = 0; i < _dateItems.Count; i++)
        {
            if (_dateItems[i].transform.GetChild(1).GetChild(0).gameObject.activeSelf == true
                && int.Parse(preViewDate[2].text) == int.Parse(_dateItems[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text))
            {
                _dateItems[i].GetComponent<Button>().Select();
                break;
            }
        }
    }


}

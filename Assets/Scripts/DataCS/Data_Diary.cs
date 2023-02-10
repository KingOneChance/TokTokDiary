using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Diary
{
    public string name;
    public string writtenDate;
    public int diaryCount;

    public Data_Diary(string _name, string _writtenData, int _diaryCount)
    {
        name = _name;
        writtenDate = _writtenData;
        diaryCount = _diaryCount;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvManager : MonoBehaviour
{
    public int _percent = 0;

    private void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Unity_Csv_Test");

        List<Dictionary<string, object>> Testdata = CSVReader.Read("Data Table - Mission");
        /*
        for (var i = 0; i < data.Count; i++)
        {
            
            Debug.Log("index " + (i).ToString() + "Monkey : " + data[i]["Monkey"] + " Percent : " + data[i]["Percent"] + " Money : " + data[i]["Money"]);
            if (i % 20 == 0)
            {
                Debug.Log("다음 칸!");
            }
        }

        _percent = (int)data[0]["Monkey"];
        Debug.Log(_percent);
        */

        for (var i = 0; i < Testdata.Count; i++)
        {

            Debug.Log("index " + (i).ToString() + "ID : " + Testdata[i]["아이디"]);
        }
    }
}

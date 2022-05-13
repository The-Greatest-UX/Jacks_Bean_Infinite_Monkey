using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvManager : MonoBehaviour
{
    public int _percent = 0;
    public List<Dictionary<string, object>> CommonData;
    public List<Dictionary<string, object>> RewardData;
    public List<Dictionary<string, object>> MajorTaskData;
    public List<Dictionary<string, object>> MinerTaskData;
    public List<Dictionary<string, object>> StructureData;
    public List<Dictionary<string, object>> ResearchData;
    public List<Dictionary<string, object>> DummyData;



    private void Start()
    {
        CommonData    = CSVReader.Read("Data Table - Common");
        RewardData    = CSVReader.Read("Data Table - Reward");
        MajorTaskData = CSVReader.Read("Data Table - MajorTask");
        MinerTaskData = CSVReader.Read("Data Table - MinorTask");
        StructureData = CSVReader.Read("Data Table - Structure");
        ResearchData  = CSVReader.Read("Data Table - Research");
        DummyData     = CSVReader.Read("Data Table - Dummy");

        for (var i = 0; i < CommonData.Count; i++)
        {

            Debug.Log("index " + (i).ToString() + "Value : " + CommonData[i]["Bignumber_Value"]);
        }

        /*
        List<Dictionary<string, object>> data = CSVReader.Read("Unity_Csv_Test");
        List<Dictionary<string, object>> Testdata = CSVReader.Read("Data Table - Mission");
        
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
        

        for (var i = 0; i < Testdata.Count; i++)
        {

            Debug.Log("index " + (i).ToString() + "ID : " + Testdata[i]["아이디"]);
        }

        */
    }
}

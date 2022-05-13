using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class CommonData
{

    public List<CommonMember_BigNumber> Init_Gold;
    public List<CommonMember_BigNumber> Init_RP;
    public List<CommonMember_Int> Init_Diamod;
    public List<CommonMember_Float> Init_Monkey_Rest_Time;
    public List<CommonMember_Float> Init_Monkey_Typing_Time;
    public List<CommonMember_Int> Init_Monkey_Health;
    public List<CommonMember_Float> Monkey_Price_Scale_Value;
    public List<CommonMember_BigNumber> Init_Stage1_Monkey_Price;
    public List<CommonMember_BigNumber> Init_Stage2_Monkey_Price;
    public List<CommonMember_BigNumber> Init_Stage3_Monkey_Price;
    public List<CommonMember_BigNumber> Init_Stage4_Monkey_Price;
    public List<CommonMember_Float> Structure_Price_Scale_Value;
    public List<CommonMember_Float> Structure_Reinforce_Gold_Scale_Value;
    public List<CommonMember_Float> Letter_Add_Gold_Scale_Value;
    public List<CommonMember_Float> Research_IDEA_SCALE_Value;
    public List<CommonMember_Float> Research_MONKEY_TYPING_TIME_Value;
    public List<CommonMember_Int> Research_MONKEY_HEALTH_Value;
    public List<CommonMember_Float> Research_MONKEY_REST_TIEM_Value;
    public List<CommonMember_Float> Research_MONKEY_PRICE_SALE_Value;
    public List<CommonMember_Float> Research_STRUCTURE_PRICE_SALE_Value;
    public List<CommonMember_Int> Research_OVERTIME_MAX_TIME_Value;
    public List<CommonMember_Float> Research_DONATION_SCALE_Value;
    public List<CommonMember_Int> Research_METALTIME_DURATION_Value;
    public List<CommonMember_Float> Research_GOLD_SCALE_Value;

    public void printData()
    {
        Debug.Log(Init_Diamod);
        Debug.Log(Init_Monkey_Rest_Time);
        Debug.Log(Init_Monkey_Typing_Time);
        Debug.Log(Init_Monkey_Health);
        Debug.Log(Monkey_Price_Scale_Value);
        Debug.Log(Init_Stage1_Monkey_Price);
        Debug.Log(Init_Stage2_Monkey_Price);
        Debug.Log(Init_Stage3_Monkey_Price);
        Debug.Log(Init_Stage4_Monkey_Price);
    }
}

public class CommonMember_BigNumber
{
    //public BigNumber Bignumber_Value;
    public string Bignumber_Value;
    public string Description;
}
public class CommonMember_Int
{
    public int Int_Value;
    public string Description;
}
public class CommonMember_Float
{
    public int Float_Value;
    public string Description;
}


public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance { get { return instance; } }




    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        FileStream fileStream = new FileStream(string.Format(Application.dataPath + "/Resources/DataJson/Data Table - Common.json"), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);


        CommonData Data = new CommonData();
        Data = JsonConvert.DeserializeObject<CommonData>(jsonData);


        Debug.Log(jsonData);
        /*
        string LoadData = File.ReadAllText(Application.dataPath + "/Resources/DataJson/Data Table - Common.json");
        
        
        //string jsonData = Encoding.UTF8.GetString(data);
        

        /*
        Data.printData();
        //stream.Close();
        /*
        CommonData Data = new CommonData();
        string jsonData = JsonConvert.SerializeObject(Data);

        string str = File.ReadAllText(Application.dataPath + "/Resources/DataJson/Data Table - Common.json");
        CommonData data2 = JsonConvert.DeserializeObject<CommonData>(str);
        //File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(data));
        
        print(str);

        data2.printData();
        */



    }


}

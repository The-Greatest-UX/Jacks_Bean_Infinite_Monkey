using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Percent_Machine {
    
    public static bool Excute_Percent(double value)
    {
        float temp = Random.Range(0, 100.0f);

        if(temp <= value)
        {
            Debug.Log("성공! | 확률은 : " + value + "%" + "나온 값은 : " + temp);
            return true;
        }

        else
        {
            Debug.Log("실패....! | 확률은 : " + value + "%" + "나온 값은 : " + temp);
            return false;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Percent_Machine {
    
    public static bool Excute_Percent(double value)
    {
        float temp = Random.Range(0, 100.0f);

        if(temp <= value)
        {
            Debug.Log("����! | Ȯ���� : " + value + "%" + "���� ���� : " + temp);
            return true;
        }

        else
        {
            Debug.Log("����....! | Ȯ���� : " + value + "%" + "���� ���� : " + temp);
            return false;
        }
    }


}

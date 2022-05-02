using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percent : MonoBehaviour
{

    public int monkey_Num = 1;
    public int key_Num = 2;
    public int sentence_Num = 5;


    double percent = 1;
    int combination = 1;
    double result = 0;



    void Start()
    {
        Set_Default();
    }

    void Set_Default()
    {
        percent = 1;
        combination = 1;
        result = 0;
    }

    void Get_Percent()
    {
        for (int i = 0; i < sentence_Num; i++)
        {
            percent *= key_Num;
        }

        percent = 1 / percent;
        Debug.Log("È®·üÀº : " + percent);
    }

    /*
    int Get_Combination(int v, int x) // vCx
    {
        combination = 1;
        int n = v;
        int r = x;

        if(r >  (n / 2))
        {
            r = r / 2;
        }

        for (int i = 0; i < r; i++)
        {
            combination *= (n - i);
            //Debug.Log(n - i);
        }

        for (int i = 0; i < r; i++)
        {
            combination /= (r - i);
            //Debug.Log(r - i);
        }

        if(v == x || x == 0)
        {
            combination = 1;
        }
        Debug.Log(v + "C" + x + "Àº : " + combination);
        return combination;
    }
    */


    double Return_Exponential_Increase(double x, int y) //xÀÇ y½Â
    {
        double temp = 1;
        for (int i = 0; i < y; i++)
        {
            temp *= x;
        }
        Debug.Log(x + "ÀÇ " + y + "½ÂÀº : " + temp);
        return temp;
    }

    void Get_Result()
    {
        result = 1 - Return_Exponential_Increase(1 - percent, monkey_Num);
    }

    public void Generate()
    {
        Debug.Log("-----------------------Generate-------------------------");
        Set_Default();
        Get_Percent();

        Get_Result();
        Debug.Log("ÃÖÁ¾ÀûÀÎ È®·üÀº = " + result);
    }

}

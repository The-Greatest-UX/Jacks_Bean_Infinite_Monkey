using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   

    public Text MantissaText;
    public Text DigitText;
    public Text BuyMonkeyText;
    public Text AddMoneyText;
    public Text Spell_Time_Text;
    public Text Percent_Text;
    public Text Key_Text;
    public Text Sentence_Text;
    public Text MonkeyNumText;
    public Text totalTimeText;


    public int key_Num = 2;
    public int sentence_Num = 5;
    public int monkey_Num = 1;
    public int Succece_Count = 0;
    public int exponent = 0;

    public float spell_Time = 2f;
    public float total_Spell_Time = 0f;

    public double mantissa = 0;
    public decimal percent = 0;

    public GameObject Monkey;
    
    BigNumber money1;
    BigNumber addmoney;
    BigNumber MonkeyCost;
    void Start()
    {
        MonkeyCost = new BigNumber(50, 0);
        money1 = new BigNumber(0, 0);
        addmoney = new BigNumber(10, 0);
        
        Cal_Percent();
        //money1 = IncrementHelper.Add(money1, money2);
        StartCoroutine("Spell_Sentence");
        Update_UI();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Percent_Machine.Excute_Percent(50f);
        }
    }

    public void BuyMonkey()
    {
        if(IncrementHelper.Compare(money1, MonkeyCost))
        {
            money1 = IncrementHelper.Subtract(money1, MonkeyCost);
            MonkeyCost = IncrementHelper.Add(MonkeyCost, MonkeyCost);
            
            monkey_Num++;
            
            Cal_Percent();

            Update_UI();
        }
        
    }

    public void Succese()
    {
        money1 = IncrementHelper.Add(money1, addmoney);
        
        addmoney = IncrementHelper.Add(addmoney, new BigNumber(5, 0));

        Succece_Count++;

        if(Succece_Count == 10)
        {
            sentence_Num++;
            Succece_Count = 0;
            
            if(sentence_Num == 5)
            {
                sentence_Num = 2;
                Succece_Count = 0;
                key_Num++;
            }

            Cal_Percent();
        }

        Update_UI();
        
    }

    public void Update_UI()
    {
        MonkeyNumText.text = "Monkey = " + monkey_Num;
        BuyMonkeyText.text = MonkeyCost.Mantissa + IncrementHelper.GetConvertedExponent(MonkeyCost.Exponent);
        MantissaText.text = "Mantissa = " + money1.Mantissa;
        DigitText.text = "Digit = " + IncrementHelper.GetConvertedExponent(money1.Exponent);
        AddMoneyText.text = "Addmoney = " + addmoney.Mantissa + IncrementHelper.GetConvertedExponent(addmoney.Exponent);
        Sentence_Text.text = "Sentence = " + sentence_Num;
        Key_Text.text = "Key = " + key_Num;

        Percent_Text.text = "Percent = " + percent.ToString();
        Spell_Time_Text.text = "Time = " + spell_Time;
        total_Spell_Time = sentence_Num * spell_Time;
        totalTimeText.text = "Total_Time " + total_Spell_Time;
    }

    private IEnumerator Spell_Sentence()
    {
        while (true)
        {
            for (int i = 0; i < sentence_Num; i++)
            {
                Monkey.transform.position = new Vector3(Monkey.transform.position.x, Monkey.transform.position.y + 1, Monkey.transform.position.z);
                yield return new WaitForSeconds(0.2f);
                Monkey.transform.position = new Vector3(Monkey.transform.position.x, Monkey.transform.position.y - 1, Monkey.transform.position.z);
                yield return new WaitForSeconds(spell_Time);
            }
            
            
            //����� �����ߴ����� �� ���ؾ���
            //Ű 2�� ���� 2����
            // 0�� ���� : 25�� / 1�� ���� : 50�� / 2�� ���� : 25�� ��
            // �� 0~25������ ���� X, 25~75������ 1�ܰ� ����, 75~100������ 2�ܰ� ����� ���� ���
            // �׷��� ������ �̷��� Ȯ���� �������� ����
            // ������ ������ ����, ������ ������

            int random = 0;

            random = Random.Range(0, 100);

            if (Percent_Machine.Excute_Percent((double)percent * 100))
            {
                Succese();
            }
            
        }
    }

    public void Cal_Percent()
    {
        //float temp = ((float)key_Num / sentence_Num);
        decimal p = 1 / (decimal)key_Num;
        Debug.Log(p);
        for (int i = 1; i < sentence_Num; i++)
        {
            p *= p;
            Debug.Log(p);
        }
        p = 1 - p;
        Debug.Log(p);
        for (int i = 1; i < monkey_Num; i++)
        {
            p *= p;
            Debug.Log(p);
        }
        p = 1 - p;
        Debug.Log(p);

        percent = p;
    }

    public void Cal_Percent_2()
    {
        float Load_Percent = 0.03f;
        float tempP = Random.Range(0.0f, 1.0f);
        if (tempP <= Load_Percent)
        {
            //����
        }
        else
        {
            //����
        }

    }

    public void Add_Time(float value)
    {
        spell_Time += value;
        Update_UI();
    }

    public void Add_Key(int value)
    {
        key_Num += value;
        Cal_Percent();
        Update_UI();
    }

    public void Add_Word(int value)
    {
        sentence_Num += value;
        Cal_Percent();
        Update_UI();
    }

    public void Add_Money(int value)
    {
        money1 = IncrementHelper.Add(money1, new BigNumber(1000, 2));
        Update_UI();
    }
    public void Add_Monkey(int value)
    {
        monkey_Num += value;
        Cal_Percent();
        Update_UI();
    }

    public void Generate()
    {
        /*
        addmoney = new BigNumber(mantissa, exponent);
        money1 = IncrementHelper.Add(money1, addmoney);
       
        //ValueText.text = "Value = " + IncrementHelper.ConvertBigNumberToInteger(money1);
        MantissaText.text = "Mantissa = " + money1.Mantissa;
        DigitText.text = "Digit = " + IncrementHelper.GetConvertedExponent(money1.Exponent);
        */
        Percent_Machine.Excute_Percent(0.5f);
        /*
        digit = ((int)Mathf.Log(value, 10));
        Mantissa = (value / Mathf.Pow(10, digit));
        Mantissa = Math.Truncate(Mantissa * 1000) / 1000;
        MantissaText.text = "Mantissa = " + Mantissa;
        DigitText.text = "Digit = " + digit;
        */
    }
}

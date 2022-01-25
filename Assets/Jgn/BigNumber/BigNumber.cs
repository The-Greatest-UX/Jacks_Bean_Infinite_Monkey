using System;
using UnityEngine;

[Serializable]
public struct BigNumber
{
    [SerializeField] private int m_Exponent;
    [SerializeField] private double m_Mantissa;
    public int Exponent => m_Exponent;
    public double Mantissa => m_Mantissa;
    public bool HasValue => m_Mantissa > 0.0 && m_Exponent >= 0;
    public static readonly BigNumber Identity = new BigNumber(0.0, 0);

    public BigNumber(double mant, int exp)
    {
        // exp�� 0���� ���� ���� ���������� �Է����� �Ǵ��ؼ� ���� ó��(���� mant�� 1000.0 ���� ū ������ �� exp�� �������� ������ ��찡 ������ �ϴ� ����)
        if (exp < 0)
        {
            Debug.Log("Exp cannot be smaller than 0!");
            m_Mantissa = 0.0;
            m_Exponent = 0;
        }
        else
        {
            if (mant >= IncrementHelper.ONE_THOUSAND)
            {
                double tempExp = Math.Floor(Math.Log(mant, IncrementHelper.ONE_THOUSAND));
                double tempMant = mant * Math.Pow(IncrementHelper.ONE_THOUSAND, -tempExp);

                m_Mantissa = tempMant;
                m_Exponent = exp + (int)tempExp;

                //Debug.Log($"BigNumber(A) : mant({m_Mantissa}) , exp({m_Exponent})");
            }
            else if (mant < 1.0)
            {
                // mant�� 1.0���� �۰� exp�� 0�� ���
                if (exp == 0)
                {
                    //Debug.Log($"Mant({mant}) is lower than 1.0 and exp is zero. Zero-BigNumber is returned!");
                    m_Mantissa = 0.0;
                    m_Exponent = 0;
                }
                // mant�� 1.0���� �۰� exp�� 0���� ū ���
                else
                {
                    // �� ��� tempExp�� �׻� 0���� �۴�.
                    double tempExp = Math.Floor(Math.Log(mant, IncrementHelper.ONE_THOUSAND));
                    double tempMant = mant * Math.Pow(IncrementHelper.ONE_THOUSAND, -tempExp);
                    int expSum = exp + (int)tempExp;

                    if (expSum < 0)
                    {
                        //Debug.Log($"Mant({mant}) is lower than 1.0 and exp({exp}) is bigger than 0.0. But, expSum({expSum}) is lower than zero! Zero-BigNumber is returned! tempExp({tempExp}), tempMant({tempMant})");
                        m_Mantissa = 0.0;
                        m_Exponent = 0;
                    }
                    else
                    {
                        m_Mantissa = tempMant;
                        m_Exponent = expSum;

                        // Exponent�� 0�� ��� Mantissa�� ������ �ƴ� �������� ���� �ʵ��� �����ϱ� ���� ����
                        if (m_Exponent == 0)
                        {
                            //Debug.Log($"Mant : {m_Mantissa}, Mant_Floor : {Math.Floor(m_Mantissa)}");
                            m_Mantissa = Math.Floor(m_Mantissa);
                        }

                        //Debug.Log($"BigNumber(B) : mant({m_Mantissa}) , exp({m_Exponent})");
                    }
                }
            }
            // mant�� 1.0���� ũ�ų� ���� 1000.0���� ���� ���
            else
            {

                m_Mantissa = mant;
                m_Exponent = exp;

                // Exponent�� 0�� ��� Mantissa�� ������ �ƴ� �������� ���� �ʵ��� �����ϱ� ���� ����
                if (m_Exponent == 0)
                {
                    //Debug.Log($"Mant : {m_Mantissa}, Mant_Floor : {Math.Floor(m_Mantissa)}");
                    m_Mantissa = Math.Floor(m_Mantissa);
                }

                //Debug.Log($"BigNumber(C) : mant({m_Mantissa}) , exp({m_Exponent})");
            }
        }
    }
}

[CreateAssetMenu()]
public class BigNumberData : ScriptableObject
{
    public BigNumber Value;
}
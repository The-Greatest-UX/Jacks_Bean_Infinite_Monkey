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
        // exp가 0보다 작은 경우는 비정상적인 입력으로 판단해서 예외 처리(물론 mant가 1000.0 보다 큰 숫자일 때 exp가 음수여도 가능한 경우가 있으나 일단 무시)
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
                // mant가 1.0보다 작고 exp가 0인 경우
                if (exp == 0)
                {
                    //Debug.Log($"Mant({mant}) is lower than 1.0 and exp is zero. Zero-BigNumber is returned!");
                    m_Mantissa = 0.0;
                    m_Exponent = 0;
                }
                // mant가 1.0보다 작고 exp가 0보다 큰 경우
                else
                {
                    // 이 경우 tempExp는 항상 0보다 작다.
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

                        // Exponent가 0인 경우 Mantissa가 정수가 아닌 유리수가 되지 않도록 방지하기 위한 로직
                        if (m_Exponent == 0)
                        {
                            //Debug.Log($"Mant : {m_Mantissa}, Mant_Floor : {Math.Floor(m_Mantissa)}");
                            m_Mantissa = Math.Floor(m_Mantissa);
                        }

                        //Debug.Log($"BigNumber(B) : mant({m_Mantissa}) , exp({m_Exponent})");
                    }
                }
            }
            // mant가 1.0보다 크거나 같고 1000.0보다 작은 경우
            else
            {

                m_Mantissa = mant;
                m_Exponent = exp;

                // Exponent가 0인 경우 Mantissa가 정수가 아닌 유리수가 되지 않도록 방지하기 위한 로직
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
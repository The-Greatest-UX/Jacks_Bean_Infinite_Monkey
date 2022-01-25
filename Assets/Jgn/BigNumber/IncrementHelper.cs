using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IncrementHelper
{
    private static readonly int CHAR_a = Convert.ToInt32('a');
    private static readonly int EXP_DIFF_MAX = 2;
    private static readonly int JEWEL_DISPLAY_MAX_AMOUNT = 999999;

    public static readonly double ONE_THOUSAND = 1000.0;


    //////////////////////////////////////////
    // Big Number
    //////////////////////////////////////////

    /// <summary>
    /// BigNumber를 알파벳 지수를 포함한 문자열로 변환한다.
    /// </summary>
    public static string GetFormattedNumber(BigNumber num, bool isInteger)
    {
        return isInteger || num.Exponent == 0
                ? string.Format(HelperManager.STRING_FORMAT_UNIT_A, num.Mantissa.ToString(HelperManager.STRING_FORMAT_NUMBER_INT), GetConvertedExponent(num.Exponent))
                : string.Format(HelperManager.STRING_FORMAT_UNIT_A, num.Mantissa.ToString(HelperManager.STRING_FORMAT_NUMBER), GetConvertedExponent(num.Exponent));
    }

    //320,000
    //320a
    // exp = 1 * 1000 * 320

    //public static BigInteger GetConvertNumber(BigNumber bignum)
    //{
    //    BigInteger num = (BigInteger)((float)bignum.Mantissa * Mathf.Pow(1000f, bignum.Exponent));
    //    return num;
    //}

    public static int ConvertBigNumberToInteger(BigNumber num)
    {
        if (num.Exponent > 1)
        {
            Debug.Log($"This bigNumer({num.Mantissa}_{num.Exponent}) is converted to 999,999 because it's too big!");
            return JEWEL_DISPLAY_MAX_AMOUNT;
        }
        else
            return (int)((float)num.Mantissa * Mathf.Pow(1000.0F, num.Exponent));
    }

    public static string GetConvertedExponent(int exp) //변환된 지수 문자로 출력
    {
        if (exp <= 0)
            return string.Empty;
        else if (exp <= 26)
            return Convert.ToChar(CHAR_a + exp - 1).ToString();
        else  // exp > 26(알파벳 두 자릿수)
        {
            int firstUnit = exp / 26;
            int secondUnit = exp % 26;
            return string.Format(HelperManager.STRING_FORMAT_UNIT_A, Convert.ToChar(CHAR_a + firstUnit - 1).ToString(), Convert.ToChar(CHAR_a + secondUnit - 1).ToString());
        }
    }

    public static int GetConvertedExponent(string exp) //변환된 지수 정수형으로 출력
    {
        if (exp.Length < 1 || !exp.All(char.IsLetter))
            return -1;

        char[] exps = exp.ToLower().ToCharArray();
        if (exps.Length > 2)
        {
            Debug.Log($"Exponents is too big({exps})!");
            return -1;
        }

        if (exps.Length == 1)
            return Convert.ToInt32(exps[0]) - CHAR_a + 1;
        else  // exp.Length == 2
        {
            int firstUnit = 26 * (Convert.ToInt32(exps[0] - CHAR_a + 1));
            int secondUnit = Convert.ToInt32(exps[1]) - CHAR_a + 1;
            return firstUnit + secondUnit;
        }
    }

    /// <summary>
    /// 두 BigNumber를 비교하는 함수. former가 latter보다 크거나 같으면 true, 작으면 false를 반환한다.
    /// </summary>
    public static bool Compare(BigNumber former, BigNumber latter) //비교
    {
        bool FormerIsBigger = true;
        if (former.Exponent < latter.Exponent)
        {
            FormerIsBigger = false;
        }
        else if (former.Exponent == latter.Exponent)
        {
            if (former.Mantissa >= latter.Mantissa)
                FormerIsBigger = true;
            else
                FormerIsBigger = false;
        }

        return FormerIsBigger;
    }

    /// <summary>
    /// 두 BigNumber를 비교하는 함수. former가 latter보다 크거나 같으면 true, 작으면 false를 반환한다.
    /// 지수가 같다면 isSameExponent에 true가 할당된다.
    /// 지수의 차이가 2보다 크거나 같다면 isExpDiffTwoOrBigger에 true가 할당된다.
    /// </summary>
    public static bool Compare(BigNumber former, BigNumber latter, out bool isSameExponent, out bool isExpDiffTwoOrBigger)
    {
        bool FormerIsBigger = true;
        bool sameExponent = false;
        if (former.Exponent < latter.Exponent)
        {
            FormerIsBigger = false;
        }
        else if (former.Exponent == latter.Exponent)
        {
            sameExponent = true;
            if (former.Mantissa >= latter.Mantissa)
                FormerIsBigger = true;
            else
                FormerIsBigger = false;
        }

        if (Mathf.Abs(former.Exponent - latter.Exponent) >= 2)
            isExpDiffTwoOrBigger = true;
        else
            isExpDiffTwoOrBigger = false;

        isSameExponent = sameExponent;
        return FormerIsBigger;
    }

    public static BigNumber Add(BigNumber numA, BigNumber numB) //더하기
    {
        int expDiff = numA.Exponent - numB.Exponent;
        int expDiff_Abs = Mathf.Abs(expDiff);
        if (expDiff_Abs > EXP_DIFF_MAX)
        {
            Debug.Log("Exponent diff is more than " + EXP_DIFF_MAX + ".");
            return expDiff > 0 ? numA : numB;
        }
        else
        {
            double sum_mant = 0.0;
            int sum_exp = 0;
            if (expDiff == 0)
            {
                sum_mant = numA.Mantissa + numB.Mantissa;
                sum_exp = numA.Exponent;
            }
            else
            {
                double multiplier = Math.Pow(ONE_THOUSAND, expDiff_Abs);
                if (expDiff > 0)
                {
                    sum_mant = (numA.Mantissa * multiplier + numB.Mantissa) / multiplier;
                    sum_exp = numA.Exponent;
                }
                else
                {
                    sum_mant = (numA.Mantissa + numB.Mantissa * multiplier) / multiplier;
                    sum_exp = numB.Exponent;
                }
            }

            return new BigNumber(sum_mant, sum_exp);
        }
    }

    public static BigNumber Add(List<BigNumber> nums)
    {
        int maxExp = int.MinValue;
        for (int i = 0; i < nums.Count; i++)
        {
            int temp = nums[i].Exponent;
            if (temp > maxExp)
                maxExp = temp;
        }

        BigNumber sum = new BigNumber();
        for (int i = 0; i < nums.Count; i++)
        {
            if (nums[i].Exponent >= maxExp - EXP_DIFF_MAX)
                sum = Add(sum, nums[i]);
        }

        return sum;
    }

    public static BigNumber Subtract(BigNumber numA, BigNumber numB) // 빼기
    {
        int expDiff = numA.Exponent - numB.Exponent;
        int expDiff_Abs = Mathf.Abs(expDiff);
        if (expDiff_Abs > EXP_DIFF_MAX)
        {
#if UNITY_EDITOR
            if (expDiff > 0)
                Debug.Log("Cannot subract : Exponent diff is more than " + EXP_DIFF_MAX + ". numA is much bigger than numB.");
            else
                Debug.Log("Cannot subract : Exponent diff is more than " + EXP_DIFF_MAX + ". numB is much bigger than numA.");
#endif
            return numA;
        }
        else
        {
            if (expDiff == 0)
            {
                double mantDiff = numA.Mantissa - numB.Mantissa;
                if (mantDiff > 0.0)
                {
                    //Debug.Log("Subtract two BigNumbers. Exps are the same.");
                    return new BigNumber(mantDiff, numA.Exponent);
                }
                else if (mantDiff < 0.0)
                {
                    //Debug.Log("Cannot subtract: numA.mantissa is smaller than numB.mantissa. numA is returned.");
                    return numA;
                }
                else  // mantDiff == 0
                {
                    //Debug.Log("Subtract two BigNumbers. numA is exactly the same as numB. Default BigNumber struct is returned.");
                    return new BigNumber();
                }
            }
            else if (expDiff > 0)
            {
                //Debug.Log("Subtract two BigNumbers. numA.Exponent is bigger than numB.Exponent.");
                double multiplier = Math.Pow(ONE_THOUSAND, (double)expDiff_Abs);
                return new BigNumber((numA.Mantissa * multiplier - numB.Mantissa) / multiplier, numA.Exponent);
            }
            else  // expDiff < 0
            {
                //Debug.Log("Cannot subtract: numA.exponent is smaller than numB.exponent. numA is returned.");
                return numA;
            }
        }
    }

    public static BigNumber Multiply(BigNumber num, double multiplier) //곱하기
    {
        return new BigNumber(num.Mantissa * multiplier, num.Exponent);
    }

    public static BigNumber Divide(BigNumber num, double divider) // 나누기
    {
        double divideResult = num.Mantissa / divider;
        return divideResult < 1.0
                ? new BigNumber(divideResult * ONE_THOUSAND, num.Exponent - 1)
                : new BigNumber(divideResult, num.Exponent);
    }

    public static double GetProportion(BigNumber numA, BigNumber numB) //비율
    {
        int expDiff = numA.Exponent - numB.Exponent;
        if (expDiff < 0)
        {
            if (expDiff == -1)
            {
                //Debug.Log("Calculate percentage : ExpDiff is inside the range.");
                return numA.Mantissa / (numB.Mantissa * ONE_THOUSAND);
            }
            else  // expDiff < -1
            {
                Debug.Log("Calculate percentage : Return 0.0 because numA.Exponent is too small than numB.Exponent.");
                return 0.0;
            }
        }
        else if (expDiff == 0)
        {
            double mantDiff = numA.Mantissa - numB.Mantissa;
            if (mantDiff < 0)
                //Debug.Log("Calculate percentage : numB.Mantissa is bigger than numA.Mantissa.");
                return numA.Mantissa / numB.Mantissa;
            else if (mantDiff > 0)
                //Debug.LogFormat("Calculate percentage : Return 1.0 because numA.Mantissa is bigger than numB.Mantissa. {0}  {1}", numA.Mantissa, numB.Mantissa);
                return 1.0;
            else  // mantDiff == 0
                //Debug.Log("Calculate percentage : Return 0.0 because numA.Mantissa is same as numB.Mantissa.");
                return 0.0;
        }
        else  // expDiff > 0
        {
            //Debug.Log("Calculate percentage : Return 1.0 because numA is bigger than numB.");
            return 1.0;
        }
    }


    //public static float Round(float value, uint uptoSecondDigitAfterDecimalPoint)
    //{
    //    float mult_A = Mathf.Pow(10.0F, (float)uptoSecondDigitAfterDecimalPoint);
    //    float mult_B = Mathf.Pow(0.1F, (float)uptoSecondDigitAfterDecimalPoint);

    //    return Mathf.Round(value * mult_A) * mult_B;
    //}

    //public static float RoundUptoSecondDigit(float value)
    //{
    //    return Mathf.Round(value * 100.0F) * 0.01F;
    //}

    //public static ulong FormatNumber(ulong value)
    //{
    //    int n = (int)Math.Log(value, 1000);
    //    var m = value / Math.Pow(1000, n);
    //    var unit = "";

    //    if (n < units.Count)
    //    {
    //        unit = units[n];
    //    }
    //    else
    //    {
    //        var unitInt = n - units.Count;
    //        var secondUnit = unitInt % 26;
    //        var firstUnit = unitInt / 26;
    //        unit = Convert.ToChar(firstUnit + charA).ToString() + Convert.ToChar(secondUnit + charA).ToString();
    //    }

    //    // Math.Floor(m * 100) / 100) fixes rounding errors
    //    return (Math.Floor(m * 100) / 100).ToString("0.##") + unit;
    //}
}
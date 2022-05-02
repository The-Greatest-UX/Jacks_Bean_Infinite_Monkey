using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LongUtil
{
    public static long Lerp(long start, long end, float t){
        if(t > 1f)
            return end;
        if(t == 0f)
            return start;
        return (long)(start * (1f-t)) + (long)(end * t);
    }
    /// <summary>
    /// 0이면 없음
    /// 1이면 K
    /// 2면 M
    /// 3이면 B
    /// 4면 T
    /// </summary>

    public static int GetKMB(this long number){
        var result = 0;
        while(number > 1000){
            number /= 1000;
            result++;
        }
        return result;

    }
}

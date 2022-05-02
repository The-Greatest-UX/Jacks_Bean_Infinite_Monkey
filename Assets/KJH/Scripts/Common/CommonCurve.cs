using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonCurve
{
    public static AnimationCurve Linear = AnimationCurve.Linear(0f,0f,1f,1f);
    public static AnimationCurve EaseIO = AnimationCurve.EaseInOut(0f,0f,1f,1f);

}


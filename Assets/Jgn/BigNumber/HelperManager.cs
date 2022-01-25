using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperManager
{

    /*
    public static readonly int LAYER_DEFAULT                      = 0;
    public static readonly int LAYER_COIN_DESTINATION_GOLD        = LayerMask.NameToLayer("UI_GOLD_COIN_DESTINATION");
    public static readonly int LAYER_COIN_DESTINATION_HEART       = LayerMask.NameToLayer("UI_HEART_COIN_DESTINATION");
    public static readonly int LAYER_COIN_DESTINATION_JEWEL       = LayerMask.NameToLayer("UI_JEWEL_COIN_DESTINATION");
    public static readonly int LAYER_COIN_DESTINATION_UTILITY     = LayerMask.NameToLayer("UI_UTILITY_COIN_DESTINATION");
    public static readonly int LAYER_COIN_DESTINATION_SHOP        = LayerMask.NameToLayer("UI_SHOP_COIN_DESTINATION");
    public static readonly int LAYER_UI                           = LayerMask.NameToLayer("UI");
    public static readonly int LAYER_WORLD_UI                     = LayerMask.NameToLayer("WORLD_UI");
    public static readonly int LAYER_ANIMAL                       = LayerMask.NameToLayer("ANIMAL");
    public static readonly int LAYER_WATER                        = LayerMask.NameToLayer("Water");
    public static readonly int LAYER_ANIMAL_NAVMESH               = LayerMask.NameToLayer("ANIMAL_NAVMESH");
    public static readonly int LAYERMASK_UI                       = 1 << LAYER_UI;
    public static readonly int LAYERMASK_WORLD_UI                 = 1 << LAYER_WORLD_UI;
    public static readonly int LAYERMASK_WATER                    = 1 << LAYER_WATER;
    public static readonly int LAYERMASK_WORLD_UI_OR_ANIMAL       = 1 << LAYER_WORLD_UI | 1 << LAYER_ANIMAL;
    public static readonly int LAYERMASK_ANIMAL                   = 1 << LAYER_ANIMAL;
    public static readonly int LAYERMASK_ANIMAL_NAVMESH           = 1 << LAYER_ANIMAL_NAVMESH;
    public static readonly int SORTING_LAYER_WORLD_UI             = SortingLayer.NameToID("WORLD_UI");

    public static readonly int  NON_LANDMARK_ANIMAL_IDX_OFFSET    = 1000;

    public static readonly float APPROXIMATE_ONE_HIGH_DEFINITION  = 0.999999F;
    public static readonly float APPROXIMATE_ONE_MID_DEFINITION   = 0.999F;
    public static readonly float APPROXIMATE_ONE_LOW_DEFINITION   = 0.99F;

    public static readonly Color WHITE_ALPHA_ZERO                  = new Color(1.0F, 1.0F, 1.0F, 0.0F);
    public static readonly Color WHITE_ALPHA_30_PERC               = new Color(1.0F, 1.0F, 1.0F, 0.3F);
    public static readonly Color WHITE_ALPHA_60_PERC               = new Color(1.0F, 1.0F, 1.0F, 0.6F);
    public static readonly Color WHITE_ALPHA_70_PERC               = new Color(1.0F, 1.0F, 1.0F, 0.7F);
    public static readonly Color WHITE_ALPHA_100_PERC              = new Color(1.0F, 1.0F, 1.0F, 1.0F);
    public static readonly Color BUTTON_COLOR_RED                  = new Color32(232, 102, 83, 255);
    public static readonly Color BUTTON_COLOR_BLUE                 = new Color32(109, 189, 231, 255);
    public static readonly Color BUTTON_COLOR_GREEN                = new Color32(208, 230, 69, 255);
    public static readonly Color BUTTON_COLOR_GREEN_DARKER         = new Color32(199, 228, 17, 255);
    public static readonly Color BUTTON_COLOR_PURPLE               = new Color32(194, 100, 171, 255);
    public static readonly Color BUTTON_COLOR_PURPLE_2             = new Color32(204, 135, 245, 255);
    public static readonly Color BUTTON_COLOR_BLACK                = new Color32(30, 68, 68, 255);
    public static readonly Color BUTTON_COLOR_YELLOW               = new Color32(255, 205, 19, 255);
    public static readonly Color BUTTON_COLOR_YELLOW_2             = new Color32(255, 224, 106, 255);
    public static readonly Color BUTTON_COLOR_YELLOWGREEN          = new Color32(199, 228, 17, 255);
    public static readonly Color BUTTON_COLOR_GRAY_100             = new Color32(100, 100, 100, 255);
    public static readonly Color BUTTON_COLOR_GRAY_181             = new Color32(181, 181, 181, 255);
    public static readonly Color BUTTON_COLOR_GRAY_200             = new Color32(200, 200, 200, 255);
    public static readonly Color BUTTON_COLOR_GRAY_227             = new Color32(227, 227, 227, 255);
    public static readonly Color BUTTON_COLOR_BLACK_70             = new Color32(70, 70, 70, 255);
    public static readonly Color BUTTON_COLOR_ANIMAL_WITH_NOTI     = new Color32(237, 119, 98, 255);
    public static readonly Color BUTTON_COLOR_ANIMAL_WITHOUT_NOTI  = new Color32(24, 55, 56, 255);

    public static readonly WaitForSeconds WS_ONE_SEC                   = new WaitForSeconds(1.0F);
    public static readonly WaitForSeconds WS_HALF_SEC                  = new WaitForSeconds(0.5F);
    public static readonly WaitForSeconds WS_ONE_FIFTH_SEC             = new WaitForSeconds(0.2F);
    public static readonly WaitForSecondsRealtime WS_ONE_SEC_REALTIME  = new WaitForSecondsRealtime(1.0F);
    public static readonly WaitForSeconds WS_TWO_SEC                   = new WaitForSeconds(2.0F);
    public static readonly WaitForSeconds WS_THREE_SEC                 = new WaitForSeconds(3.0F);
    public static readonly WaitForSeconds WS_FIVE_SEC                  = new WaitForSeconds(5.0F);
    public static readonly WaitForFixedUpdate WFU                      = new WaitForFixedUpdate();
    public static readonly WaitForEndOfFrame WEOF                      = new WaitForEndOfFrame();

    

    public static readonly string TWEEN_ID_ZEN_MODE_FLOATING     = "ZEN_MODE_FLOATING";
    public static readonly string TWEEN_ID_HATCHING_FLOATING     = "HATCHING_FLOATING";
    public static readonly string TWEEN_ID_MOVE_NEXT_ZEN_MODE    = "MOVE_NEXT_ZEN_MODE";
    public static readonly string TWEEN_ID_ENTER_ORBIT_MODE      = "ENTER_ORBIT_MODE";
    public static readonly string TWEEN_ID_LENS_SHIFT            = "LENS_SHIFT";
    public static readonly string TWEEN_ID_CAM_ROTATION          = "CAM_ROTATION";
    public static readonly string TWEEN_ID_SCREEN_BLUR           = "SCREEN_BLUR";
    public static readonly string TWEEN_ID_COLOR_GRADING         = "COLOR_GRADING";
    public static readonly string TWEEN_ID_FOV                   = "FOV";
    public static readonly string TWEEN_ID_DOF                   = "DOF";
    public static readonly string TWEEN_ID_VIEW_FINDER           = "VIEW_FINDER";

    public static readonly float FONT_SIZE_LANDMARK_LEVELUP      = 0.5F;
    public static readonly float FONT_SIZE_ANIMAL_LEVELUP        = 0.3F;

    */
    public static readonly BigNumber MAX_BIGNUM = new BigNumber(1.0, 10000);
    public static readonly string STRING_FORMAT_NUMBER = "##0.00";
    public static readonly string STRING_FORMAT_NUMBER_INT = "##0";
    public static readonly string STRING_FORMAT_UTILITY_STAT = "###0.0";
    public static readonly string STRING_FORMAT_UNIT_A = "{0}{1}";
    public static readonly string STRING_FORMAT_UNIT_B = "{0}_{1}";
    public static readonly string STRING_FORMAT_UNIT_C = "{0}_{1}_{2}";
    public static readonly string STRING_FORMAT_UNIT_FRACTION = "{0}/{1}";
    public static readonly string STRING_FORMAT_PERCENTAGE = "+{0}%";
    public static readonly string STRING_FORMAT_PLUS_SIGN = "+{0}";

    //////////////////////////////////////////
    // Utilities
    //////////////////////////////////////////

    /*
    public static bool IsPointerOverUI()
    {
        int pointerId;
#if UNITY_EDITOR
        pointerId = -1;
#elif UNITY_ANDROID || UNITY_IOS
        pointerId = 0;
#endif

        EventSystem eventSystem = EventSystem.current;
        if (eventSystem != null)
        {
            return eventSystem.IsPointerOverGameObject(pointerId);
        }
        else
        {
            Debug.Log("There is no eventsystem.");
            return false;
        }
    }

    public static void AlignWithSameDistanceFromCenter(TextMeshProUGUI text, Image coinImg, float blankspaceOffset = 15.0F, bool isScaled = false)
    {
        Transform costTr = text.transform;
        RectTransform coinTr = coinImg.rectTransform;
        float costTextWidth = text.preferredWidth;
        float coinImageWidth = coinTr.sizeDelta.x;
        if (isScaled)
        {
            costTextWidth *= costTr.localScale.x;
            coinImageWidth *= coinTr.localScale.x;
        }
        float costPosXOffset = (coinImageWidth + blankspaceOffset) * 0.5F;
        float coinPosXOffset = -(blankspaceOffset + costTextWidth) * 0.5F;
        costTr.localPosition = new Vector3(costPosXOffset, costTr.localPosition.y, 0.0F);
        coinTr.localPosition = new Vector3(coinPosXOffset, coinTr.localPosition.y, 0.0F);
    }

    public static void PlayFloatingTween(Transform tr, float amplitude, float interval, Ease ease, string tweenId)
    {
        if (DOTween.IsTweening(tweenId))
            DOTween.Kill(tweenId);

        tr?.DOLocalMove(tr.up * amplitude, interval)
            .SetRelative(true)
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId(tweenId)
            .Play();
    }

    public static Vector3 GetRandomXZDirection(float minDist, float maxDist)
    {
        Vector2 randomXZ = Random.insideUnitCircle.normalized;
        Vector3 randomDirection = new Vector3(randomXZ.x, 0.0F, randomXZ.y);
        float randomDistance = Random.Range(minDist, maxDist);
        randomDirection *= randomDistance;
        return randomDirection;
    }

    public static Tweener GetTextBlinkTween(TextMeshProUGUI text, float blinkEndVal = 0.7F, float duration = 1.5F, Ease ease = Ease.InOutSine)
    {
        if (text == null)
            return null;

        if (DOTween.IsTweening(text))
            DOTween.Kill(text);

        return text.DOFade(blinkEndVal, duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }

    public static List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);

            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return list;
    }

    public static void ResetAllTriggers(this Animator animator)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }

    //public static bool CheckIfKoreanWordJongsungExists(string korWord, out bool hasJongsung)
    //{
    //    if (LocalizationManager.CurrentLanguage == )

    //    char lastChar = korWord.ToCharArray()[korWord.Length - 1];

    //    // 한글의 제일 처음과 끝의 범위밖일 경우는 오류
    //    if (lastChar < 0xAC00 || lastChar > 0xD7A3)
    //    {
    //        Debug.LogError($"Last character({lastChar}) is not valid!");
    //        hasJongsung = false;
    //        return false;
    //    }


    //    String seletedValue = (lastName - 0xAC00) % 28 > 0 ? firstValue : secondValue;
    //    return name+seletedValue;
    //}
    */
}
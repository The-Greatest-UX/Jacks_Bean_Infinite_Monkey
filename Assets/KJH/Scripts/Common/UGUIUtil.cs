using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UGUIUtil {
    public static void SetAlpha (this Graphic target, float alpha) {
        var color = target.color;
        color.a = alpha;
        target.color = color;
    }

    public static void SetAlpha (this Graphic target, float alpha, bool containChild) {
        var color = target.color;
        color.a = alpha;
        target.color = color;

        if(containChild){
            foreach(var ig in target.GetComponentsInChildren<Graphic>())
            {
                var c = ig.color;
                c.a = alpha;
                ig.color = c;
            }
        }


    }



    // public enum __BTNSTATE
    // {
    // 	INVISIBLE = 0,
    // 	ENABLE,
    // 	DISABLE,
    // }
    public static void SetState (this Button button, int state) {
        if (state == 0) {
            button.gameObject.SetActive (false);
        } else if (state == 1) {
            button.gameObject.SetActive (true);
            button.interactable = true;
        } else if (state == 2) {
            button.gameObject.SetActive (true);
            button.interactable = false;
        }
    }
    public static void SetStateAlpha(this Button button, int state){
        if (state == 0) {
            button.gameObject.SetActive (false);
        } else if (state == 1) {
            button.gameObject.SetActive (true);
            button.SetActiveAlpha(true);
        } else if (state == 2) {
            button.gameObject.SetActive (true);
            button.SetActiveAlpha(false);
        }
    }

    public static void SetActiveAlpha(this Button button, bool active){
        button.interactable = active;
        button.targetGraphic.AlphaTween(active ? 1f : 0.5f, 0f, true);
    }

    public static Vector3 GetFillEndPosition(this Image target){
        //일단 필요한 horizontal만 나중에 다른거 채워넣기
        if(target.type != Image.Type.Filled)
            return target.transform.position;

        if(target.fillMethod == Image.FillMethod.Horizontal)
        {
            var pos = target.GetComponent<RectTransform>().anchoredPosition;
            var width = target.GetComponent<RectTransform>().sizeDelta.x;
            pos.x -= width/2f;
            pos.x += width * target.fillAmount;
            return pos;
        }
        return target.transform.position;
        
    }


    public static void SetImage (this Button button, ButtonImageSet imageSet){
        //button.sp
    }
}

[Serializable]
public class ButtonImageSet {
    public Sprite normal;
    public Sprite highlight;
}
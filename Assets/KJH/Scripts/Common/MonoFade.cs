﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MonoFade : MonoSingleton<MonoFade> {
    Image _fadeImage;
    public Image closeTrigger;

    public void Awake() {
        _fadeImage = GetComponent<Image>();
        _fadeImage.color = Color.black;
    }

    /// <summary>
    /// 전부 꺼진 상태 0
    /// 다 켜진 상태 1
    /// 맨위에만 꺼진 상태 2
    /// </summary>

    public void SetInputBlock(int state) {
        if(state == 0){
            foreach(var i in GetComponentsInChildren<Image>())
                i.raycastTarget = false;
            return;
        }
        else {
            foreach(var i in GetComponentsInChildren<Image>())
                i.raycastTarget = true;
            closeTrigger.raycastTarget = state == 1;
        }
    }

    [TestMethod]
    public Coroutine Fade(float alpha, float time) {
        _fadeImage.StopAllCoroutines();
        if (alpha == 0f)
            SetInputBlock(0);
        else
            SetInputBlock(2);

        return _fadeImage.AlphaTween(alpha, time);
    }

    public void SetFade(float alpha) {
        var color = _fadeImage.color;
        color.a = alpha;
        _fadeImage.color = color;
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class CC {
    /// <typeparam name="TOwner">보간을 값을 가지고 있는 대상</typeparam>
    /// <typeparam name="TValue">보간할 값의 타입</typeparam>
    /// <returns></returns>
    public static IEnumerator TweenRoutine<TOwner, TValue> (
        TOwner target,
        Func<TValue> end,
        Func<TOwner, TValue> startGetter,
        Action<TOwner, TValue> setter,
        Func<TValue, TValue, float, TValue> lerpFunc,
        TimeContainer tc,
        AnimationCurve curve = null,Action callback = null) {
        if (curve == null)
            curve = AnimationCurve.Linear (0, 0, 1, 1);
        TValue start = default (TValue);

        if (startGetter != null)
            start = startGetter (target);

        if(tc.time != 0){
            while (tc.t < 1f) {
                tc.t += Time.deltaTime / tc.time;
                if (setter != null && lerpFunc != null)
                    setter (target, lerpFunc (start, end (), curve.Evaluate (tc.t)));
                yield return null;
            }
        }{
            tc.t = 1f;
        }
        if (setter != null)
            setter (target, end ());
        callback?.Invoke();
        tc.Complete();
    }

    public static Coroutine Scale (this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.localScale, (t, e) => t.localScale = e, Vector3.LerpUnclamped, time, curve));
    }
    public static Coroutine Scale (this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => start, (t, e) => t.localScale = e, Vector3.LerpUnclamped, time, curve));
    }
    public static Coroutine Scale (this SpriteRenderer runner, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();

        return mono.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.localScale, (t, e) => t.localScale = e, Vector3.LerpUnclamped, time, curve));
    }
        public static Coroutine Move (this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null, Action callback = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => start, (t, e) => t.position = e, Vector3.LerpUnclamped, time, curve, callback));
    }
    public static Coroutine Move (this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null, Action callback = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.position, (t, e) => t.position = e, Vector3.LerpUnclamped, time, curve, callback));
    }
    public static Coroutine Move (this SpriteRenderer runner, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();

        return mono.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.position, (t, e) => t.position = e, Vector3.LerpUnclamped, time, curve));
    }

    public static Coroutine Move (this MonoBehaviour runner, Transform end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end.position, t => t.position, (t, e) => t.position = e, Vector3.LerpUnclamped, time, curve));
    }

    public static Coroutine MoveBezier(this MonoBehaviour runner, Vector3 mid, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => t.position, (t, e) => t.position = e, (s,e,t) => (s) * (1 - t) * (1 - t) + mid * (1 - t) * t + e * t * t, time, curve));
    }

    public static Coroutine MoveBezier(this MonoBehaviour runner, Vector3 start, Vector3 mid, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => start, (t, e) => t.position = e, (s,e,t) => (s) * (1 - t) * (1 - t) + mid * (1 - t) * t + e * t * t, time, curve));
    }

    public static Coroutine Stay (this MonoBehaviour runner, Transform end, TimeContainer time) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end.position, t => t.position, (t, e) => t.position = e, (a, b, t) => b, time));
    }

    // public static Coroutine Move (this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
    //     return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => start, (t, e) => t.position = e, Vector3.Lerp, time, curve));
    // }

    public static Coroutine MoveUI (this MonoBehaviour runner, Vector2 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.GetComponent<RectTransform> (), () => end, t => t.anchoredPosition, (t, e) => t.anchoredPosition = e, Vector2.Lerp, time, curve));
    }

    public static Coroutine MoveUI (this MonoBehaviour runner, Vector2 start, Vector2 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.GetComponent<RectTransform> (), () => end, t => start, (t, e) => t.anchoredPosition = e, Vector2.Lerp, time, curve));
    }

    public static Coroutine ChangeUISize (this MonoBehaviour runner, Vector2 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.GetComponent<RectTransform> (), () => end, t => t.sizeDelta, (t, e) => t.sizeDelta = e, Vector2.Lerp, time, curve));
    }

    public static Coroutine MoveLocal (this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.localPosition, (t, e) => t.localPosition = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine MoveLocal (this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => start, (t, e) => t.localPosition = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine MoveRelatively (this MonoBehaviour runner, Vector3 dis, TimeContainer time, AnimationCurve curve = null) {
        var end = runner.transform.position + dis;
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.position, (t, e) => t.position = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine MoveLocal (this SpriteRenderer runner, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();

        return mono.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.localPosition, (t, e) => t.localPosition = e, Vector3.LerpUnclamped, time, curve));
    }
    public static Coroutine Rotation (this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.eulerAngles, (t, e) => t.eulerAngles = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine Rotation (this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => start, (t, e) => t.eulerAngles = e, Vector3.Lerp, time, curve));
    }

    public static Coroutine RotationLocal (this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => start, (t, e) => t.localEulerAngles = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine RotationLocal (this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => end, t => t.localEulerAngles, (t, e) => t.localEulerAngles = e, Vector3.Lerp, time, curve));
    }

    public static Coroutine RotationRelative (this MonoBehaviour runner, Vector3 relativeAngle, TimeContainer time, AnimationCurve curve = null) {
        var currentQ = runner.transform.rotation;
        return runner.StartCoroutine (TweenRoutine (runner.transform, () => currentQ * Quaternion.Euler(relativeAngle), t => t.rotation, (t, e) => t.rotation = e, Quaternion.Lerp, time, curve));
    }


    public static Coroutine Wait (this MonoBehaviour runner, TimeContainer wait, AnimationCurve curve = null) {
        //2번째 인자와 템플린 인자는 필요없긴함.
        return runner.StartCoroutine (TweenRoutine<object, object> (null, null, null, null, null, wait));
    }

     public static Coroutine AlphaTween (this SpriteRenderer runner, float end, TimeContainer time, AnimationCurve curve = null) {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();
        
        return mono.StartCoroutine (TweenRoutine (runner, () => end, sp => sp.color.a, (sp, a) => sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, a), Mathf.Lerp, time, curve));
    }
    public static Coroutine AlphaTween (this SpriteRenderer runner, float start, float end, TimeContainer time, AnimationCurve curve = null) {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();
        
        return mono.StartCoroutine (TweenRoutine (runner, () => end, sp => start, (sp, a) => sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, a), Mathf.Lerp, time, curve));
    }


    public static Coroutine ColorTween (this SpriteRenderer runner, Color start, Color end, TimeContainer time, AnimationCurve curve = null) {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();
        
        return mono.StartCoroutine (TweenRoutine (runner, () => end, sp => start, (sp, a) => sp.color = a, Color.Lerp, time, curve));
    }
    public static Coroutine ColorTween (this SpriteRenderer runner, Color end, TimeContainer time, AnimationCurve curve = null) {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();
        
        return mono.StartCoroutine (TweenRoutine (runner, () => end, sp => sp.color, (sp, a) => sp.color = a, Color.Lerp, time, curve));
    }

    public static Coroutine AlphaTween (this Graphic runner, float end, TimeContainer time, bool containChild = false, AnimationCurve curve = null) {
        if (containChild) {
            var child = runner.transform.GetComponentsInChildren<Graphic> ();
            foreach (var c in child)
                c.AlphaTween (end, time, false, curve);
        }

        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => sp.color.a, UGUIUtil.SetAlpha, Mathf.Lerp, time, curve));
    }

    public static Coroutine ColorTween (this Graphic runner, Color end, TimeContainer time, bool containChild = false, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => sp.color, (o, c) => o.color = c, Color.Lerp, time, curve));
    }

    public static Coroutine AlphaTween (this Graphic runner, float start, float end, TimeContainer time, bool containChild = false, AnimationCurve curve = null) {
        if (containChild) {
            var child = runner.transform.GetComponentsInChildren<Graphic> ();
            foreach (var c in child)
                c.AlphaTween (start, end, time, false, curve);
        }

         return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => start, UGUIUtil.SetAlpha, Mathf.Lerp, time, curve));
    }

    public static Coroutine MoveWithSpeed (this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve speedCurve, float speed = 1f, Action onUpdate = null) {
        return runner.StartCoroutine (SpeedRoutine (runner.transform, end, time, speedCurve, speed, onUpdate));
    }

    public static Coroutine FillTween (this Image runner, float start, float end, TimeContainer time, AnimationCurve curve = null) {

        return runner.StartCoroutine (TweenRoutine (runner, () => end, r => start, (p, i) => p.fillAmount = i, Mathf.Lerp, time, curve));
    }

    public static Coroutine FillTween (this Image runner, float end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, r => r.fillAmount, (p, i) => p.fillAmount = i, Mathf.Lerp, time, curve));
    }

    public static Coroutine NumberTween (this TextMeshProUGUI runner, long end, TimeContainer time, AnimationCurve curve = null) {
        if(LongParse(runner) == end){
            time.Complete();
            return null;
        }
        runner.StopAllCoroutines();

        return runner.StartCoroutine (TweenRoutine (runner, () => end, LongParse, (p, i) => p.text = i.ToString ("N0"), LongUtil.Lerp, time, curve));
    }
    
    public static Coroutine NumberTween (this TextMeshProUGUI runner, long end,  Func<long ,string> interProcessor, TimeContainer time, AnimationCurve curve = null) {
        if(LongParse(runner) == end){
            time.Complete();
            return null;
        }
        runner.StopAllCoroutines();

        return runner.StartCoroutine (TweenRoutine (runner, () => end, LongParse, (p, i) => p.text = interProcessor(i), LongUtil.Lerp, time, curve));
    }
    public static Coroutine NumberTween (this TextMeshProUGUI runner,long start,  long end,  Func<long ,string> interProcessor, TimeContainer time, AnimationCurve curve = null) {
        if(start == end){
            time.Complete();
            return null;
        }
        runner.StopAllCoroutines();

        return runner.StartCoroutine (TweenRoutine (runner, () => end, t=>start, (p, i) => p.text = interProcessor(i), LongUtil.Lerp, time, curve));
    }


    private static long LongParse (TextMeshProUGUI text) {
        var str = text.text;
        long result =0;
        if(long.TryParse(str.Replace (",", ""), out result)){
            return result;
        }
        return 0;
    }

    public static Coroutine PingPongScale (this MonoBehaviour runner, float min, float max, float length, Func<bool> isPlaying) {
        return runner.StartCoroutine (PingPong (runner.transform, min, max, length, isPlaying));
    }

    public static IEnumerator PingPong (Transform trans, float min, float max, float length, Func<bool> isPlay) {
        var st = Time.realtimeSinceStartup;
        while (isPlay ()) {
            var t = Mathf.PingPong (Time.realtimeSinceStartup - st, length) / length;
            trans.localScale = Vector3.Lerp (Vector3.one * min, Vector3.one * max, t);
            yield return null;
        }
    }

    public static Coroutine PingPongColor (this Graphic runner, Color min, Color max, float length, Func<bool> isPlaying) {
        return runner.StartCoroutine (PingPong (runner, min, max, length, isPlaying));
    }

    public static IEnumerator PingPong (Graphic target, Color min, Color max, float length, Func<bool> isPlay) {
        var st = Time.realtimeSinceStartup;
        while (isPlay ()) {
            var t = Mathf.PingPong (Time.realtimeSinceStartup - st, length) / length;
            target.color = Color.Lerp ( min,  max, t);
            yield return null;
        }
    }

    public static Coroutine PingPongColor (this SpriteRenderer runner, Color min, Color max, float length, Func<bool> isPlaying) {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();

        return mono.StartCoroutine (PingPong (runner, min, max, length, isPlaying));
    }

    public static IEnumerator PingPong (SpriteRenderer target, Color min, Color max, float length, Func<bool> isPlay) {
        var st = Time.realtimeSinceStartup;
        while (isPlay ()) {
            var t = Mathf.PingPong (Time.realtimeSinceStartup - st, length) / length;
            target.color = Color.Lerp ( min,  max, t);
            yield return null;
        }
    }

    public static Coroutine AlphaPingPongTween(this SpriteRenderer runner, float loop, TimeContainer time)
    {
        var mono = runner.GetComponent<MonoBehaviour>();
        if(mono == null)
            mono = runner.gameObject.AddComponent<MonoObject>();
        
        return mono.StartCoroutine(PingPongRoutine(runner,(o,v)=>o.color = new Color(o.color.r, o.color.g, o.color.b, v), loop,time));
    }

    public static Coroutine AlphaPingPongTween(this Graphic runner, float loop, TimeContainer time)
    {
        return runner.StartCoroutine(PingPongRoutine(runner,(o,v)=>o.color = new Color(o.color.r, o.color.g, o.color.b, v), loop,time));
    }

    public static IEnumerator PingPongRoutine<TOwner>(
        TOwner target,
        Action<TOwner, float> setter,
        float loop,
        TimeContainer tc,
        float max = 1f)
    {
        while (tc.t < 1f)
        {
            tc.t += Time.deltaTime / tc.time;
            //기준값2
            setter(target,  Mathf.PingPong(tc.t*2f*loop,max));
            yield return null;
        }
        setter(target, Mathf.PingPong(2f*loop, max));
    }

    public static IEnumerator SpeedRoutine (Transform target, Vector3 end, TimeContainer time, AnimationCurve speedCurve, float speed, Action onUpdate = null) {
        if (speed < 0f)
            throw new Exception ("speed는 0보다 커야됩니다.");

        foreach (var node in speedCurve.keys)
            if (node.value < 0)
                throw new Exception ("모든 커브 키값은 0보다 커야 됩니다.");

        var start = target.localPosition;
        //var tc = new TimeContainer("tempForSpeedRoutine", 1f);
        var allDistance = Vector3.Distance (start, end);
        var originDirection = (end - start).normalized;
        var distance = allDistance;
        var currentDirection = originDirection;

        while (Vector3.Dot (originDirection, currentDirection) > 0f && time.t < 1f) { //뒤에 조건은 외부에서 캔슬시켰을 때 상황임.

            target.Translate (currentDirection * speed * speedCurve.Evaluate (1f - distance / allDistance), Space.Self);

            distance = Vector3.Distance (target.localPosition, end);
            currentDirection = (end - target.localPosition).normalized;

            if (onUpdate != null)
                onUpdate ();

            yield return null;
        }

        target.localPosition = end;
        time.Complete ();
    }
}
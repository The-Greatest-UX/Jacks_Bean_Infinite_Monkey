using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class GamePopupManager : MonoBehaviour
{
    Dictionary<Type, GamePopupBase> m_dic = new Dictionary<Type, GamePopupBase>();

    public List<GamePopupBase> m_popupPrefabList;

    List<GamePopupBase> m_mem = new List<GamePopupBase>();

    public float fadeTime = 0.3f;

    private static GamePopupManager Instance;

    public static void SetBackClose(bool enable){
        Instance.fadeClose.interactable = enable;
    }

    public Image fade;
    public Button fadeClose;
    private void Awake() {
        Instance = this;
        foreach(var p in m_popupPrefabList) {
            m_dic[p.GetType()] = p;
        }
    }
    
    public static T Open<T>(bool useScaleAnimation=true) where T : GamePopupBase{
        if(!isOpen<T>())
            return Instance.CreatePopup<T>(null,useScaleAnimation);

        return null;
    }

    public static T Open<T,TArg>(TArg arg, bool useScaleAnimation=true) where T : GamePopupBase{
        if(!isOpen<T>())
            return Instance.CreatePopup<T>(arg,useScaleAnimation);
        return null;
    }


    private T CreatePopup<T>(object arg = null,bool useScaleAnimation = true) where T : GamePopupBase{
        if(m_mem.Count == 0){
            fade.AlphaTween(0.4f,fadeTime);
            fade.raycastTarget = true;
        }
        fade.transform.SetSiblingIndex(m_mem.Count);//페이드를 맨뒤로
        T popup =  Instantiate((T)m_dic[typeof(T)], transform);
        
        if(useScaleAnimation){
            popup.transform.localScale = Vector3.one * 0.4f;
            popup.Scale(Vector3.one, fadeTime,CommonCurve.EaseIO);
        }
        if(arg == null)
            popup.SendMessage("Init");
        else
            popup.SendMessage("Init",arg);

        m_mem.Add(popup);
        return popup;
    }

    public static void Close(){
        var closetarget = Instance.m_mem.Last();
        Instance.m_mem.Remove(closetarget);
        if(Instance.m_mem.Count == 0){
            Instance.fade.AlphaTween(0f,Instance.fadeTime);
            Instance.fade.raycastTarget = false;
        }
        else
            Instance.fade.transform.SetSiblingIndex(Instance.m_mem.Count-1);//페이드를 맨뒤로

        Destroy(closetarget.gameObject);
    }

    public static void Close<T>(){
        
        
        GamePopupBase removeTarget = null;
        foreach(var t in Instance.m_mem)
            if(t is T)
            {
               removeTarget = t;
            }

        Instance.m_mem.Remove(removeTarget);
        if(Instance.m_mem.Count == 0){
            Instance.fade.AlphaTween(0f,Instance.fadeTime);
            Instance.fade.raycastTarget = false;
        }
        else
            Instance.fade.transform.SetSiblingIndex(Instance.m_mem.Count-1);//페이드를 맨뒤로

        if (removeTarget != null)
            Destroy(removeTarget.gameObject);
    }

    public void CloseLocal(){
        Close();
    }

    public static bool isOpen<T>()where T:GamePopupBase{
        foreach(var t in Instance.m_mem){
            if(t is T)
                return true;
        }
        return false;
    }


}

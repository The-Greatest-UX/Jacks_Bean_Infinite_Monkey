using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sound : MonoSingleton<Sound>
{
    public AudioSource asBG;
    public List<AudioSource> asEff;

    public bool bBgmMute = false;
    public bool bEffMute = false;

    public List<AudioClipItemObj> audioClipDatas = new List<AudioClipItemObj>();
    Dictionary<string, AudioClip> dicClips = new Dictionary<string, AudioClip>();

    public bool useInitInside = false;

    private void Awake()
    {
        InitDic();

    }

    private void InitDic()
    {

        foreach (var data in audioClipDatas)
        {
            foreach (var clip in data.m_dic)
            {
                if (!dicClips.ContainsKey(clip.Key))
                    dicClips.Add(clip.Key, clip.Value);
                else
                    Debug.LogError("[Sound] Same Key in Sound!!\nkey : " + clip.Key);
            }
        }
    }

    public void BGMPlay(string key)
    {
        if (dicClips.ContainsKey(key))
            asBG.clip = dicClips[key];
        else
            Debug.LogError("[BGMPlay] Not exist Key!! key : " + key);

        asBG.loop = true;
        asBG.Play();
    }

    public void EffPlay(string key, float vol = 1f)
    {
        for (int a = 0; a < asEff.Count; a++)
        {
            if (!asEff[a].isPlaying)
            {
                asEff[a].volume = vol;
                if (dicClips.ContainsKey(key))
                {
                    Debug.Log("Sound EffPlay key : " + key);
                    asEff[a].PlayOneShot(dicClips[key]);
                    Debug.Log("[EffPlay] key : " + key);

                }
                else
                    Debug.LogError("[EffPlay] Not exist Key!! key : " + key);
                return;
            }
        }
    }

    public void LimitedEffPlay(string key, int maxCount, float vol = 1f)
    {
        if (!dicClips.ContainsKey(key))
        {
            Debug.LogError("[EffPlay] Not exist Key!! key : " + key);
            return;
        }

        int cnt = 0;
        for (int a = 0; a < asEff.Count; a++)
        {
            if (asEff[a].isPlaying)
            {
                if (asEff[a].clip == null) continue;

                if (asEff[a].clip.name == dicClips[key].name)
                cnt++;
            }
        }

        if (cnt >= maxCount) return;
        
        EffPlay(key, vol);
    }

    //mute
    public void AllMute(bool bMute)
    {
        // 뭔가 글로벌로 쓸거 하나 있어야 할듯 
        BGMMute(bMute);
        EffMute(bMute);
    }
    public void BGMMute(bool bMute)
    {
        bBgmMute = bMute;
        asBG.mute = bMute;
    }

    public void EffMute(bool bMute)
    {
        bEffMute = bMute;
        // 뭔가 글로벌로 쓸거 하나 있어야 할듯 

        for (int i = 0; i < asEff.Count; i++)
            asEff[i].mute = bMute;

    }
    public AudioClip GetAudioClip(string key)
    {
        if (dicClips.ContainsKey(key))
            return dicClips[key];
        else
            Debug.LogError("[EffPlay] Not exist Key!! key : " + key);
        return null;
    }
}
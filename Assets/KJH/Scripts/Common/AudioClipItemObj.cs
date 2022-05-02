using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ObjData/AudioClipItemObj")]
public class AudioClipItemObj : ScriptableObject, ISerializationCallbackReceiver
{
    [OneLine.OneLine][OneLine.HideLabel]
    public List<AudioClipData> list = new List<AudioClipData>();
    public Dictionary<string, AudioClip> m_dic = new Dictionary<string, AudioClip>();

    public AudioClip GetValue(string id)
    {
        if (!m_dic.ContainsKey(id))
            throw new Exception("해당하는 id가 없습니다. id : " + id);

        return m_dic[id];
    }

    public void OnAfterDeserialize()
    {
        int count = 1;
        m_dic.Clear();
        foreach (var item in list)
        {
            if (m_dic.ContainsKey(item.id))
            {
                m_dic.Add("Dummy Sound_" + count, item.data);
                count++;
            }
            else
                m_dic.Add(item.id, item.data);
        }
    }

    public void OnBeforeSerialize()
    {
        list.Clear();
        foreach (var pair in m_dic)
            list.Add(new AudioClipData(pair.Key, pair.Value));
    }
}

[Serializable]
public class AudioClipData
{
    public string id;
    public AudioClip data;

    public AudioClipData(string id, AudioClip data)
    {
        this.id = id;
        this.data = data;
    }
}

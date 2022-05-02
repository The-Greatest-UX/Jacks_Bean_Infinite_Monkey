﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// float으로 치환된다.(time container가 필요한 곳에 1f 이런식으로 넣어도 됨.))
/// </summary>
public class TimeContainer
{
    public float t = 0f;
    public float time = 1f;
    public static Dictionary<string, List<TimeContainer>> st_allList = new Dictionary<string, List<TimeContainer>>();
    public string _id;
    private bool m_cancel = false;
    private bool m_neverCencel = false;
    public TimeContainer(string id, float time,bool neverCancel= false)
    {
        m_neverCencel = neverCancel;
        _id = id;
        t = 0;
        this.time = time;
        if (!st_allList.ContainsKey(id))
            st_allList.Add(id, new List<TimeContainer>());
        st_allList[id].Add(this);
    }
    public static int GetCount(string m_listName)
    {
        if (st_allList.ContainsKey(m_listName))
            return st_allList[m_listName].Count;
        else
            return 0;
    }
    public static void Clear(string m_listName, bool forceClear= false)
    {
        if (!st_allList.ContainsKey(m_listName))
            return;
        foreach (var timeContainer in st_allList[m_listName])
        {
            if(timeContainer.m_neverCencel && !forceClear)
                continue;

            timeContainer.t = 1f;
            timeContainer.m_cancel = true;
        }
    }

    public static void ContainClear(string m_listName, bool forceClear = false)
    {
        if (!st_allList.ContainsKey(m_listName))
            return;

        foreach (var list in st_allList)
        {
            if (list.Key.Contains(m_listName))
            {
                foreach (var timeContainer in list.Value)
                {
                    if (timeContainer.m_neverCencel && !forceClear)
                        continue;

                    timeContainer.t = 1f;
                    timeContainer.m_cancel = true;
                }
            }
        }
    }

    public void Clear()
    {
        t = 1f;
        m_cancel = true;
    }

    public void Complete()
    {
        //if(st_allList[_id].Contains(this))
            st_allList[_id].Remove(this);
    }
    
    public void CheckComplete(){
        if(t != 1f){
            if(st_allList[_id].Contains(this))
                st_allList[_id].Remove(this);
        }
    }
    public static implicit operator TimeContainer(float time)
    {
        return new TimeContainer("AutoBinding", time);
    }
    public static void AllClear()
    {
        foreach (string listName in st_allList.Keys)
        {
            Clear(listName);
        }
    }


    public class Stack : IEnumerable<TimeContainer> {
        Stack<TimeContainer> m_stack = new Stack<TimeContainer>();
  
        public Stack(int count, float time,string id){
            for (int i = 0; i < count; i++)
            {
                m_stack.Push(new TimeContainer(id,time));
            }
        }
        public int Count {
            get{return m_stack.Count;}
        }

        public void Push(TimeContainer timeContainer){
            m_stack.Push(timeContainer);
        }

        public IEnumerator<TimeContainer> GetEnumerator()
        {
            foreach(var tc in m_stack)
                yield return tc;
        }

        public TimeContainer Pop(){
            if(m_stack.Count == 0)
            {
                Debug.LogError("[TimeContainer.Stack] no item");
                return null;
            }

            return m_stack.Pop();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator TimeContainer.Stack(float time)
        {
            Debug.LogWarning("앵간하면 테스트용으로만 사용 몇개를 만들어야 되는지 모름");
            //런타임중에는 쓰지 말것. 테스트 용으로만 사용
            return new TimeContainer.Stack(10, time,"StackAutoBinding");
        }


    }

    public class StackSum : IEnumerable<TimeContainer>{
        TimeContainer.Stack[] _stacks;
        public StackSum(params TimeContainer.Stack[] stacks){
            _stacks = stacks;
        }

        public IEnumerator<TimeContainer> GetEnumerator()
        {
            foreach(var s in _stacks){
                foreach(var tc in s){
                    yield return tc;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
            
        }
    }

    
}
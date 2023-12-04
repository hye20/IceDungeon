using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portaitData;
    public Sprite[] portaitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portaitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    private void GenerateData()
    {
        // Talk
        talkData.Add(1000, new string[] { "안녕? :0", "이 곳에 처음 왔구나? :4" });

        talkData.Add(2000, new string[] { "안녕 하냥? :0", "이 곳에 처음 왔구냐? :4" });

        talkData.Add(100, new string[] { "평범한 나무상자다" });
        talkData.Add(200, new string[] {"누군가 사용한 흔적이 있는 책상이다"});

        //Sprite Gm
        portaitData.Add(1000 + 0, portaitArr[0]);
        portaitData.Add(1000 + 1, portaitArr[1]);
        portaitData.Add(1000 + 2, portaitArr[2]);
        portaitData.Add(1000 + 3, portaitArr[3]);
        portaitData.Add(1000 + 4, portaitArr[4]);
        portaitData.Add(2000 + 5, portaitArr[5]);
        portaitData.Add(2000 + 0, portaitArr[6]);
        portaitData.Add(2000 + 1, portaitArr[7]);
        portaitData.Add(2000 + 2, portaitArr[8]);
        portaitData.Add(2000 + 3, portaitArr[9]);
        portaitData.Add(2000 + 4, portaitArr[10]);
    }


    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex]; 
        }
    }

    public Sprite GetPortait(int id, int portraitIndex)
    {
        return portaitData[id + portraitIndex];
    }
}

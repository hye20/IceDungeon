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
        // 기본 대화 퀘스트가 없을때
        NoneTalk();

        // Quest Talk
        talkData.Add(10 + 1000, new string[] {  "잘둘러 봤어? :0",
                                                "이 동굴에는 전설이 있어 :4" ,
                                                "왼쪽의 Angel이 알려줄거야!!:2"});
        talkData.Add(11 + 1000, new string[] {  "Angel에게 안 가봤어?:0",
                                                "왼쪽에 있을거야 :4" });

        /*talkData.Add(11 + 2000, new string[] {  "Gm 한테 내용 들었어?:0",
                                                "응 이동굴의 전설은 ...:4" ,
                                                "Shaman Variant 말을 걸어야해!!:2"});*/

        talkData.Add(20 + 1000, new string[] { "Shaman Variant 만나고 옴 :0" });
        /*  talkData.Add(20 + 2000, new string[] { "아직 이야기 진행중:0" });
          talkData.Add(20 + 3000, new string[] { "Gm에게 가야할듯...:4" });
          talkData.Add(21 + 2000, new string[] { "Gm에게 가야할듯...:0" });*/


        talkData.Add(30 + 1000, new string[] { "선택형 퀘스트의 시작:0" });
        talkData.Add(31 + 2000, new string[] { "Gm에게 가야할듯...:0" });

        talkData.Add(40 + 1000, new string[] { "1.A 선택지, 2.B 선택지" });

        talkData.Add(50 + 1000, new string[] { "1.A 선택지 결과값:0" });
        talkData.Add(51 + 1000, new string[] { "퀘스트 종료로 넘김:0" });

        talkData.Add(60 + 1000, new string[] { "2.B 선택지 결과값:1" });

        talkData.Add(70 + 1000, new string[] { "70퀘스트 종료:0" });
        talkData.Add(80 + 1000, new string[] { "80퀘스트 종료:0" });


        NonePortait();
    }


    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (talkData.ContainsKey(id - id % 10))
            { // 순서에 따라 변경되는 대사가 없을때 -> 기본대사를 가져온다
                return GetTalk(id - id % 10, talkIndex);
            }
            else
            {  // 해당 퀘스트 진행 순서 대사가 없을 때 -> 맨 처음 대사를 가져온다
                return GetTalk(id - id % 100, talkIndex);
            }
        }

        if (talkIndex == talkData[id].Length) { return null; }
        else { return talkData[id][talkIndex]; }
    }

    void NoneTalk()
    {
        // Talk Data
        // Npc Gm: 1000, Angel: 2000
        // Box: 100 , Desk: 200
        talkData.Add(1000, new string[] {   "안녕? :0",
                                            "이 곳에 처음 왔구나? :4" ,
                                            "한번 둘러보도록해:1"});
        talkData.Add(2000, new string[] {   "안녕 하냥? :0",
                                            "이 곳에 처음 왔구냐? :4",
                                            "Gm을 찾아 보는 것도 나쁘지 않다냐?:1"});

        talkData.Add(100, new string[] { "안이 비어있는 상자다" });
        talkData.Add(600, new string[] { "한번 사용할 독백 창이다",
                                         "이 독백창은 1번 가동후 사라진다"});
    }

    void NonePortait()
    {
        // 사진 관리 (바뀌는 이미지)
        // 0: Normal, 1: Speak, 2: Happy, 3: Angry, 4:...., 5:....
        portaitData.Add(1000 + 0, portaitArr[0]);
        portaitData.Add(1000 + 1, portaitArr[1]);
        portaitData.Add(1000 + 2, portaitArr[2]);
        portaitData.Add(1000 + 3, portaitArr[3]);
        portaitData.Add(1000 + 4, portaitArr[4]);
        portaitData.Add(1000 + 5, portaitArr[5]);
        portaitData.Add(2000 + 0, portaitArr[6]);
        portaitData.Add(2000 + 1, portaitArr[7]);
        portaitData.Add(2000 + 2, portaitArr[8]);
        portaitData.Add(2000 + 3, portaitArr[9]);
        portaitData.Add(2000 + 4, portaitArr[10]);
        portaitData.Add(2000 + 5, portaitArr[11]);
        portaitData.Add(3000 + 0, portaitArr[12]);
        portaitData.Add(3000 + 1, portaitArr[13]);
        portaitData.Add(3000 + 2, portaitArr[14]);
        portaitData.Add(3000 + 3, portaitArr[15]);
        portaitData.Add(3000 + 4, portaitArr[16]);
        portaitData.Add(3000 + 5, portaitArr[17]);
    }

    public Sprite GetPortait(int id, int portraitIndex)
    {
        return portaitData[id + portraitIndex];
    }
}
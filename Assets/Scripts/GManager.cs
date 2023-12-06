using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    InteractionType interactionType;
    public TalkManager talkManager;
    public QuestManager questManager;

    // 대화창
    [Header ("TalkDateWrite")]
    public GameObject talkPanel;
    public Text talkText;
    public Text talkName;
    public Image talkImg;

    public Button[] btnList;

    public GameObject scanObject;
    public bool isAction = false;
    public int talkIndex;

    public void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }


    // 이 행동을 실행하면
    // 대화를 한다
    public void Action(GameObject _scanObject)
    {
        isAction = true;
        scanObject = _scanObject;
        interactionType = scanObject.GetComponent<InteractionType>();
        Talk(interactionType.id, interactionType.isNpc);

        talkPanel.SetActive(isAction);
        
    }
    
    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        Debug.Log(questTalkIndex);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        Debug.Log(id + questTalkIndex + talkIndex);

        // Talk 데이터가 값을 가지고 있지 않으면
        // Talk 의 종료
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Time.timeScale = 1;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        if (Time.timeScale != 0)
        {
            Time.timeScale = 0; 
        }

        // 이름과 대화창을 끌어옴
        if (isNpc)
        {
            if (questManager.choice==false)
            {
                talkName.text = interactionType.GetName();
                talkText.text = talkData.Split(':')[0];
                talkImg.sprite = talkManager.GetPortait(id, int.Parse(talkData.Split(':')[1]));

            }
            else if(questManager.choice == true)
            {
                talkName.text = interactionType.GetName();

                for (int i = 0; i < talkData.Split(',').Length; i++)
                {
                    btnList[i].gameObject.SetActive(true);
                    btnList[i].gameObject.GetComponentInChildren<Text>().text = talkData.Split(",")[i];
                    // 각 버튼에 대한 이벤트 추가
                    int index = i; // 중요: 람다식 내에서 반복 변수를 사용할 때 변수 복사
                    btnList[i].onClick.AddListener(() => OnClickButton(index, id));
                }
                for (int i = talkData.Split(',').Length; i < btnList.Length; i++)
                {
                    btnList[i].gameObject.SetActive(false);
                    btnList[i].gameObject.GetComponentInChildren<Text>().text = "";
                }
                talkImg.sprite = talkManager.GetPortait(id, int.Parse(talkData.Split(':')[1]));
            }

            talkImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkName.text = interactionType.GetName();
            talkText.text = talkData;
            talkImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

    void OnClickButton(int buttonIndex, int id)
    {
        questManager.ChoiceQuest(buttonIndex+1,id);
        for (int i = 0; i < btnList.Length; i++)
        {
            btnList[i].gameObject.SetActive(false);
        }
        talkPanel.SetActive(true);
    }
}

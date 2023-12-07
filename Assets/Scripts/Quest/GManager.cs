using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class GManager : MonoBehaviour
{
    InteractionController interactionController;
    InteractionType interactionType;
    public TalkManager talkManager;
    public QuestManager questManager;

    // 대화창
    [Header ("TalkDateWrite")]
    public GameObject talkPanel;
    public Text talkText;
    public Text talkName;
    public Image talkImg;
    // 퀘스트 창
    public TMP_Text questTitle;
    public TMP_Text questDetail;

    public Button[] btnList;

    public GameObject scanObject;
    public bool isAction = false;
    public int talkIndex;

    // 존재하는 버튼 수
    int btnint;
    //
    public int lbtnint;
    public void Awake()
    {
        interactionController = GetComponent<InteractionController>();
        interactionType = GetComponent<InteractionType>();
    }

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
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        Debug.Log($" id: {id} ,questTalkIndex: {questTalkIndex}, talkIndex: {talkIndex}");

        // Talk 데이터가 값을 가지고 있지 않으면
        // Talk 의 종료
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Time.timeScale = 1;
            questDetail.text = questManager.CheckQuest(id);
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
                btnint = talkData.Split(',').Length;
                for (int i = 0; i < talkData.Split(',').Length; i++)
                {
                    btnList[i].gameObject.SetActive(true);
                    btnList[i].gameObject.GetComponentInChildren<Text>().text = talkData.Split(",")[i];
                    // 각 버튼에 대한 이벤트 추가
                    int index = i; // 중요: 람다식 내에서 반복 변수를 사용할 때 변수 복사
                    btnList[i].onClick.AddListener(() => OnClickButton(index));
                }
                for (int i = talkData.Split(',').Length; i < btnList.Length; i++)
                {
                    btnList[i].gameObject.SetActive(false);
                    btnList[i].gameObject.GetComponentInChildren<Text>().text = "";
                }
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

    void OnClickButton(int buttonIndex)
    {
        lbtnint = buttonIndex - btnint;
        questManager.btnChoicNum = buttonIndex + 1;
        for (int i = 0; i < btnList.Length; i++)
        {
            btnList[i].gameObject.SetActive(false);
        }
    }
}

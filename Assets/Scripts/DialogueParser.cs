using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    //  CSV 파일을 분석하고 Dialogue 배열을 반환
    public Dialogue[] Parser(string _CSVFileName)
    {
        // 대사리스트 생성
        List<Dialogue> dialogueList = new List<Dialogue>(); 
        // Csv파일 로드
        TextAsset csvDate = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvDate.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); 
            
            Dialogue dialogue = new Dialogue(); 
            dialogue.name = row[1];

            List<string> contextList = new List<string>();
            // 현재 캐릭터에 대한 대화 라인 추출
            do
            {
                contextList.Add(row[2]);
                if (++i < data.Length) {
                    row = data[i].Split(new char[] { ',' });
                }
                else {
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray(); 
            dialogueList.Add(dialogue);
        }
        return dialogueList.ToArray();
    }

    private void Start()
    {
        Parser("NpcData");
    }
}

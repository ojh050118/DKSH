using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        InteractionableInfo objData = scanObject.GetComponent<InteractionableInfo>();
        Talk(objData.ID, objData.isNPC);
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkindex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+questTalkindex, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
        
        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }
        isAction = true;
        talkIndex++;
    }
}

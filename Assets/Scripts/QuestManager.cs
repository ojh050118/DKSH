using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static int questId;
    public static int questActionIndex;
    public GameObject[] questObject;

    public TextAsset QuestDataJson;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = parseQuestData(QuestDataJson.text);
        if (questId == 0) questId = 10;
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {

        //Next Talk Object
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        //Control Quest Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }

        //Return Quest Name
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //Return Quest Name
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        //switch(questId)
        //{
        //    case 10:
        //        if (questActionIndex == 2)
        //        {
        //            questObject[0].SetActive(true);
        //        }
        //        break;
        //    case 20:
        //        if (questActionIndex == 1)
        //        {
        //            questObject[0].SetActive(false);
        //        }
        //        break;
        //}
    }

    Dictionary<int, QuestData> parseQuestData(string json)
    {
        var questData = JsonConvert.DeserializeObject<QuestList[]>(json, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        });

        Dictionary<int, QuestData> result = new Dictionary<int, QuestData>();

        foreach (var data in questData)
            result.Add(data.Id, data.quest);

        return result;
    }
}

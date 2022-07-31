using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public TextAsset QuestDataJson;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = parseQuestData(QuestDataJson.text);
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId;
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

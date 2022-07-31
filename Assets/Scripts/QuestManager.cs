using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public TextAsset QuestDataJson;

    Dictionary<int, QuestData> questData;

    public int Current = 0;
    public int CurrentNpcIDIndex;
    public QuestData CurrentQuestData => GetQuestData(Current);

    void Awake()
    {
        questData = parseQuestData(QuestDataJson.text);
    }

    public string GetQuestTalk(int npcID, int index)
    {
        if (questData[Current].TalkData.FirstOrDefault(t => t.ID == npcID) == null)
            return null;

        var talkData = questData[Current].TalkData.FirstOrDefault(t => t.ID == npcID);

        if (index >= talkData.Data.Length || index < 0)
            return null;
        else
            return talkData.Data[index];
    }

    public string GetQuestMessage() => questData[Current].QuestMessage;

    public QuestData GetQuestData(int id)
    {
        if (!questData.ContainsKey(id))
            return null;
        else
            return questData[id];
    }

    public void ProcessQuest()
    {
        CurrentNpcIDIndex++;
    }

    Dictionary<int, QuestData> parseQuestData(string json)
    {
        var questData = JsonConvert.DeserializeObject<QuestList[]>(json, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        });

        Dictionary<int, QuestData> result = new Dictionary<int, QuestData>();

        foreach (var data in questData)
            result.Add(data.ID, data.Data);

        return result;
    }
}

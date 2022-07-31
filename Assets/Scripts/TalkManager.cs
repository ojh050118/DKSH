using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    /// <summary>
    /// NPC�� ��簡 ���ԵǾ��ִ� json������ ����.
    /// </summary>
    public TextAsset TalkDataJson;

    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = parseTalkData(TalkDataJson.text);
    }

    public string GetTalk(int? id, int index)
    {
        if (!id.HasValue)
            return null;

        //ID�� NPCTalkData�� �������� �ʰų� ��ȭ �ε����� ������ �ʰ��ϸ� null�� ��ȯ�մϴ�.
        if (!talkData.ContainsKey(id.Value) || index >= talkData[id.Value].Length)
            return null;
        else
            return talkData[id.Value][index];
    }

    public string[] GetData(int id)
    {
        if (!talkData.ContainsKey(id))
            return new string[0];
        else
            return talkData[id];
    }

    /// <summary>
    /// json������ ������ ������ȭ�� �����մϴ�.
    /// </summary>
    /// <param name="json"><see cref="TalkData"/>[]�� �̷���� json ������ �ؽ�Ʈ.</param>
    /// <returns>ID�� Data�� �̷���� ��ųʸ� ��ü.</returns>
    Dictionary<int, string[]> parseTalkData(string json)
    {
        var talkData = JsonConvert.DeserializeObject<TalkData[]>(json, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        });

        Dictionary<int, string[]> result = new Dictionary<int, string[]>();

        foreach (var data in talkData)
            result.Add(data.ID, data.Data);

        return result;
    }

    public class TalkData
    {
        public int ID;

        public string[] Data;
    }
}

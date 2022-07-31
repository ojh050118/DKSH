using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    /// <summary>
    /// NPC의 대사가 포함되어있는 json형식의 내용.
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

        //ID가 NPCTalkData에 존재하지 않거나 대화 인덱스가 범위를 초과하면 null을 반환합니다.
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
    /// json형식의 파일을 역직렬화를 시작합니다.
    /// </summary>
    /// <param name="json"><see cref="TalkData"/>[]로 이루어진 json 형식의 텍스트.</param>
    /// <returns>ID와 Data로 이루어진 딕셔너리 객체.</returns>
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

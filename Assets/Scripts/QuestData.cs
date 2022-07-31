public class QuestList
{
    public int ID;
    public QuestData Data;
}
public class QuestData
{
    public string QuestMessage;
    public int[] NpcID;
    public TalkManager.TalkData[] TalkData;
}

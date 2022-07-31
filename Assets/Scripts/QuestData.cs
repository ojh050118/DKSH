using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestList : MonoBehaviour
{
    public int Id;
    public QuestData quest;
}
public class QuestData : MonoBehaviour
{
    public string questName;
    public int[] npcId;
}

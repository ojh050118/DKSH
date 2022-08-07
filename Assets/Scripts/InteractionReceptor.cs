using UnityEngine;
using UnityEngine.UI;

public class InteractionReceptor : MonoBehaviour
{
    public GameObject InteractionTarget;
    public GameObject Dialog;
    public Text Textfield;

    public TalkManager TalkManager;
    public QuestManager questManager;

    /// <summary>
    /// ���� ��ȣ�ۿ��ϰ��ִ� ��ü�� ID, ��ȭ �ε���.
    /// </summary>
    (int? id, int index) current = (null, 0);
    int questTalkIndex;

    string currentTalk => TalkManager.GetTalk(current.id.Value + questTalkIndex, current.index);

    /// <summary>
    /// ��ȣ�ۿ��� ������Ʈ�� ��ȣ�ۿ��� �����մϴ�.
    /// ��ȣ�ۿ� �߿� �ٽ� �� �Լ��� ȣ���ϸ� TalkData�� Ȯ���� ��ȣ�ۿ��� ��������� �����մϴ�.
    /// </summary>
    /// <param name="interactionTarget">��ȣ�ۿ��� ������ ������Ʈ.</param>
    public void Interaction(GameObject interactionTarget)
    {
        //questTalkIndex = questManager.GetQuestTalkIndex(current.id.Value);
        // ���� ID�� null�� �ƴ� �� ��ȣ�ۿ��� �ϰ������� �ǹ��մϴ�.
        // ��ȣ�ۿ��� ������ �ϱ⶧���� ��ȭ���ڸ� ��Ȱ��ȭ�ϰ� InteractionTarget�� ���� null�� ����ϴ�.
        if (current.id.HasValue && string.IsNullOrEmpty(currentTalk))
        {
            InteractionTarget = null;
            current = (null, 0);
            Dialog.SetActive(false);
            return;
        }

        var interactionableData = interactionTarget.GetComponent<InteractionableInfo>();
        current.id = interactionableData.ID;
        InteractionTarget = interactionTarget;

        string questName = questManager.CheckQuest(current.id.Value);
        Debug.Log(questName);
        questTalkIndex = questManager.GetQuestTalkIndex(current.id.Value);

        Dialog.SetActive(interactionTarget != null);
        Textfield.text = currentTalk;
        current.index++;
    }
}

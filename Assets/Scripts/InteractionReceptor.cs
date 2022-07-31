using UnityEngine;
using UnityEngine.UI;

public class InteractionReceptor : MonoBehaviour
{
    public GameObject InteractionTarget;
    public GameObject Dialog;
    public Text Textfield;

    public TalkManager TalkManager;
    public QuestManager QuestManager;

    /// <summary>
    /// ���� ��ȣ�ۿ��ϰ��ִ� ��ü�� ID, ��ȭ �ε���.
    /// ��ȣ�ۿ� ���� ���°� �ƴϸ� ID�� ���� null�Դϴ�.
    /// </summary>
    (InteractionableInfo info, int index) current = (null, 0);

    string currentTalk => TalkManager.GetTalk(current.info?.ID, current.index);

    string questTalk => QuestManager.GetQuestTalk(0, current.index);

    /// <summary>
    /// ��ȣ�ۿ��� ������Ʈ�� ��ȣ�ۿ��� �����մϴ�.
    /// ��ȣ�ۿ� �߿� �ٽ� �� �Լ��� ȣ���ϸ� TalkData�� Ȯ���� ��ȣ�ۿ��� ��������� �����մϴ�.
    /// </summary>
    /// <param name="interactionTarget">��ȣ�ۿ��� ������ ������Ʈ.</param>
    public void Interaction(GameObject interactionTarget)
    {
        // ���� ID�� null�� �ƴ� �� ��ȣ�ۿ��� �ϰ������� �ǹ��մϴ�.
        // ��ȣ�ۿ��� ������ �ϱ⶧���� ��ȭ���ڸ� ��Ȱ��ȭ�ϰ� InteractionTarget�� ���� null�� ����ϴ�.
        if (IsInteractionEnded())
        {
            InteractionTarget = null;
            current = (null, 0);
            Dialog.SetActive(false);
            return;
        }

        current.info = interactionTarget.GetComponent<InteractionableInfo>();
        InteractionTarget = interactionTarget;

        Dialog.SetActive(true);
        showText();
        current.index++;
    }

    private void showText()
    {
        var text = QuestManager.GetQuestTalk(current.info.ID, current.index);
        Textfield.text = text ?? TalkManager.GetTalk(current.info?.ID, current.index);
    }

    private bool IsInteractionEnded()
    {
        if (current.info != null)
        {
            if (current.index >= TalkManager.GetData(current.info.ID).Length || current.index >= QuestManager.CurrentQuestData.TalkData.Length)
                return true;
        }

        return false;
    }
}

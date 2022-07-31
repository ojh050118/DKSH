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
    /// 현재 상호작용하고있는 개체의 ID, 대화 인덱스.
    /// 상호작용 중인 상태가 아니면 ID의 값은 null입니다.
    /// </summary>
    (InteractionableInfo info, int index) current = (null, 0);

    string currentTalk => TalkManager.GetTalk(current.info?.ID, current.index);

    string questTalk => QuestManager.GetQuestTalk(0, current.index);

    /// <summary>
    /// 상호작용한 오브젝트와 상호작용을 시작합니다.
    /// 상호작용 중에 다시 이 함수를 호출하면 TalkData를 확인해 상호작용을 멈출것인지 결정합니다.
    /// </summary>
    /// <param name="interactionTarget">상호작용이 가능한 오브젝트.</param>
    public void Interaction(GameObject interactionTarget)
    {
        // 현재 ID가 null이 아닐 땐 상호작용을 하고있음을 의미합니다.
        // 상호작용을 끝내야 하기때문에 대화상자를 비활성화하고 InteractionTarget의 값을 null로 만듭니다.
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

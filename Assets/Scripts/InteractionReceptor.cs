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
    /// 현재 상호작용하고있는 개체의 ID, 대화 인덱스.
    /// </summary>
    (int? id, int index) current = (null, 0);
    int questTalkIndex;

    string currentTalk => TalkManager.GetTalk(current.id.Value + questTalkIndex, current.index);

    /// <summary>
    /// 상호작용한 오브젝트와 상호작용을 시작합니다.
    /// 상호작용 중에 다시 이 함수를 호출하면 TalkData를 확인해 상호작용을 멈출것인지 결정합니다.
    /// </summary>
    /// <param name="interactionTarget">상호작용이 가능한 오브젝트.</param>
    public void Interaction(GameObject interactionTarget)
    {
        questTalkIndex = questManager.GetQuestTalkIndex(current.id.Value);
        // 현재 ID가 null이 아닐 땐 상호작용을 하고있음을 의미합니다.
        // 상호작용을 끝내야 하기때문에 대화상자를 비활성화하고 InteractionTarget의 값을 null로 만듭니다.
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

        Dialog.SetActive(interactionTarget != null);
        Textfield.text = currentTalk;
        current.index++;
    }
}

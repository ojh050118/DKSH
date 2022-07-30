using UnityEngine;
using UnityEngine.UI;

public class InteractionReceptor : MonoBehaviour
{
    public GameObject InteractionTarget;
    public GameObject Dialog;
    public Text Textfield;

    /// <summary>
    /// 상호작용한 오브젝트와 상호작용을 시작합니다.
    /// 상호작용 중에 다시 이 함수를 호출하면 상호작용을 중지합니다.
    /// </summary>
    /// <param name="interactionTarget">상호작용이 가능한 오브젝트.</param>
    public void Interaction(GameObject interactionTarget)
    {
        // InteractionTarget이 null이 아닐 땐 상호작용을 하고있음을 의미합니다.
        // 상호작용을 끝내야 하기때문에 대화상자를 비활성화하고 InteractionTarget의 값을 null로 만듭니다.
        if (InteractionTarget != null)
        {
            InteractionTarget = null;
            Dialog.SetActive(false);
            return;
        }

        Dialog.SetActive(interactionTarget != null);
        InteractionTarget = interactionTarget;
        Textfield.text = $"Object name: {interactionTarget.name}";
    }
}

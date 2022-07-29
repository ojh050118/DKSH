using UnityEngine;
using UnityEngine.UI;

public class InteractionReceptor : MonoBehaviour
{
    public GameObject InteractionTarget;
    public GameObject Dialog;
    public Text Textfield;

    private void Awake()
    {
        Dialog?.SetActive(false);
    }

    /// <summary>
    /// ��ȣ�ۿ��� ������Ʈ�� ��ȣ�ۿ��� �����մϴ�.
    /// ��ȣ�ۿ� �߿� �ٽ� �� �Լ��� ȣ���ϸ� ��ȣ�ۿ��� �����մϴ�.
    /// </summary>
    /// <param name="interactionTarget">��ȣ�ۿ��� ������ ������Ʈ.</param>
    public void Interaction(GameObject interactionTarget)
    {
        // InteractionTarget�� null�� �ƴ� �� ��ȣ�ۿ��� �ϰ������� �ǹ��մϴ�.
        // ��ȣ�ۿ��� ������ �ϱ⶧���� ��ȭ���ڸ� ��Ȱ��ȭ�ϰ� InteractionTarget�� ���� null�� ����ϴ�.
        if (InteractionTarget != null)
        {
            InteractionTarget = null;
            Dialog.SetActive(false);
            return;
        }

        Dialog.SetActive(interactionTarget != null);
        InteractionTarget ??= interactionTarget;
        Textfield.text = $"Object name: {interactionTarget.name}";
    }
}

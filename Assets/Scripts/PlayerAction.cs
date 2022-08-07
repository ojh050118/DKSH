using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction instance;

    public string currentMapName;

    public float Speed;
    public GameManager manager;

    Rigidbody2D rigid;
    Animator anim;

    Vector2 moveDelta;
    Vector3 currentDirection = Vector3.down;

    GameObject interactionTarget;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // �����̽� Ű�� ������ �� ��ȣ�ۿ� �� ����� �ִٸ� �� ������Ʈ�� ��ȣ�ۿ��� �����մϴ�.
        if (Input.GetKeyDown(KeyCode.Space) && interactionTarget != null)
            manager.Action(interactionTarget);

        // ��ȣ�ۿ� ���� �� �̵� �Է��� �����մϴ�.
        //if (mananger.scanObject != null)
        //    return;

        moveDelta.x = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        moveDelta.y = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // ���� �Է� ������ �÷��̾��� ������ �����մϴ�.
        // Todo: �밢�� �������� �̵��ϴ� ���� ������� �ʾƾ��մϴ�.
        if (moveDelta.x != 0)
            currentDirection = moveDelta.x < 0 ? Vector3.left : Vector3.right;

        if (moveDelta.y != 0)
            currentDirection = moveDelta.y < 0 ? Vector3.down : Vector3.up;
    }

    void FixedUpdate()
    {
        // �ִϸ��̼� �Ŵ����� �Ű������� ���� �����մϴ�.
        // �ִϸ��̼� �Ŵ����� �Ű����� ���� ���ϴ� ��� �ִϸ��̼��� �����մϴ�.
        // �����ϰ� �ִϸ��̼��� �����ؾ� �ϱ⶧���� FixedUpdate() �Լ����� �����ؾ��մϴ�.
        processAnimation();

        rigid.velocity = moveDelta * Speed;

        // �÷��̾� ���濡 ������Ʈ�� �ִ��� Ȯ���ϱ� ���� �÷��̾��� ��ġ���� �ϳ��� ���� ����ϴ�.
        // ������Ʈ�� Interactonable���̾ �ִ� ������Ʈ�� �����˴ϴ�.
        var rayHit = Physics2D.Raycast(rigid.position, currentDirection, 0.7f, LayerMask.GetMask("Interactionable"));

        // ���� �浹�� ������Ʈ�� ������ �װ��� ��ȣ�ۿ��� ������ ������Ʈ�Դϴ�.
        if (rayHit.collider != null)
            interactionTarget = rayHit.collider.gameObject;
        else
            interactionTarget = null;
    }

    void processAnimation()
    {
        if (anim.GetInteger("X") != moveDelta.x)
        {
            anim.SetBool("IsMoved", true);
            anim.SetInteger("X", (int)moveDelta.x);
        }
        else if (anim.GetInteger("Y") != moveDelta.y)
        {
            anim.SetBool("IsMoved", true);
            anim.SetInteger("Y", (int)moveDelta.y);
        }
        else
        {
            anim.SetBool("IsMoved", false);
        }
    }
}

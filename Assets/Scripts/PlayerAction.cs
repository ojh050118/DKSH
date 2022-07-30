using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction instance;

    public string currentMapName;

    public float Speed;
    public InteractionReceptor receptor;

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
        // 스페이스 키를 눌렀을 때 상호작용 할 대상이 있다면 그 오브젝트와 상호작용을 시작합니다.
        if (Input.GetKeyDown(KeyCode.Space) && interactionTarget != null)
            receptor.Interaction(interactionTarget);

        // 상호작용 중일 때 이동 입력은 무시합니다.
        if (receptor.InteractionTarget != null)
            return;

        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");

        // 현재 입력 값으로 플레이어의 방향을 결정합니다.
        // Todo: 대각선 방향으로 이동하는 것을 허용하지 않아야합니다.
        if (moveDelta.x != 0)
            currentDirection = moveDelta.x < 0 ? Vector3.left : Vector3.right;

        if (moveDelta.y != 0)
            currentDirection = moveDelta.y < 0 ? Vector3.down : Vector3.up;
    }

    void FixedUpdate()
    {
        // 애니메이션 매니저의 매개변수에 값을 설정합니다.
        // 애니메이션 매니저는 매개변수 값이 변하는 즉시 애니메이션을 실행합니다.
        // 일정하게 애니메이션을 실행해야 하기때문에 FixedUpdate() 함수에서 실행해야합니다.
        processAnimation();

        rigid.velocity = moveDelta * Speed;

        // 플레이어 전방에 오브젝트가 있는지 확인하기 위해 플레이어의 위치에서 하나의 선을 만듭니다.
        // 오브젝트는 Interactonable레이어에 있는 오브젝트로 한정됩니다.
        var rayHit = Physics2D.Raycast(rigid.position, currentDirection, 0.7f, LayerMask.GetMask("Interactionable"));

        // 선과 충돌한 오브젝트가 있으면 그것은 상호작용이 가능한 오브젝트입니다.
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

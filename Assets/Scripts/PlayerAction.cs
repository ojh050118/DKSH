using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction instance;

    public string currentMapName;

    public float Speed;
    public GameManager manager;
    public GameObject talkPanel;
    public Text talkText;



    Rigidbody2D rigid;
    Animator anim;

    Vector2 moveDelta;
    Vector3 currentDirection = Vector3.down;

    bool isHorizontal;

    GameObject interactionTarget;

    void Awake()
    {
        // 객체가 최초로 생성되면 정적 변수에 저장해 기억합니다.
        // 다시 같은 객체가 만들어 진다면 그 객체는 폐기됩니다.
        if (currentMapName == "Main")
        {
            talkText.text = "학교로 가십시오.";
            talkPanel.SetActive(true);
        }

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 스페이스 키를 눌렀을 때 상호작용 할 대상이 있다면 그 오브젝트와 상호작용을 시작합니다.
        if (Input.GetKeyDown(KeyCode.Space) && interactionTarget != null)
            manager.Action(interactionTarget);

        // 상호작용 중일 때 이동 입력은 무시합니다.
        moveDelta.x = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        moveDelta.y = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        if (moveDelta.x != 0)
            moveDelta.y = 0;

        var left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        var right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (left || right)
            isHorizontal = true;
        else
            isHorizontal = false;

        // 현재 입력 값으로 플레이어의 방향을 결정합니다.
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

        rigid.velocity = (isHorizontal ? new Vector2(moveDelta.x, 0) : new Vector2(0, moveDelta.y)) * Speed;

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

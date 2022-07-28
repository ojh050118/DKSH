using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    Vector2 moveDelta;

    Rigidbody2D rigid;
    Animator anim;

    Vector3 currentDirection = Vector3.down;

    GameObject interactionTarget;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");

        if (moveDelta.x != 0)
            currentDirection = moveDelta.x < 0 ? Vector3.left : Vector3.right;

        if (moveDelta.y != 0)
            currentDirection = moveDelta.y < 0 ? Vector3.down : Vector3.up;

        if (Input.GetKeyDown(KeyCode.Space) && interactionTarget != null)
            Debug.Log($"Current interaction target: {interactionTarget.name}");
    }

    void FixedUpdate()
    {
        anim.SetInteger("X", (int)moveDelta.x);
        anim.SetInteger("Y", (int)moveDelta.y);

        rigid.velocity = moveDelta * Speed;

        var rayHit = Physics2D.Raycast(rigid.position, currentDirection, 0.7f, LayerMask.GetMask("Interactionable"));

        if (rayHit.collider != null)
            interactionTarget = rayHit.collider.gameObject;
        else
            interactionTarget = null;
    }
}

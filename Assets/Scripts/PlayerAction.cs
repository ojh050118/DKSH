using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    Vector2 moveDelta;

    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        anim.SetInteger("X", (int)moveDelta.x);
        anim.SetInteger("Y", (int)moveDelta.y);

        rigid.velocity = moveDelta * Speed;
    }
}

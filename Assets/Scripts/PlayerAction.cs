using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public float Speed;

<<<<<<< Updated upstream
    float h;
    float v;
=======
    float x;
    float y;
>>>>>>> Stashed changes

    Rigidbody2D rigid;
    private Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
<<<<<<< Updated upstream
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        anim.SetFloat("MoveX", h);
        anim.SetFloat("MoveY", v);
=======
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        anim.SetInteger("X", (int)x);
        anim.SetInteger("Y", (int)y);
>>>>>>> Stashed changes
    }

    void FixedUpdate()
    {
<<<<<<< Updated upstream
        rigid.velocity = new Vector2(h, v) * Speed;
=======
        rigid.velocity = new Vector2(x, y) * Speed;
>>>>>>> Stashed changes
    }
}

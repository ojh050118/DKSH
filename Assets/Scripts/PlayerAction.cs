using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public float Speed;

    float x;
    float y;

    Rigidbody2D rigid;
    private Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (anim.GetInteger("X") != x)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("X", (int)x);
        } else if (anim.GetInteger("Y") != y)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("Y", (int)y);
        } else
        {
            anim.SetBool("isChange", false);
        }
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(x, y) * Speed;
    }
}

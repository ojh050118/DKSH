using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public float Speed;

    private float x;
    private float y;

    private Rigidbody2D rigid;
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

        anim.SetInteger("X", (int)x);
        anim.SetInteger("Y", (int)y);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(x, y) * Speed;
    }
}

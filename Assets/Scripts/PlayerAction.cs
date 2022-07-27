using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public float Speed;

    float h;
    float v;

    Rigidbody2D rigid;
    private Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        anim.SetFloat("MoveX", h);
        anim.SetFloat("MoveY", v);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v) * Speed;
    }
}

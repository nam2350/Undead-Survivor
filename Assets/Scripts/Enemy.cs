using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animaCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }
                    
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        spriter.flipX = target.position.x < rigid.position.x;    
    }
    void OnEnable()
    {
        target = GameManager.instance.player.gameObject.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animaCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
        {
            return;
        }
        //Debug.Log(collision.name);
        health -= collision.GetComponent<Bullet>().demage;

        if (health > 0)
        {
            // Hit Action

        }
        else
        {
            // DIE
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}

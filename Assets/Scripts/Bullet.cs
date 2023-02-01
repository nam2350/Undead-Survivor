using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float demage;
    public int per;
    

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }

    public void Init(float demage, int per, Vector3 dir)
    {
        this.demage = demage;
        this.per = per;

        if(per > -1)
        {
            rigid.velocity = dir * 10f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
        {
            return;
        }

        per--;

        if(per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }

    }
}

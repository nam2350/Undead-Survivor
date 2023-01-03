using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        /* 
         ��ġ �̵�
         1. ���� �ش�
            rigid.AddForce(inputVec);
         2. �ӵ� ����
            rigid.velocity = inputVec;
         3. ��ġ �̵�
            rigid.MovePosition(rigid.position + inputVec);
        */

        // ���� �̵�

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
}

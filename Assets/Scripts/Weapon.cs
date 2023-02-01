using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float demage;
    public int count;
    public float speed;

    private float timer;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();    
    }


    void Start()
    {
        Init();
        
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // Test Levelup
        if(Input.GetButtonDown("Jump")) {
            LevelUp(10, 1);
        }
    }

    void LevelUp(float demage, int count)
    {
        this.demage = demage;
        this.count += count;

        if(id == 0)
        {
            Batch();
        }
    }


    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150f;
                Batch();
                    
                break;

            default:
                speed = 1.0f;
                break;
        }
    }

    void Batch()
    {
        for (int index=0; index < count; index++)
        {
            Transform bullet;

            if(index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
            }
            bullet.parent = transform;

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(demage, -1, Vector3.zero); // -1 is Infinity Per.
        }
    }

    void Fire()
    {
        if (!player.scaner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = player.scaner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        Transform bullet =  GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(demage, count, dir);
    }
}

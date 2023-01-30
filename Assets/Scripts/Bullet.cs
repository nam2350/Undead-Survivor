using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float demage;
    public int per;

    public void Init(float demage, int per)
    {
        this.demage = demage;
        this.per = per;
    }
}

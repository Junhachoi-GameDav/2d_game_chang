using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    public float distance;
    public LayerMask isLayer;
    protected Vector2 home;
    protected bool isDie = false;
    protected bool isEnd = false;
    protected bool isDelay = false;
    protected bool isFollow = false;
    protected bool isDamage = false;
    protected bool isAttack = false;
    protected Transform player_position;
    //protected int Hp;
    public float isLeft;
    public float speed;
    protected int op = 0;//������
    public GameObject prefab_weapon;
    protected bool iseffect = false;

  

    public void damage()
    {
        isDamage = false;
        Debug.Log("is move");
    }
   
}

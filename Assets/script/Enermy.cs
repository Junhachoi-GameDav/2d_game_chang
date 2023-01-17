using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{

    public float distance;
    public LayerMask isLayer;
    protected Vector2 home;
    protected bool isEnd = false;
    protected bool isDelay = false;
    protected bool isFollow = false;
    protected bool isDamage = false;
    //protected int Hp;
    public float isLeft;
    public float speed;
    protected int op = 0;//º±≈√¡ˆ


  

    public void damage()
    {
        isDamage = false;
        Debug.Log("is move");
    }
}

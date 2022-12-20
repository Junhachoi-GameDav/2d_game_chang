using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //속도
    public float apply_speed; // 현재 스피드
    public float crouch_speed = 0f;
    public float run_speed = 6f;
    public float walk_speed= 3f;

    float x; // 가로 이동

    Rigidbody2D rigid; 
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); // 선언
        apply_speed = run_speed; // 기존 스피드는 run속도
    }

    void Update()
    {
        
    }
    // 이동은 효율을 위해 여기에 넣는다.
    void FixedUpdate()
    {
        player_move();
    }
    void player_move()
    {
        x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Horizontal"))
        {
            if(x != 0)
            {
                transform.localScale = new Vector3(x, 1, 1); //캐릭터 뒤집기
            }
            transform.Translate(new Vector3(x * apply_speed * Time.deltaTime, 0, 0));
        }
    }
}

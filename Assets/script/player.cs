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

    //점프
    public float jump_force = 10f; //점프 힘 값

    // 가로 이동 값
    float x; 

    //상태
    bool is_trun; //앞 ,뒤 전환 상태
    bool is_ground; //땅 상태
    bool ray_wall; //벽 상태

    Rigidbody2D rigid; 
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); // 선언
        apply_speed = run_speed; // 기존 스피드는 run속도
    }

    void Update()
    {
        check_wall_and_bottom();
        player_jump();
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

    //벽 && 바닥 체크.
    void check_wall_and_bottom()
    {
        if (x != 0)
        {
            if (x == 1)
            {
                is_trun = true;
            }
            else
            {
                is_trun = false;
            }
        }
        //땅체크
        Debug.DrawRay(rigid.position, Vector2.down * 1f, new Color(0, 1, 0));
        is_ground = Physics2D.Raycast(rigid.position, Vector2.down * 1f, 1f, LayerMask.GetMask("bottom"));
        
        //벽체크
        float cont_num = 0.5f;
        Debug.DrawRay(rigid.position, Vector2.right * (is_trun ? cont_num : cont_num * -1), new Color(0, 1, 0));
        ray_wall = Physics2D.Raycast(rigid.position, Vector2.right * (is_trun ? cont_num : cont_num * -1), cont_num, LayerMask.GetMask("bottom"));
    }

    void player_jump()
    {
        if (Input.GetButtonDown("Jump") && is_ground)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jump_force);
        }
    }
}

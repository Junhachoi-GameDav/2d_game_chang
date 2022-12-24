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
    public float hook_jump_force = 8f; //후크점프 힘 값

    // 가로,세로 이동 값
    float x;
    float y;


    //상태
    bool is_trun; //앞 ,뒤 전환 상태
    bool is_ground; //땅 상태
    bool ray_wall; //벽 상태
    public bool is_hook_range_max; // 갈고리 길이 최대 상태
    //public bool is_hook_range_min; // 갈고리 길이 최소 상태

    // 컴포넌트
    Rigidbody2D rigid;
    grapping grap;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); // 선언
        apply_speed = run_speed; // 기존 스피드는 run속도
        grap = GetComponent<grapping>();
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
        y = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Horizontal"))
        {
            if(x != 0)
            {
                transform.localScale = new Vector3(x, 1, 1); //캐릭터 뒤집기
            }
            if (grap.is_attach) // 갈고리에 붙을시
            {
                rigid.AddForce(new Vector2(x * apply_speed * Time.deltaTime, 0),ForceMode2D.Impulse); //좌,우 이동
            }
            else
            {
                transform.Translate(new Vector3(x * apply_speed * Time.deltaTime, 0, 0)); // 기본 이동
            }
        }
        if (Input.GetButton("Vertical") && !is_hook_range_max)
        {
            if (grap.is_attach) // 갈고리에 붙을시
            {
                transform.Translate(new Vector3(0, y * apply_speed * Time.deltaTime, 0)); //위,아래 이동
            }
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
        else if (Input.GetButtonDown("Jump") && grap.is_attach) // 갈고리에 붙을시
        {
            rigid.velocity = Vector2.zero;
            rigid.velocity = new Vector2(rigid.velocity.x, hook_jump_force);
        }
    }
}

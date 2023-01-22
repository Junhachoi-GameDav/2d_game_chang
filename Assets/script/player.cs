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
    public float dash_speed= 8f;

    //레이 길이
    public float ray_dis;
    public float ray_wall_dis;


    //타임
    float dash_timer;
    [Range(0.1f, 3)] public float dash_time;

    //점프
    public float jump_force = 10f; //점프 힘 값
    public float hook_jump_force = 8f; //후크점프 힘 값
    public float wall_jump_force = 5f; //벽점프 힘 값

    // 가로,세로 이동 값
    float x;
    float y;

    //기타 오브젝트
    public GameObject granade;
    public GameObject hiar;

    //총알 및 폭탄 힘, 기타 값
    public float g_force;

    //상태
    bool is_trun; //앞 ,뒤 전환 상태
    bool is_ground; //땅 상태
    bool is_air; //공중 상태
    bool is_wall_jump_ready; //벽 점프 준비 상태
    bool is_dash; //대쉬 상태
    bool ray_wall; //벽 상태
    public bool is_hook_range_max; // 갈고리 길이 최대 상태

    // 컴포넌트
    Rigidbody2D rigid;
    grapping grap;
    Animator anime;
    void Start()
    {
        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>(); // 선언
        apply_speed = run_speed; // 기존 스피드는 run속도
        grap = GetComponent<grapping>();
        is_trun = true;
    }

    void Update()
    {
        check_wall_and_bottom();
        player_jump();
        player_wall_jump();
        player_use_granade();
        player_dash();
    }

    // 이동은 효율을 위해 여기에 넣는다.
    void FixedUpdate()
    {
        player_move();
    }

    void player_move()
    {
        if (is_wall_jump_ready)
        {
            return; //위에 조건이면 함수를 끝냄.
        }
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Horizontal"))
        {
            anime.SetBool("is_run", true);
            hiar.transform.localPosition = new Vector3(0.05f, 0.15f, 0);
            if (x != 0)
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
        else
        {
            anime.SetBool("is_run", false);
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
        Debug.DrawRay(rigid.position, Vector2.down * ray_dis, new Color(0, 1, 0));
        is_ground = Physics2D.Raycast(rigid.position, Vector2.down * ray_dis, ray_dis, LayerMask.GetMask("bottom"));
        
        //벽체크
        Debug.DrawRay(rigid.position, Vector2.right * (is_trun ? ray_wall_dis : ray_wall_dis * -1), new Color(0, 1, 0));
        ray_wall = Physics2D.Raycast(rigid.position, Vector2.right * (is_trun ? ray_wall_dis : ray_wall_dis * -1), ray_wall_dis, LayerMask.GetMask("wall"));
    }

    void player_jump()
    {
        if (is_wall_jump_ready)
        {
            anime.SetBool("do_jump", false);
            return; //위에 조건이면 함수를 끝냄.
        }

        if (Input.GetButtonDown("Jump") && is_ground)
        {
            is_air = false;
            Invoke("jump_ani_deley", 0.5f);
            rigid.velocity = new Vector2(rigid.velocity.x, jump_force);
            anime.SetBool("do_jump", true);
        }
        else if (Input.GetButtonDown("Jump") && grap.is_attach) // 갈고리에 붙을시
        {
            is_air = false;
            Invoke("jump_ani_deley", 0.5f);
            rigid.velocity = Vector2.zero;
            rigid.velocity = new Vector2(rigid.velocity.x, hook_jump_force);
            anime.SetBool("do_jump", true);
        }
        if(is_air && is_ground)
        {
            anime.SetBool("do_jump", false);
        }
    }
    void jump_ani_deley()
    {
        is_air = true;
    }
    void player_wall_jump()
    {
        if(ray_wall && !is_ground) //벽에 붙었고 땅에 없을시.
        {
            #region 벽매달리기 취소
            //왼쪽 벽에서 오른쪽으로 가면 벽매달리기 취소
            
            if (!is_trun && Input.GetKey(KeyCode.D))
            {
                is_wall_jump_ready = false;
                anime.SetBool("is_wall", false);
                anime.SetBool("do_jump", true);
                return;
            }
            //오른쪽 벽에서 왼쪽으로 가면 벽매달리기 취소
            if (is_trun && Input.GetKey(KeyCode.A))
            {
                is_wall_jump_ready = false;
                anime.SetBool("is_wall", false);
                anime.SetBool("do_jump", true);
                return;
            }
            #endregion
            
            anime.SetBool("is_wall", true);

            is_wall_jump_ready = true; //벽점프 준비 완료.
            rigid.velocity = Vector2.zero; // 멈춤.
            rigid.gravityScale = 0;

            
            if (Input.GetButtonDown("Jump")) //벽에서 점프를 눌렀을시.
            {
                // is_trun이 트루면 왼쪽으로 펄스면 오른쪽으로 튕김 (즉 왼쪽벽에서 점프를 누르면 오른쪽으로 튕김)
                rigid.velocity = new Vector2(wall_jump_force * (is_trun ? -1 : 1), wall_jump_force * 1.5f);
                anime.SetBool("is_wall", false);
                anime.SetBool("do_jump", true);
                Invoke("wall_jump_deley", 0.15f); // 튕기고 딜레이
            }
        }
        else
        {
            rigid.gravityScale = 2.5f;
        }
    }
    void wall_jump_deley()
    {
        is_wall_jump_ready = false;
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    void player_use_granade()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 참고로 중력값은 2이다.
            GameObject ins_granade = Instantiate(granade, transform.position, transform.rotation); // 캐릭터 위치에서 생성, 나중에 오브젝트 풀링 해줄거임.
            Rigidbody2D rigid_granade = ins_granade.GetComponent<Rigidbody2D>(); // 물리 선언
            
            //캐릭터위치에서 (위* 힘* 조절) + (오른쪽 * 힘* 캐릭터 바라보는 방향) = 대각선으로 포물선을 그린다.
            rigid_granade.velocity = (transform.up * g_force *0.7f ) + (transform.right * g_force * (is_trun ? 1 : -1));
           
        }
    }
    
    void player_dash()
    {
        if(x != 0) //서있지 않을 때
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                is_dash = true;

                if (is_dash)
                {
                    dash_timer += Time.deltaTime;
                    if (dash_timer >= dash_time)
                    {
                        apply_speed = run_speed;
                    }
                    else
                    {
                        apply_speed = dash_speed;
                    }
                }
            }
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            dash_timer = 0;
            is_dash = false;
            apply_speed = run_speed;
        }
    }
}

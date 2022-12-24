using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapping : MonoBehaviour
{
    public LineRenderer line;
    public Transform hook;

    public float hook_speed;
    public float hook_distence;

    bool is_hook_key_down;
    bool is_line_max;
    public bool is_attach;

    Vector2 mouse_direction;
    player p;
    void Start()
    {
        p = GetComponent<player>();
        //라인 그리기
        line.positionCount = 2; //그려질 라인 포인트 개수
        line.endWidth = line.startWidth = 0.05f; // 그려질 가로 길이
        line.SetPosition(0, transform.position); //index 포인트, 위치
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true; // 월드좌표로 한다는 뜻
        is_attach = false;
    }


    void Update()
    {
        //위치를 계속 업데이트 해줘야 라인이 따라가져 보인다.
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        if (Input.GetMouseButtonDown(1) && !is_hook_key_down) // 마우스 오른쪽을 누르고 훅키를 안눌렀을때
        {
            hook.position = transform.position; // 누를시 처음위치
            // 화면(스크린)월드 좌표에 마우스 위치에 케릭터 위치를 빼면 = 마우스의 방향
            mouse_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            is_hook_key_down = true;   //키를 눌렀다 =true
            is_line_max = false;       //아직 거리가 짧으니까
            hook.gameObject.SetActive(true); // 활성화
        }

        if (is_hook_key_down && !is_line_max && !is_attach)// 눌렀고 false 이고 안붙었을때
        {
            //translate를 이용해서  (날아가는 방향.정규화 시키고 * 시간 * 스피드(힘))
            hook.Translate(mouse_direction.normalized * Time.deltaTime * hook_speed);

            if (Vector2.Distance(transform.position, hook.position) > hook_distence)// 후크의 거리가 hook_distence보다 클시
            {
                is_line_max = true;
            }
        }
        else if (is_hook_key_down && is_line_max && !is_attach)// 눌렀고 true 안붙었을때
        {
            //movetowards는 타겟으로 가는 함수이다.
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * hook_speed);
            if (Vector2.Distance(transform.position, hook.position) < 0.1f)//플레이어와의 거리가 0.1보다 작다면
            {
                is_hook_key_down = false;
                is_line_max = false;
                hook.gameObject.SetActive(false); // 비활성화

            }
        }
        else if (is_attach) //붙을때
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) // 붙은상태에서 다시 마우스 오른쪽을 누르면 또는 붙은상태에서 다시 점프키를 누르면
            {
                is_attach = false;
                is_hook_key_down = false;
                is_line_max = false;
                hook.GetComponent<hooking>().joint2D.enabled = false;
                hook.gameObject.SetActive(false);
            }
            if (Vector2.Distance(transform.position, hook.position) > hook_distence) // 붙은 상태에서 위아래로 움직일때 제한 길이 및 상태
            {
                p.is_hook_range_max = true;
                transform.position = Vector2.MoveTowards(transform.position, hook.position, Time.deltaTime * 2); // 제한을 넘으면 조금 되돌려서 true값 false로 되돌리기.
            }
            else
            {
                p.is_hook_range_max = false;
            }
        }
    }
}
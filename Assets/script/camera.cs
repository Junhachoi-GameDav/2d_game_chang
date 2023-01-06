using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target; //플레이어
    public float speed; //카메라 속도

    Transform cam_limit; // 카메라 화면 제한 범위

    public Transform[] limits;

    float height;
    float width;

    void Start()
    {
        // 화면의 정가운데를 기준으로 한다.
        height = Camera.main.orthographicSize; //카메라 직각 투시 모드(주로 2d에서 사용함)
        width = height * Screen.width / Screen.height; //(총 가로길이에서 높이 만큼 나누면 딱 절반 값)
        change_limit(0); //
    }

    public void change_limit(int x)
    {
        cam_limit = limits[x]; // 캠제한 스크린 갯수
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        //가로
        float lx = cam_limit.localScale.x * 0.5f - width; //캠제한 스크린의 절반에 위에 가로(절반) 값을 뺀다. 
        float clamp_x = Mathf.Clamp(transform.position.x, -lx + cam_limit.position.x, lx + cam_limit.position.x); // -lx 는 최소값 lx 는 최대값
        //세로
        float ly = cam_limit.localScale.y * 0.5f - height;//캠제한 스크린의 절반에 위에 세로(절반) 값을 뺀다.
        float clamp_y = Mathf.Clamp(transform.position.y, -ly + cam_limit.position.y, ly + cam_limit.position.y);// -ly 는 최소값 ly 는 최대값

        transform.position = new Vector3(clamp_x, clamp_y, -10f); //z값의 -10은 기본 카메라 위치 값이다.
    }
}

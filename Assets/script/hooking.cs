using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hooking : MonoBehaviour
{
    grapping grap;
    public DistanceJoint2D joint2D;
    void Start()
    {
        //find 함수로 플레이어(이름)안에있는 컴포넌트에 접근한다.
        grap = GameObject.Find("player").GetComponent<grapping>();
        //고리에 있는 조인트를 활성화
        joint2D = GetComponent<DistanceJoint2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ring")) //tag가 링일떄
        {
            joint2D.enabled = true; // 활성화
            grap.is_attach = true;
            grap.hook_ef.SetActive(false);
        }
        
    }
}

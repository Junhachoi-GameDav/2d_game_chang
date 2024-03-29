using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hooking : MonoBehaviour
{
    grapping grap;
    public DistanceJoint2D joint2D;
    SpriteRenderer sprite;
    void Start()
    {
        //find 함수로 플레이어(이름)안에있는 컴포넌트에 접근한다.
        grap = GameObject.Find("player").GetComponent<grapping>();
        //고리에 있는 조인트를 활성화
        joint2D = GetComponent<DistanceJoint2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ring")) //tag가 링일떄
        {
            joint2D.enabled = true; // 활성화
            grap.is_attach = true;
            sprite.color = new Color(0.25f, 0.57f, 0.48f, 1);
            grap.hook_ef.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ring")) //tag가 링일떄
        {
            sprite.color = new Color(1, 1, 1, 0);
        }
    }
}

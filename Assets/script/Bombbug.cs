using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombbug : Enermy
{
    Animator animator;
    bool isFind=false;
    // Start is called before the first frame update
    private void Awake()
    {
       animator = GetComponent<Animator>();
            op = Random.Range(0, 3);
        home = transform.position;//물체의 위치
        Physics2D.IgnoreLayerCollision(3, 11);//플레이어와의 충돌 무시
    }

    // Update is called once per frame
    private void Update()
    {
        if (isFollow == false && isEnd == false)//쫓아가고 있지 않을때만 이런 동작허용한다.
        {
            if (op == 0)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                isLeft = -1;
                animator.SetFloat("Isleft",isLeft);
                animator.SetBool("Walk", true);
            }
            else if (op == 1)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                isLeft = 1;
                animator.SetFloat("Isleft", isLeft);
                animator.SetBool("Walk", true);
            }
            else
            {
                transform.Translate(Vector2.zero); 
                animator.SetFloat("Isleft", isLeft);//전에 방향으로 머리를 향함
                animator.SetBool("Walk", false);
            }

            if (isDelay == false)
            {
                isDelay = true;
                StartCoroutine(Move());
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (isEnd == false)
        {

            RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * isLeft, distance, isLayer);//플레이어와만 충돌할수 있다
            Debug.DrawRay(transform.position, Vector2.right * isLeft * distance, new Color(0, 1, 0)); //듀레이션 없애야 계속 ray가 안 생긴다.
            if (raycast.collider != null)//플레이어와 충돌시에 
            {
                //  Debug.Log("isfollow");
                if (isFind == false)
                {
                    isFollow = true;
                    animator.SetBool("Find", isFollow);//true
                    StartCoroutine(Find());

                }
                else if (isFind == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, raycast.collider.transform.position, Time.deltaTime * speed * 3f);
                    if (Vector2.Distance(transform.position, raycast.collider.transform.position) <= 1f)
                    {
                        //몸통박치기
                        Attack();
                    }                                     
                }
            }
            else
            {
                isFollow = false;
                isFind = false;
                animator.SetBool("Find", isFind);//false
                animator.ResetTrigger("Run");//다시 리셋
                box.SetActive(false);
            }
        }
        else
        {
            isFollow = false;
            isFind = false;
            animator.SetBool("Find", isFollow);
            animator.ResetTrigger("Run");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Endpoint"&&isEnd==false)
        {
           // Debug.Log("collision");
            isEnd = true;
            animator.SetBool("Walk", false);//벽에 부딫치면은 안움직이기에 walk모션을 취하지 않는다.
            if (isLeft == -1)
            {           
                isLeft = 1;              
            }
            else
            {               
                isLeft = -1;
            }
            transform.position = Vector2.MoveTowards(transform.position, home, Time.deltaTime * speed * 1.4f);
            StartCoroutine(Endpoint());
        }
    }
    public Vector2 boxSize;
    public Transform boxpos;
    public Transform direct;
    public GameObject box;
    public void Attack()
    {
        box.SetActive(true);
        Debug.Log("yes");
        if (isLeft == -1)
        {
            //direct.localScale = new Vector3(direct.localScale.x, direct.localScale.y, direct.localScale.z);
            if (boxpos.localPosition.x > 0)//부모와의 거리가 양수일때 음수가 정상 왼쪽
            {
                boxpos.localPosition = new Vector2(boxpos.localPosition.x*-1, boxpos.localPosition.y);//음수로 만든다
                //axepos.localPosition = new Vector2(axepos.localPosition.x * -1, axepos.localPosition.y);
            }
            

        }
        else
        {
           // direct.localScale = new Vector3(direct.localScale.x*-1, direct.localScale.y, direct.localScale.z);
            
            if (boxpos.localPosition.x < 0)//부모와의 거리가 음수일때 양수가 정상 오른쪽
            {
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x), boxpos.localPosition.y);//절대값으로 양수로 만든다.
                //axepos.localPosition = new Vector2(Mathf.Abs(axepos.localPosition.x), axepos.localPosition.y);
            }

        }
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        //박스의 위치와 박스의 크기에 그리고 회전값을 넣는다
        foreach (Collider2D colider in collider2Ds)
        {
           // Debug.Log("충돌");
            if (colider.tag == "Player")//콜라이더의 테그를 비교해서 플레이어면은 넣어놓는다
            {
                Debug.Log("player damage");
                colider.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f*isLeft,10f));
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxpos.position, new Vector2(1f, 1f));
    }

    private IEnumerator Endpoint()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector2.MoveTowards(transform.position, home, Time.deltaTime * speed * 3f);
        yield return new WaitForSeconds(0.5f);
        isEnd = false;
    }
    private IEnumerator Move()
    {
        yield return new WaitForSeconds(0.8f);
        op = Random.Range(0, 3);
        isDelay = false;
    }

    private IEnumerator Find()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Run");
        isFind = true;
    }
}

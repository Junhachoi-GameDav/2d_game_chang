using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug :Enermy
{
    // Start is called before the first frame update
    Animator animator;
    public Transform ray;
    int Hp = 5;
    Transform target;
    [Header("�����Ÿ�")]
    [SerializeField] [Range(0f, 3f)] float contactDistance = 1f;
    [Header("�νĺҰ��Ÿ�")]
    [SerializeField] [Range(0f, 6f)] float dontcatch = 5f;
    private void Awake()
    {
        op = Random.Range(0, 3);
        animator = GetComponent<Animator>();
        home = transform.position;//��ü�� ��ġ
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollow == false && isEnd == false && isDamage == false && isDie == false)//�Ѿư��� ���� �������� �̷� ��������Ѵ�.
        {
            if (op == 0)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                isLeft = -1;
                animator.SetFloat("Isleft", isLeft);
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
                animator.SetFloat("Isleft", isLeft);//���� �������� �Ӹ��� ����
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
        FollowTarget();
        backhome();
    }
    void backhome()
    {
        if(Vector2.Distance(home,transform.position)>10f)
        {
            isEnd = true;
            isFollow = false;
        }

        if(Vector2.Distance(home, transform.position) >= 0.5f && isEnd ==true&&isFollow==false)
        {
            StartCoroutine(goHome());
        }
        else if(isFollow==true)
        {
            isEnd=false;
        }
        else if(Vector2.Distance(home, transform.position) <= 0.5f && isEnd == true)
        {
            Debug.Log("Move");
            isEnd = false;
        }
    }
    void FollowTarget()
    {
        if (isEnd == false&&isFollow==true)
        {
            if (Vector2.Distance(transform.position, target.position) > contactDistance && isFollow == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                Debug.Log("follow");
            }
            else if (Vector2.Distance(transform.position, target.position) < dontcatch && isFollow == true)
            {
                transform.Translate(Vector2.zero);
            }
        }
    }

    public void TakeDamage(int damage, int h)
    {
        
        Hp = h - damage;
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
       // return h;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weapon")
        {

            Debug.Log("damage");
            TakeDamage(5, Hp);
            isDamage = true;
            Debug.Log("isnot move");
            Invoke("damage", 0.4f);
        }

        if (collision.CompareTag("Player"))
        {
            isFollow = true;
        }
    }


    private IEnumerator Move()
    {
        yield return new WaitForSeconds(0.8f);
        op = Random.Range(0, 3);
        isDelay = false;
    }

    private IEnumerator goHome()
    {
        yield return new WaitForSeconds(1.5f);
        transform.position = Vector2.MoveTowards(transform.position, home, speed * 5f * Time.deltaTime);
        /*yield return new WaitForSeconds(0.8f);
        isFollow = false;
        Debug.Log("no");
        yield return new WaitForSeconds(0.8f);
        isEnd = false;*/
    }
}
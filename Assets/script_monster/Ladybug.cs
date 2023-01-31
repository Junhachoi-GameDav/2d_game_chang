using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug :Enermy
{
    

    // float player_isleft;
    // Start is called before the first frame update
    int weapon_damage;
    private float roll = 1;
    GameObject effect;
    Animator animator;
    public Transform ray;
    public int Hp = 20;
    Transform target;
    [Header("근접거리")]
    [SerializeField] [Range(0f, 3f)] float contactDistance = 1f;
    [Header("인식불가거리")]
    [SerializeField] [Range(0f, 6f)] float dontcatch = 5f;
    private void Awake()
    {
        op = Random.Range(0, 3);
        animator = GetComponent<Animator>();
        home = transform.position;//물체의 위치
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GameObject weapon = Instantiate(prefab_weapon);
        weapon_damage = weapon.GetComponent<granade>().granade_dmg;
        effect = weapon.GetComponent<granade>().granade_effect;
        //player_isleft= GameObject.FindGameObjectWithTag("Player").GetComponent<player>().;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (isFollow == false && isEnd == false && isDamage == false && isDie == false&&isDamage==false&&isAttack==false)//쫓아가고 있지 않을때만 이런 동작허용한다.
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
                animator.SetFloat("Isleft", isLeft);//전에 방향으로 머리를 향함
                animator.SetBool("Walk", false);
            }

            if (isDelay == false)
            {
                isDelay = true;
                StartCoroutine(Move());
            }
        }
      //  Debug.Log(isLeft);
    }

    
    private void FixedUpdate()
    {
        if (isDie == false&&isAttack==false)
        {
            FollowTarget();
            backhome();
        }
        if(isDie == true)
        {
            DirectionEnemy(target.position.x, transform.position.x);
        }
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
        if (isEnd == false&&isFollow==true&&isDamage==false)
        {
            DirectionEnemy(target.position.x, transform.position.x);
            if (Vector2.Distance(transform.position, target.position) > contactDistance && isFollow == true)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
               // Debug.Log("follow");
                //if () ;//플레이어가왼쪽에 있는지 오른쪽에 있는지를 알아야 한다.
            }
            else if (Vector2.Distance(transform.position, target.position) < contactDistance && isFollow == true&&isDie==false)
            {
                //if(isDamage==false)
               Invoke("Attack",0.3f);
            
            }

            if(isEnd == true)
            {
                speed += 1.5f;              
            }
           // Debug.Log(isLeft);
           // box.SetActive(false);
        }
    }


    public void DirectionEnemy(float target, float basobj)
    {
        if (target < basobj)
        {
            animator.SetFloat("Isleft", -1);//방향의 설정
            isLeft = -1;
        }
        else
        {
            animator.SetFloat("Isleft", 1);
            isLeft = 1;
        }
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        isDie = true;
        
        Debug.Log("isroll");
        Debug.Log(roll);
        //isDamage = true;//적 못움직이게
        yield return new WaitForSeconds(4f);
        //폭발하도록하기
        
        //Explosion();
        //yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    float stoptime = 1f;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDie == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                {
                    transform.rotation = Quaternion.Euler(0, 0, roll);
                    //Debug.Log(roll);
                    if (isLeft == -1)
                    {
                        roll -= Time.deltaTime * 120;
                    }
                    else
                    {
                        roll += Time.deltaTime * 120;
                    }
                    stoptime = 1f;//원래 멈추는 시간
                }
            }
            else//플레이어와 부딫치지 않고 있을경우
            {
                stoptime -= Time.deltaTime*1.25f;
                if (stoptime <= 0)
                {
                    Debug.Log(stoptime);
                }
                else if(stoptime >0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, roll);
                    if (isLeft == -1)
                    {
                        roll -= Time.deltaTime * 100*stoptime;
                    }
                    else
                    {
                        roll += Time.deltaTime * 100*stoptime;
                    }
                }
            }
        }
    }
   

    public void TakeDamage(int damage, int h)
    {
        
        Hp = h - damage;
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            StartCoroutine(Die());
        }
       // return h;
    }

    void attacked()
    {
        animator.SetBool("Attacked", false);
    }

    IEnumerator Attacked_weapon(GameObject collision)
    {
        iseffect = true;
        Transform d = collision.transform;
        Destroy(collision);
        GameObject eff = Instantiate(effect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Destroy(eff);
        yield return new WaitForSeconds(0.1f);
        iseffect = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weapon" && isDie == false)
        {
            if (collision.tag == "weapon")
                StartCoroutine(Attacked_weapon(collision.gameObject));
            Debug.Log("weapondamage");
            TakeDamage(weapon_damage, Hp);
            isDamage = true;
            Debug.Log("isnot move");
            animator.SetBool("Attacked", true);
            Debug.Log(isDamage);
            Invoke("attacked", 0.15f);
            Invoke("damage", 0.15f);

        }
        else if (collision.tag == "effect" && iseffect == false && isDie == false)
        {
            Debug.Log("effectdamage");
            TakeDamage(weapon_damage, Hp);
            isDamage = true;
            Debug.Log("isnot move");
            animator.SetBool("Attacked", true);
            Debug.Log(isDamage);
            Invoke("attacked", 0.15f);
            Invoke("damage", 0.15f);
            Debug.Log(isDamage);

        }

        if (collision.CompareTag("Player"))
        {
            isFollow = true;
        }
    }

    public Vector2 boxSize;
    public Transform boxpos;
    public GameObject explosion;
    public GameObject box;
    public GameObject melee;

    public void Attack()
    {
        box.SetActive(true);
        animator.SetBool("Attack",true);
        isAttack = true;
       
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        //박스의 위치와 박스의 크기에 그리고 회전값을 넣는다
        foreach (Collider2D colider in collider2Ds)
        {
            // Debug.Log("충돌");
            if (colider.tag == "Player")//콜라이더의 테그를 비교해서 플레이어면은 넣어놓는다
            {
                Debug.Log("player damage");
                damage_manager.Instance.damage_count(1);
                melee.SetActive(true);
                colider.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f * isLeft, 10f));
            }
        }
        StartCoroutine(attack());
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.4f);
        isAttack = false;
        box.SetActive(false);
        melee.SetActive(false);
        animator.SetBool("Attack", false);
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
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxpos.position, new Vector2(1f, 1f));
    }

}



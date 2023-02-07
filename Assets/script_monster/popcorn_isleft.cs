using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popcorn_isleft : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;
    float isLeft;
    public GameObject islefts;
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        isLeft = islefts.GetComponent<Popcornbug>().isLeft;
        animator.SetFloat("Isleft",isLeft);
       // Debug.Log(isLeft);
    }
}

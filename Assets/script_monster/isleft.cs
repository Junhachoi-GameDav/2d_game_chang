using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isleft : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;
    float isLeft;
    public GameObject islefts;
    private void Awake()
    {
        isLeft = islefts.GetComponent<Popcornbug>().isLeft;
        animator = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        
        animator.SetFloat("Isleft",isLeft);
        Debug.Log(isLeft);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_head :MonoBehaviour
    { 
    public Transform home;
    public GameObject ob;
    float move_time = 0.2f;
    float isleft;
    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = home.position;
        isleft = ob.GetComponent<Bombbug>().isLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (move_time > 0)
        {
            if (isleft == -1)
            {
                transform.Translate(new Vector2(-1, 0) * 3f * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector2(1, 0) * 3f * Time.deltaTime);
            }
            Invoke("movehome", 0.02f);
           
            // move_time -= Time.deltaTime;

        }
        
    }
    public void movehome()
    {
        transform.position = home.position;
    }
}

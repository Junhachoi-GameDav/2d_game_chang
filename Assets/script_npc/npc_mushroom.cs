using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_mushroom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("in to_npc");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("out to_npc");
        }
    }
}

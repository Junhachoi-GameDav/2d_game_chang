using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_mushroom : MonoBehaviour
{
    dialogue_controller controller;
    private void Start()
    {
        controller = FindObjectOfType<dialogue_controller>();
    }
    private void Update()
    {
        if (controller.mushroom && Input.GetKeyDown(KeyCode.F))
        {
            controller.is_talk = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("in to_npc");
            controller.mushroom = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("out to_npc");
            controller.mushroom = false;
        }
    }
}


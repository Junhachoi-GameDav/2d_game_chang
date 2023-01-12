using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    public int next_map_num;

    bool is_tele;
    public Transform GetDestination()
    {
        return destination;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && is_tele)
        {
            Camera.main.GetComponent<camera>().change_limit(next_map_num);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            is_tele = true;
            Debug.Log(next_map_num);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            is_tele = false;
        }
    }
    
}

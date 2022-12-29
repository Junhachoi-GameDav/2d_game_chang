using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granade_destroy : MonoBehaviour
{
    public GameObject boom_position;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bottom")
        {
            Instantiate(boom_position, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}

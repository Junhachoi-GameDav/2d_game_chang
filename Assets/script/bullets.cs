using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets : MonoBehaviour
{
    public int granade_dmg;
    public int bullet_dmg;

    public GameObject granade_effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bottom")
        {
            Destroy(gameObject);
            granade_effect.transform.position = transform.position;
            Instantiate(granade_effect, transform.position, transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_melee : MonoBehaviour
{
    public GameObject hit_effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            GameObject hit_ef = Instantiate(hit_effect, collision.transform.localPosition, collision.transform.localRotation);
            Destroy(hit_ef, 0.2f);
        }
    }
}

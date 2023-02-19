using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_melee : MonoBehaviour
{
    public GameObject hit_effect;

    player p;
    private void Start()
    {
        p = FindObjectOfType<player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            GameObject hit_ef = Instantiate(hit_effect, collision.transform.localPosition, collision.transform.localRotation);
            if (p.atk_num == 1)
            {
                game_manager.Instance.gm_ef_sound_mng("atk1_hit_sound");
            }
            else
            {
                game_manager.Instance.gm_ef_sound_mng("atk2_hit_sound");
            }
            Destroy(hit_ef, 0.2f);
        }
    }
}

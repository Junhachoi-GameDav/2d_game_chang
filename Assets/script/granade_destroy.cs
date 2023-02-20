using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granade_destroy : MonoBehaviour
{
    
    obj_manager obj_m;
    GameObject granade_effect;
    GameObject granade_partical_ef;
    GameObject granade_bottle_effect;
    GameObject excolusion;
    float ro_speed =350;
    
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * ro_speed, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bottom" || collision.gameObject.tag == "Monster")
        {
            //Instantiate(boom_position, transform.position, boom_position.transform.rotation);
            obj_m = FindObjectOfType<obj_manager>();

            excolusion = obj_m.make_obj("exclusion");
            granade_effect = obj_m.make_obj("grenades_ef");
            granade_partical_ef = obj_m.make_obj("grenades_partical");
            granade_bottle_effect = obj_m.make_obj("grenades_bottle");
            game_manager.Instance.gm_ef_sound_mng("grenade_sound");

            excolusion.transform.position = gameObject.transform.position;
            granade_effect.transform.position = gameObject.transform.position;
            granade_partical_ef.transform.position = gameObject.transform.position;
            granade_bottle_effect.transform.position = gameObject.transform.position;

            Invoke("granade_bottle_effect_destroy", 0.18f);
            Invoke("granade_effect_destroy", 0.34f);
            Invoke("granade_partical_ef_destroy", 1.34f);
            gameObject.SetActive(false);
        }
    }
    void granade_effect_destroy()
    {
        granade_effect.SetActive(false);
    }
    void granade_bottle_effect_destroy()
    {
        granade_effect.SetActive(false);
    }
    void granade_partical_ef_destroy()
    {
        granade_effect.SetActive(false);
    }
}

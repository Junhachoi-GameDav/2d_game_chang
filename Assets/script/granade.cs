using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granade : MonoBehaviour
{
    public int granade_dmg;

    obj_manager obj_m;
    GameObject granade_effect;
    GameObject granade_partical_ef;
    GameObject granade_bottle_effect;

    private void Start()
    {
        //g_sys();
    }
    
    void g_sys()
    {
        obj_m = FindObjectOfType<obj_manager>();
        granade_effect = obj_m.make_obj("grenades_ef");
        granade_partical_ef = obj_m.make_obj("grenades_partical");
        granade_bottle_effect = obj_m.make_obj("grenades_bottle");

        //granade_effect.transform.position = gameObject.transform.position;
        //granade_partical_ef.transform.position = gameObject.transform.position;
        //granade_bottle_effect.transform.position = gameObject.transform.position;

        Invoke("granade_bottle_effect_destroy", 0.18f);
        Invoke("granade_effect_destroy", 0.34f);
        Invoke("granade_partical_ef_destroy", 1.34f);
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

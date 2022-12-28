using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade_effect : MonoBehaviour
{
    
    void Awake()
    {
        Invoke("effect_destroy", 0.2f);
    }

    
    void effect_destroy()
    {
        Destroy(gameObject);
    }
}

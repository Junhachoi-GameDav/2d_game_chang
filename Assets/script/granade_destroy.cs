using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granade_destroy : MonoBehaviour
{
    public GameObject boom_position;
    float ro_speed =350;
    
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * ro_speed, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bottom")
        {
            Instantiate(boom_position, transform.position, boom_position.transform.rotation);
            game_manager.Instance.gm_ef_sound_mng("grenade_sound");
            Destroy(gameObject);
        }
    }

}

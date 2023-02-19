using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_battle : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            game_manager.Instance.gm_bg_sound_mng("none_sound");
            gameObject.SetActive(false);
        }
    }
}

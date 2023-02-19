using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_manager : MonoBehaviour
{
    public AudioClip main_menu_bgm;
    public AudioClip story1_bgm;

    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

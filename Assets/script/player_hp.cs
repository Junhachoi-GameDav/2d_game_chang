using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_hp : MonoBehaviour
{
    public Sprite[] sprites;
    public Transform[] ef_pos;
    public GameObject hp_ef;
    Image cur_img; //현재 체력 이미지
    player p;
    void Start()
    {
        cur_img = GetComponent<Image>();
        p = FindObjectOfType<player>();
    }
    private void Update()
    {
        change_img(p.player_hp);
    }
    void change_img(int num)
    {
        cur_img.sprite = sprites[num-1];
        switch (num)
        {
            case 9:
            case 8:
                Instantiate(hp_ef, ef_pos[0].position, ef_pos[0].rotation);
                break;
            case 7:
            case 6:
                Instantiate(hp_ef, ef_pos[1].position, ef_pos[1].rotation);
                break;
            case 5:
            case 4:
                Instantiate(hp_ef, ef_pos[2].position, ef_pos[2].rotation);
                break;
            case 3:
            case 2:
                Instantiate(hp_ef, ef_pos[3].position, ef_pos[3].rotation);
                break;
            case 1:
            case 0:
                Instantiate(hp_ef, ef_pos[4].position, ef_pos[4].rotation);
                break;
        }
    }
}

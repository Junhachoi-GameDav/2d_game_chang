using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_hp : MonoBehaviour
{
    public Sprite[] sprites;
    public Transform[] ef_pos;
    public GameObject hp_ef;

    int count =9;


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
        cur_img.sprite = sprites[num];
        hp_efft();
    }
    void hp_efft()
    {
        switch (p.player_hp)
        {
            case 9:
                if(count == 9)
                {
                    Instantiate(hp_ef, ef_pos[0].position, ef_pos[0].rotation);
                    count--;
                }
                break;
            case 8:
                if (count == 8)
                {
                    Instantiate(hp_ef, ef_pos[0].position, ef_pos[0].rotation);
                    count--;
                }
                break;
            case 7:
                if (count == 7)
                {
                    Instantiate(hp_ef, ef_pos[1].position, ef_pos[1].rotation);
                    count--;
                }
                break;
            case 6:
                if (count == 6)
                {
                    Instantiate(hp_ef, ef_pos[1].position, ef_pos[1].rotation);
                    count--;
                }
                break;
            case 5:
                if (count == 5)
                {
                    Instantiate(hp_ef, ef_pos[2].position, ef_pos[2].rotation);
                    count--;
                }
                break;
            case 4:
                if (count == 4)
                {
                    Instantiate(hp_ef, ef_pos[2].position, ef_pos[2].rotation);
                    count--;
                }
                break;
            case 3:
                if (count == 3)
                {
                    Instantiate(hp_ef, ef_pos[3].position, ef_pos[3].rotation);
                    count--;
                }
                break;
            case 2:
                if (count == 2)
                {
                    Instantiate(hp_ef, ef_pos[3].position, ef_pos[3].rotation);
                    count--;
                }
                break;
            case 1:
                if (count == 1)
                {
                    Instantiate(hp_ef, ef_pos[4].position, ef_pos[4].rotation);
                    count--;
                }
                break;
            case 0:
                if (count == 0)
                {
                    Instantiate(hp_ef, ef_pos[4].position, ef_pos[4].rotation);
                    count--;
                }
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class menu_manager : MonoBehaviour
{
    public GameObject ingame_menu;

    public GameObject[] in_button_menu;

    public bool is_menu_show;


    void Update()
    {
        press_esc();
        if (is_menu_show)
        {
            ingame_menu.SetActive(true);
            Time.timeScale = 0f; //Ω√∞£ ∏ÿ√„
        }
        else
        {
            ingame_menu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    void press_esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            is_menu_show = !is_menu_show;
        }
    }

    public void ingame_quit()
    {
        SceneManager.LoadScene(0);
    }

    public void info()
    {
        check_ingame_menu(0);
    }
    public void inventory()
    {
        check_ingame_menu(1);
    }
    public void setting()
    {
        check_ingame_menu(2);
    }
    public void save_and_quit()
    {
        check_ingame_menu(3);
    }


    void check_ingame_menu(int num)
    {
        for (int i = 0; i < in_button_menu.Length; i++)
        {
            in_button_menu[i].SetActive(false);
            in_button_menu[num].SetActive(true);
        }
    }
}

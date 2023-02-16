using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu_manager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void new_game()
    {
        game_manager.Instance.scene_load("story1");
    }
    public void load_game()
    {
        // 저장된 씬을 불러옴
    }
    public void option()
    {
        // 게임 매니져에 있는 옵션을 불러올것임.
    }
    public void quit_game()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static game_manager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static game_manager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    public void new_game()
    {
        SceneManager.LoadScene(1);
    }
    public void load_game()
    {

    }
    public void option()
    {

    }
    public void quit_game()
    {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screen_option : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutions_dropdown;
    public Toggle full_screen_b;
    List<Resolution> resolutions = new List<Resolution>();

    public int resoultion_num;

    
    private void Start()
    {
        ui_form();
    }
    void ui_form()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60) //60헤르츠만 넣음
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        //resolutions.AddRange(Screen.resolutions);
        resolutions_dropdown.options.Clear(); //현재 있는 드랍다운의 리스트(옵션)들을 제거

        int option_num = 0;

        foreach(Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(); // 객체를 생성
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            resolutions_dropdown.options.Add(option);

            if(item.width == Screen.width && item.height == Screen.height)
            {
                resolutions_dropdown.value = option_num;
            }
            option_num++;
        }
        resolutions_dropdown.RefreshShownValue(); //보기 새로고침 함수.

        full_screen_b.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;

    }

    public void dropboxoption_change(int x)
    {
        resoultion_num = x;
    }

    public void full_screen_btn(bool is_full)
    {
        screenMode = is_full ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed; 
    }

    public void screen_ok_btn()
    {
        //screenMode = is_full ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        // 가로, 높히, 전체화면, 헤르츠
        Screen.SetResolution(resolutions[resoultion_num].width, resolutions[resoultion_num].height, screenMode);

    }

}

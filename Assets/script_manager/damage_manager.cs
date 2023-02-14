using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 자료 출처 https://glikmakesworld.tistory.com/2
// 싱글톤으로 만들되, MonoBehaviour를 상속 받지 않아 씬이 넘어가도 존재할수 있게 만든다.(대신 화면상에 나오지 않는다.)
public class damage_manager
{
    private static damage_manager instance;

    public static damage_manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new damage_manager();
            }
            return instance;
        }
    }
    //생성
    public damage_manager()
    {
        
    }
    public int en_dmg = 1; //적 공격력을 담을 정수(ui 때문에 정수로 한다.)
    public void damage_count(int enemy_damage)
    {
        en_dmg = enemy_damage;
    }
}

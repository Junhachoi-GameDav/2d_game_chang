using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class load_scene_mng : MonoBehaviour
{
    public Slider progress_bar;
    public Text loading_text;
    public Text loading_done_text;

    private void Start()
    {
        loading_text.gameObject.SetActive(true);
        loading_done_text.gameObject.SetActive(false);
        StartCoroutine(load_scene_co_ro());
    }
    IEnumerator load_scene_co_ro()
    {
        yield return null;
        //asy~ 의 속성을 사용할것임 _ 로드신ㅁasync 는 씬을 넘어가는 중에 멈추지 않고 동작을 실행할수 있음
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");
        operation.allowSceneActivation = false; // 마지막(90%)에 멈춤


        while (!operation.isDone)//씬이 불러와지면 투루 = 씬을 불러오기 전까지
        {
            yield return null;
            if (progress_bar.value < 0.9f)
            {
                progress_bar.value = Mathf.MoveTowards(progress_bar.value, 0.9f, operation.progress);
            }
            else if (operation.progress >= 0.9f)
            {
                progress_bar.value = Mathf.MoveTowards(progress_bar.value, 1f, operation.progress);
            }


            if (progress_bar.value >= 1f)
            {
                loading_text.gameObject.SetActive(false);
                loading_done_text.gameObject.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0) && progress_bar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}

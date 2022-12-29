using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granade : MonoBehaviour
{
    public int granade_dmg;

    public GameObject granade_effect;
    public GameObject granade_partical_ef;
    public GameObject granade_bottle_effect;

    private void Start()
    {
        StartCoroutine(granade_ef_co_ru());
    }
    IEnumerator granade_ef_co_ru()
    {
        //위치
        granade_effect.transform.position = transform.position;
        granade_partical_ef.transform.position = transform.position;
        granade_bottle_effect.transform.position = transform.position;

        //활성화
        GameObject g_ef = Instantiate(granade_effect, transform.position, transform.rotation);
        GameObject g_part = Instantiate(granade_partical_ef, transform.position, transform.rotation);
        GameObject g_b_ef =  Instantiate(granade_bottle_effect, transform.position, transform.rotation);


        
        yield return new WaitForSeconds(0.2f);//0.2초후 비활성화
        Destroy(g_b_ef);
        yield return new WaitForSeconds(0.16f);//0.36초후 비활성화
        Destroy(g_ef);
        yield return new WaitForSeconds(1.14f);//1.5초후 비활성화
        Destroy(g_part);
        Destroy(gameObject);
    }

}

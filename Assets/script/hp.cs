using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp : MonoBehaviour
{
    void Start()
    {
        Invoke("this_destory", 1f);
    }

    void this_destory()
    {
        Destroy(this.gameObject);
    }
}

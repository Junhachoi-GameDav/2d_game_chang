using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_rotation : MonoBehaviour
{
    public float ro_speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * ro_speed, Space.Self);
    }
}

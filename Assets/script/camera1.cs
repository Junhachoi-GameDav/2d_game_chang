using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera1 : MonoBehaviour
{
    public Transform cam;
    private void FixedUpdate()
    {
        this.transform.position = cam.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    public int next_map_num;

    public Transform GetDestination()
    {
        return destination;
    }
}

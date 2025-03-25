using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class TrapDoorway : MonoBehaviour
{
    private BoxCollider[] numberOfDoors;
    private Random rnd;

    // Start is called before the first frame update
    void Start()
    {
        numberOfDoors = GetComponentsInChildren<BoxCollider>();
        rnd = new Random();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
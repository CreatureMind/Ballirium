using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorway : MonoBehaviour
{
    private GameObject[] numberOfDoors;
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfDoors = GetComponentsInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
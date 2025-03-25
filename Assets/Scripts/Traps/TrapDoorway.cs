using UnityEngine;
using Random = System.Random;

public class TrapDoorway : MonoBehaviour
{
    private BoxCollider[] _numberOfDoors;
    private Random rnd = new Random();

    void Start()
    {
        _numberOfDoors = GetComponentsInChildren<BoxCollider>();
        int num = rnd.Next(0, _numberOfDoors.Length);
        _numberOfDoors[num].enabled = false;
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpinning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int speed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
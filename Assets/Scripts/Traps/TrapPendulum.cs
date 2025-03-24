using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;
using Math = System.Math;

public class TrapPendulum : MonoBehaviour
{
    [SerializeField] private float speed = 80f;
    [SerializeField] private float timer = 2.3f;
    private float dir = -1f;
    private Coroutine pendulumCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (pendulumCoroutine == null)
        {
            pendulumCoroutine = StartCoroutine(RotatePendulum());
        }
    }

    private IEnumerator RotatePendulum()
    {
        for (float i = 0; i < timer; i += Time.deltaTime)
        {
            transform.Rotate(Time.deltaTime * speed * dir, 0, 0);
            yield return null;
        }

        dir *= -1;
        pendulumCoroutine = null;
    }
}
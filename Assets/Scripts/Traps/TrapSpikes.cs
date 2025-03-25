using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class TrapSpikes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 80f;
    [SerializeField] private float height = 5f;
    
    private float waitFor = 2.3f;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool dir = true;
    private Coroutine spikesCoroutine;


    void Start()
    {
        startPos = transform.position;
        endPos = startPos;
        endPos.y += height;
    }

    // Update is called once per frame
    void Update()
    {
        if (spikesCoroutine == null)
        {
            spikesCoroutine = StartCoroutine(MoveSpikes());
        }
    }


    private IEnumerator MoveSpikes()
    {
        if (dir)
        {
            transform.position = endPos;
            dir = false;
            yield return new WaitForSeconds(waitFor);
        }
        else
        {
            if (Math.Abs(Vector3.Distance(transform.position, startPos)) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
            }
            else
            {
                dir = true;
                yield return new WaitForSeconds(waitFor);
            }
        }

        spikesCoroutine = null;
    }
}
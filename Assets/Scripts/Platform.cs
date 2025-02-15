using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// TODO remove
// public enum Direction
// {
//     Up,
//     Down,
//     North,
//     South,
//     East,
//     West
// }

public class Platform : MonoBehaviour
{
    public bool isReturn = true;
    public bool onRepeat;
    public bool movePlayer = true;
    public float startDelay;
    public float waitTime;
    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;

    private bool _CanMove = true;
    private GameObject _player = null;
    private bool _onPlatform;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        yield return new WaitForSeconds(startDelay);
        while (onRepeat)
        {
            while (transform.position != endPosition)
            {
                // _player.transform.position =
                    // Vector3.MoveTowards(_player.transform.position, endPosition, speed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);

                yield return 0;
            }

            if (isReturn)
            {
                yield return new WaitForSeconds(waitTime);
                while (transform.position != startPosition)
                {
                    // _player.transform.position =
                    //     Vector3.MoveTowards(_player.transform.position, endPosition, speed * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
                    yield return 0;
                }
            }

            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (_player == null)
        {
            _player = other.gameObject;
            
            // Debug.Log("Inside onCollision");
        }
    }
    
}
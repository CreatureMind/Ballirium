using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

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
    private Rigidbody _playerRigidbody = null;
    private Rigidbody _platformRigidbody = null;
    private bool _onPlatform;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        _platformRigidbody = gameObject.GetComponent<Rigidbody>();
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

    void FixedUpdate()
    {
        if (_onPlatform)
        {
            Debug.Log("In fixed update: " + _platformRigidbody.velocity + " | " + _playerRigidbody.velocity );
            // _playerRigidbody.velocity = _platformRigidbody.velocity. * _playerRigidbody.velocity;
        } // todo https://www.reddit.com/r/Unity3D/comments/14h5zkx/comment/jp9rkaa/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_player == null)
        {
            _onPlatform = true;
            _player = other.gameObject;
            _playerRigidbody = _player.GetComponent<Rigidbody>();
            Debug.Log("in OnCollisionEnter ver:  " +  _platformRigidbody.velocity);
            // Debug.Log("Inside onCollision");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _onPlatform = false;
    }
}
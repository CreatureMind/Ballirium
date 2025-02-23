using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Platform : MonoBehaviour
{
    public bool isReturn = true;
    public bool onRepeat;
    public bool movePlayer = true;
    public float startDelay;
    public float waitTime = 10f;
    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private Vector3 _target;


    private bool _CanMove = true;
    private bool _isPlatformMoving;
    private bool _returning;
    private GameObject _player = null;
    private Rigidbody _playerRigidbody = null;
    private Rigidbody _platformRigidbody = null;
    private bool _onPlatform;
    private Vector3 _LastPos;
    private Vector3 _CurrentPos;
    private bool _enabled;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        _target = endPosition;
        _platformRigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator WaitPlatform()
    {
        _enabled = true;
        Debug.Log("1inside wait platform");
        Debug.Log("1Target: " + _target);
        _returning = !_returning;
        _target = _target == startPosition ? endPosition : startPosition;
        yield return new WaitForSeconds(waitTime);
        _enabled = false;
        Debug.Log("2Target: " + _target);
        Debug.Log("2inside wait platform");
  
    }

    //TODO CLEAN CODE
    void FixedUpdate()
    {
        if (!_enabled)
        {
            if (Mathf.Abs((transform.position - _target).magnitude) > 0.1f)
            {
                _isPlatformMoving = true;
                Vector3 move = _target - transform.position;
                move.Normalize();
                _platformRigidbody.MovePosition(_platformRigidbody.position + move * (Time.deltaTime * speed));
            }
            else
            {
                Debug.Log("Inside else from start to end ");
                _isPlatformMoving = false;
                StartCoroutine(WaitPlatform());
            }
        }
        // if (onRepeat)
        // {
        //     if (!_returning)
        //     {
        //         Debug.Log(">>>>>>>>>>>>>>");
        //         if (Mathf.Abs((transform.position - _target).magnitude) > 1f)
        //         {
        //             _isPlatformMoving = true;
        //             Vector3 move = endPosition - transform.position;
        //             move.Normalize();
        //             _platformRigidbody.MovePosition(_platformRigidbody.position + move * (Time.deltaTime * speed));
        //         }
        //         else
        //         {
        //             Debug.Log("Inside else from start to end ");
        //             _isPlatformMoving = false;
        //             StartCoroutine(WaitPlatform());
        //         }
        //     }
        //     //
        //     // else
        //     // {
        //     //     Debug.Log("<<<<<<<<<<<<<<<<<<<<<<");
        //     //     if (isReturn)
        //     //     {
        //     //         Debug.Log("inside is return 71");
        //     //         if (Mathf.Abs((transform.position - startPosition).magnitude) > 1f)
        //     //         {
        //     //             Debug.Log("inside 74");
        //     //             _isPlatformMoving = true;
        //     //             Vector3 move = startPosition - transform.position;
        //     //             move.Normalize();
        //     //             _platformRigidbody.MovePosition(_platformRigidbody.position + move * (Time.deltaTime * speed));
        //     //         }
        //     //         else
        //     //         {
        //     //             _isPlatformMoving = false;
        //     //             StartCoroutine(WaitPlatform());
        //     //         }
        //     //     }
        //     // }
        // }


        if (_onPlatform)
        {
            if (_isPlatformMoving)
            {
                Vector3 vel = _platformRigidbody.position - _LastPos;
                Debug.Log("In fixed update:  vel   " + vel);
                Debug.Log("In fixed update:  _platformRigidbody.velocity   " + _platformRigidbody.velocity);
                Debug.Log("In fixed update:  _playerRigidbody.velocity   " + _playerRigidbody.velocity);
                _LastPos = _platformRigidbody.position;
                _playerRigidbody.AddForce(vel, ForceMode.Force);
            }


            // _player.transform.SetParent(transform);
            // Vector3 vel = _platformRigidbody.position - _LastPos;
            // _playerRigidbody.position = (_playerRigidbody.position + vel) * Time.deltaTime;
            // _LastPos = _platformRigidbody.position;
            // Debug.Log("In fixed update: " + _platformRigidbody.velocity + " | " + _playerRigidbody.velocity);
            // _playerRigidbody.velocity *= _platformRigidbody.velocity.magnitude ;
        } // todo https://www.reddit.com/r/Unity3D/comments/14h5zkx/comment/jp9rkaa/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button

        // if (_platformRigidbody != null && _playerRigidbody != null)
        // Debug.Log("In fixed update: " + _platformRigidbody.velocity + " | " + _playerRigidbody.velocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_player == null)
        {
            _LastPos = new Vector3(0, 0, 0);
            _onPlatform = true;
            _player = other.collider.gameObject;
            _playerRigidbody = _player.GetComponent<Rigidbody>();
            Debug.Log("in OnCollisionEnter vel:  " + _playerRigidbody.velocity);
            // Debug.Log("Inside onCollision");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _onPlatform = false;
    }
}
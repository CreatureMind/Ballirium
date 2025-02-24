using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float waitTime = 10f;
    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private Vector3 _target;


    private bool _CanMove = true;
    private bool _isPlatformMoving;
    private bool _returning;
    private GameObject _player;
    private Rigidbody _playerRigidbody;
    private Rigidbody _platformRigidbody;
    private bool _onPlatform;
    private Vector3 _LastPos;
    private Vector3 _CurrentPos;
    private bool _enabled;

    void Start()
    {
        startPosition = transform.position;
        _target = endPosition;
        _platformRigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator WaitPlatform()
    {
        _enabled = true;
        _returning = !_returning;
        _target = _target == startPosition ? endPosition : startPosition;
        yield return new WaitForSeconds(waitTime);
        _enabled = false;
    }

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
                if (_onPlatform)
                {
                    if (_isPlatformMoving)
                    {
                        Vector3 vel = _platformRigidbody.position - _LastPos;
                        _LastPos = _platformRigidbody.position;
                        _playerRigidbody.AddForce(vel, ForceMode.Force);
                    }
                }
            }
            else
            {
                _isPlatformMoving = false;
                StartCoroutine(WaitPlatform());
            }
        }
        // todo https://www.reddit.com/r/Unity3D/comments/14h5zkx/comment/jp9rkaa/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_player == null)
        {
            _LastPos = new Vector3(0, 0, 0);
            _onPlatform = true;
            _player = other.collider.gameObject;
            _playerRigidbody = _player.GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _onPlatform = false;
    }
}
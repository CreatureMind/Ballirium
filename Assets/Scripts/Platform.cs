using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float waitTime = 10f;
    public float speed;
    public Vector3 startPosition = Vector3.zero;
    public Vector3 endPosition = Vector3.zero;

    private bool _isPlatformMoving;
    private bool _onPlatform;
    private bool _enabled;
    private GameObject _player;
    private Rigidbody _playerRigidbody;
    private Rigidbody _platformRigidbody;
    private Vector3 _target = Vector3.zero;
    private Vector3 _lastPos = Vector3.zero;
    void Start()
    {
        Debug.Log("From start 1: " + startPosition);
        Debug.Log("From start 2: " + endPosition);

        _platformRigidbody = GetComponent<Rigidbody>();
        _platformRigidbody.position = startPosition;
        if (_target == Vector3.zero)
        {
            _target = endPosition;
        }
    }

    public void SetStartPos()
    {
        startPosition = transform.position;
    }

    public void SetEndPos()
    {
        endPosition = transform.position;
    }
    private IEnumerator WaitPlatform()
    {
        _enabled = true;
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
                        Vector3 vel = _platformRigidbody.position - _lastPos;
                        _lastPos = _platformRigidbody.position;
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
            _lastPos = Vector3.zero;
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
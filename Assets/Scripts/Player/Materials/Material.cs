using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Material : MonoBehaviour
{
    private string _name;
    private float _mass;
    private float _drag;
    private bool _canJump;
    private float _jumpForce;
}
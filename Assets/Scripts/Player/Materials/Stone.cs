using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Material
{
    // Start is called before the first frame update
    private string _name = "Stone";
    private float _mass = 1.0f;
    private float _drag = 0.25f;
    private float _jumpForce;
    private bool _canJump = false;
    
}
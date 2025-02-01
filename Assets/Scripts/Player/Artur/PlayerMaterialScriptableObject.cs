using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "Material", menuName = "Scriptable Objects/Player Material", order = 1)]
public class PlayerMaterialScriptableObject : ScriptableObject
{
    public string name;
    public float jumpForce;
    public float moveForce;
    public float mass;
    public float drag;
    public float angularDrag;
    public UnityEngine.Material material;
    
    public bool useGravity;
    public bool isKinematic;
    public bool canJump;
}

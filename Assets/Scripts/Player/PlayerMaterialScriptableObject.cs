using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "Material", menuName = "Scriptable Objects/Player Material", order = 1)]
public class PlayerMaterialScriptableObject : ScriptableObject
{
    public new string name;
    public float moveForce;
    public float jumpForce;
    public float maxVelocity;
    public float mass;
    public float drag;
    public float angularDrag;
    public Material material;
    
    public bool useGravity;
    public bool isKinematic;
    public bool canJump;
}

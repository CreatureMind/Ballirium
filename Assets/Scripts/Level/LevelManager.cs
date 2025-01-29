using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public Material[] _materials = null; // _materials used on each level
    
    void Start()
    {
        //pseudocode for level 1
        //add stone 
        _materials = new Material[] { gameObject.AddComponent<Stone>() };
        Debug.Log("array length: " + _materials.Length + " | index 0: " + _materials[0]);
        MaterialManager matManager = new MaterialManager(_materials);

    }


    private void Update()
    {
        
    }
}

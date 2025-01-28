using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MaterialManager : MonoBehaviour
{
    private Material[] _materialManagers = null;

    public MaterialManager(Material[] mats)
    {
        if (mats != null)
        {
            _materialManagers = mats;
        }
    }
}

public interface Material
{
    string name { get; set; }
    float mass { get; set; }
    float drag { get; set; }
}
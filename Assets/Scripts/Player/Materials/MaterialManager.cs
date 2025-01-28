using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
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

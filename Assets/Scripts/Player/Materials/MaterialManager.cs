using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager 
{
    public Material[] materialManager = null;

    public MaterialManager(Material[] mats)
    {
        if (mats != null)
        {
            materialManager = mats;
        }
    }
}

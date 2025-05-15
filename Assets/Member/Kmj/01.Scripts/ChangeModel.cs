using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : MonoBehaviour
{   
    public List<GameObject> prefabs;

    private void Awake()
    {
        prefabs.ForEach(plane => plane.SetActive(false));
    }
}

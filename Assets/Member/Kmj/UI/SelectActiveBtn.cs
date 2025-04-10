using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectActiveBtn : MonoBehaviour
{
    [field :SerializeField] public List<GameObject> _invenList {get; private set;}

    [field: SerializeField] public int currentListCount { get; set; } = 0;

    public void SetInventory()
    {
        _invenList.Clear();
        currentListCount = 0;
    }     
}

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VolumeComponent;

public class SelectActiveBtn : MonoBehaviour
{
    [field :SerializeField] public List<GameObject> _invenList {get; private set;}

    [field: SerializeField] public int currentListCount  { get; set; } = 0;

    private void Awake()
    {
        GetComponentsInChildren<Inven>().ToList().ForEach(UI=> 
        _invenList.Add(UI.gameObject));
    }
    public void SetInventory()
    {
        _invenList.Clear();
        currentListCount = 0;
    }
}

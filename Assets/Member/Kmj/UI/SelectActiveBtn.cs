using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VolumeComponent;

public class SelectActiveBtn : MonoBehaviour
{
    public Button StaticBtn;
    [field :SerializeField] public List<GameObject> _invenList {get; private set;}

    [field: SerializeField] public int currentListCount  { get; set; } = 1;


    private void Awake()
    {
        GetComponentsInChildren<Inven>().ToList().ForEach(UI=> 
        _invenList.Add(UI.gameObject));
    }

    public void SetInventory()
    {
        _invenList.Clear();
        currentListCount = 1;
    }
}

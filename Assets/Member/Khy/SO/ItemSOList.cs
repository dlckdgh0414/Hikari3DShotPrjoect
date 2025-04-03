using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSOList", menuName = "Scriptable Objects/ItemSOList")]
public class ItemSOList : ScriptableObject
{
    public List<ItemSO> intList = new();
}

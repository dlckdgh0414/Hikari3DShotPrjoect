using System;
using UnityEngine;

[CreateAssetMenu(fileName = "scenSO", menuName = "SO/scenSO", order = 1)]
public class scenSO : ScriptableObject
{
   public Action<bool> OnScenEvent;


    public void Raise()
    {
     OnScenEvent.Invoke(true);
    }
}

using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class FruitsButtonData : MonoBehaviour
{
    public FruitsSO FruitsButtonSO { get; set; }
    
    public List<FruitsSO> FruitsDataList = new List<FruitsSO>();
    
    public List<FruitsButtonData> FruitsButtonDataList = new List<FruitsButtonData>();
    public Button Button { get; set; }
    public bool isActive { get; set; }
    public FruitsButtonData(Button button)
    {
        Button = button;
    }
}

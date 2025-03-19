using UnityEngine;
using UnityEngine.UIElements;

public class FruitsButtonData : MonoBehaviour
{
    public FruitsSO FruitsButtonSO { get; set; }
    public Button Button { get; set; }
    public bool isActive { get; set; }
    public FruitsButtonData(Button button)
    {
        Button = button;
    }
}

using UnityEngine;

public interface IFruits
{
    public FruitsSO FruitsSO { get; }

    public void Initialize(Fruits fruits) { }
}

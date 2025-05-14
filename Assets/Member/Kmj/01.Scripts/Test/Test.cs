using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    private GameObject _player;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_player);
        }
    }
}

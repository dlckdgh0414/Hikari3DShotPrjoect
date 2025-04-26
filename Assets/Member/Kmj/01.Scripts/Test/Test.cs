using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private InputReader _input;


    private void Awake()
    {
        _input.TestInput += BindingDebug;
        _input.TestInput2 += BindingDebug2;
        _input.TestInput3 += BindingDebug3;
        _input.TestInput4 += BindingDebug4;
    }

    private void OnDisable()
    {
        _input.TestInput -= BindingDebug;
        _input.TestInput2 -= BindingDebug2;
        _input.TestInput3 -= BindingDebug3;
        _input.TestInput4 -= BindingDebug4;
    }

    private void BindingDebug()
    {
        Debug.Log("1");
    }

    private void BindingDebug2()
    {
        Debug.Log("2");
    }

    private void BindingDebug3()
    {
        Debug.Log("3");
    }

    private void BindingDebug4()
    {
        Debug.Log("4");
    }
}

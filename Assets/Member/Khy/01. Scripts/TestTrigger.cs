using UnityEngine;
using UnityEngine.Events;

public class TestTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnTrigger?.Invoke();
        }
    }
}

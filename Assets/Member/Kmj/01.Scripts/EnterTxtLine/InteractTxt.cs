using UnityEngine;

public class InteractTxt : MonoBehaviour
{
    private ShowTuToTxt _txtParent;

    private void Awake()
    {
        _txtParent = GetComponentInParent<ShowTuToTxt>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            _txtParent.OnTxtEvent?.Invoke();
    }
}

using UnityEngine;

namespace Member.Ysc._01_Code.GameSystem
{
    public class MapMover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = Vector3.forward * (-1 * _speed);
        }
    }
}
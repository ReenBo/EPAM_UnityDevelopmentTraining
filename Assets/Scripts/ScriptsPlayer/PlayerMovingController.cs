using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public readonly struct Axis
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
    }

    public class PlayerMovingController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public Camera _camera;

        private float _mouseY;

        [Header("Speed parameters")]
        [Range(0, 10)]
        [SerializeField] private float _xSpeed = 1;
        [Range(0, 10)]
        [SerializeField] private float _zSpeed = 1;
        [Range(0, 50)]
        [SerializeField] private float _sensitivity = 1;

        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            Move();
            RotationAxisY();
        }

        private void Move()
        {
            Vector3 vector3 = new Vector3(
                Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (_xSpeed),
                Rigidbody.velocity.y,
                Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (_zSpeed));

            Rigidbody.velocity = vector3;
        }

        private void RotationAxisY()
        {
            // Супер временное решение!!!
            _mouseY += Input.GetAxis("Mouse X") * _sensitivity;
            Rigidbody.rotation = Quaternion.Euler(0f, _mouseY, 0f);
        }
    }
}

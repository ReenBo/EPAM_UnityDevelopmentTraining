using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class PlayerMovingController : MonoBehaviour
    {
        private AnimationsPlayer _animPlayer;
        private Rigidbody _rigidbody;
        private Camera _camera;

        private Vector3 _mousePos;

        //private Vector3 _xVector;
        //private Vector3 _yVector;
        //private Vector3 _zVector;


        [Header("Speed parameters")]
        [Range(0, 10)]
        [SerializeField] private float _xSpeed = 1;
        [Range(0, 10)]
        [SerializeField] private float _zSpeed = 1;
        [Header("Mouse parameters")]
        [Range(0, 50)]
        [SerializeField] private float _sensitivity = 1;

        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

        private void Awake()
        {
            _animPlayer = GetComponent<AnimationsPlayer>();
            Rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;

            //_xVector = Vector3.right;
            //_yVector = Vector3.up;
            //_zVector = Vector3.forward;
        }

        private void FixedUpdate()
        {
            Move();
            RotationAxisY();
        }

        private void Update()
        {
            RotationAxisY();
            AnimationPlayer();
        }

        private void Move()
        {
            Vector3 vector3 = new Vector3(
                Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (_zSpeed),
                Rigidbody.velocity.y,
                Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (_xSpeed));

            Rigidbody.velocity = vector3;
        }

        private void RotationAxisY()
        {
            Debug.DrawLine(transform.position, _mousePos, Color.red);

            //_mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 direction = _mousePos - transform.position;

            //float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90;

            //print(angle);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            float hitDis = 0f;

            if (playerPlane.Raycast(ray, out hitDis))
            {
                Vector3 targetPoint = ray.GetPoint(hitDis);
                Quaternion targetRot =
                    Quaternion.LookRotation(targetPoint - transform.position);
                targetRot.x = 0f;
                targetRot.z = 0f;
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    targetRot, 
                    _sensitivity * Time.deltaTime);
            }
        }

        private void AnimationPlayer()
        {
            if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0 || 
                Input.GetAxisRaw(Axis.VERTICAL_AXIS) != 0)
            {
                _animPlayer.PlayAnimation(_animPlayer.Run);
            }
            else _animPlayer.StopAnimation(_animPlayer.Run, false);
        }
    }
}

using ET.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables
        private Animator _animator = null;
        private Rigidbody _rigidbody = null;
        private Camera _camera = null;
        private AudioSource _audioSource = null;

        private bool _shoot = false;

        [Header("Speed parameters")]
        [Range(0, 10)]
        [SerializeField] private float _xSpeed = 1;
        [Range(0, 10)]
        [SerializeField] private float _zSpeed = 1;
        [Header("Mouse parameters")]
        [Range(0, 50)]
        [SerializeField] private float _sensitivity = 1;
        [Header("Sound Effects")]
        [SerializeField] private AudioClip _runAudio;
        #endregion

        #region Properties
        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
        #endregion

        #region Animations Hash Code
        private int _run = Animator.StringToHash(AnimationsTags.RUN);
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;
            _audioSource = GetComponent<AudioSource>();
        }

        protected void FixedUpdate()
        {
            MovingAxis();
            RotationAxisY();
        }

        protected void Update()
        {
            AnimationPlayer();
        }

        private void MovingAxis()
        {
            if (_shoot)
            {
                Rigidbody.velocity = Vector3.zero;
            }
            else
            {
                Vector3 vector3 = new Vector3(
                    Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (_zSpeed),
                    Rigidbody.velocity.y,
                    Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (_xSpeed));

                Rigidbody.velocity = vector3;
            }
        }

        private void RotationAxisY()
        {
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
                _animator.SetTrigger(_run);
            }
            else
            {
                _animator.SetBool(_run, false);
            } 
        }

        private void PlaySoundEffects(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}

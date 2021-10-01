using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class PlayerMovingController : MonoBehaviour
    {
        #region Variables
        private Animator _animator = null;
        private Rigidbody _rigidbody = null;
        private Camera _camera = null;
        private WeaponsController _weaponsController = null;

        private bool _shoot = false;
        private bool _reload = false;
        private float reloadTimer = 0f;

        private float _coolDown = 0f;
        private int[] _bulletArray = new int[4] { 0, 1, 2, 3 };
        private int _bulletIDNumber = 0;
        private int _enumNumber = 0;

        [Header("Speed parameters")]
        [Range(0, 10)]
        [SerializeField] private float _xSpeed = 1;
        [Range(0, 10)]
        [SerializeField] private float _zSpeed = 1;
        [Header("Mouse parameters")]
        [Range(0, 50)]
        [SerializeField] private float _sensitivity = 1;
        [SerializeField] private float _reloadTimeWeapon = 1;
        #endregion

        #region Properties
        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

        public int BulletIDNumber
        { 
            get => _bulletIDNumber; 
            set => _bulletIDNumber = Mathf.Clamp(value, 0, 3);
        }
        #endregion

        #region Animations Hash Code
        private int _run = Animator.StringToHash(AnimationsTags.RUN);
        private int _shooting = Animator.StringToHash(AnimationsTags.SHOOTING);
        private int _reloadWeapons = Animator.StringToHash(AnimationsTags.RELODING_WEAPONS);
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;
            _weaponsController = GetComponentInChildren<WeaponsController>();
        }

        protected void FixedUpdate()
        {
            MovingAxis();
            RotationAxisY();
        }

        protected void Update()
        {
            ShootWeapon();
            AnimationPlayer();
            ReloadAmmo();
            SwitchBullets();
        }

        private void MovingAxis()
        {
            if (_shoot || _reload) Rigidbody.velocity = Vector3.zero;
            else
            {
                _animator.SetBool(_shooting, false);

                Vector3 vector3 = new Vector3(
                    Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (_zSpeed),
                    Rigidbody.velocity.y,
                    Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (_xSpeed));

                Rigidbody.velocity = vector3;
                reloadTimer = 0f;
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

        private void ShootWeapon()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _shoot = true;
                _animator.SetTrigger(_shooting);
                StartCoroutine(_weaponsController.TakeShot(_bulletArray[BulletIDNumber]));
            }
            else _shoot = false;
        }

        private void SwitchBullets()
        {
            float mouseScrollnumber = Input.GetAxis("Mouse ScrollWheel");

            if (mouseScrollnumber < 0)
            {
                if (_enumNumber < 3) _enumNumber++;
                else if (_enumNumber == 3) _enumNumber = 0;
            }
            
            if(mouseScrollnumber > 0)
            {
                if (_enumNumber == 0) _enumNumber = 3;
                else if (_enumNumber > 0 ) _enumNumber--;
            }

            BulletIDNumber = _enumNumber;
        }

        private void ReloadAmmo()
        {
            if (Input.GetKey(KeyCode.R))
            {
                if (_weaponsController.AmmoCounter < 40 && _coolDown < _reloadTimeWeapon)
                {
                    _coolDown += Time.deltaTime;

                    _reload = true;
                    _animator.SetTrigger(_reloadWeapons);
                    _weaponsController.ReloadingWeapons();
                }
                else
                {
                    _reload = false;
                    _coolDown = 0f;
                }
            }
        }

        private void AnimationPlayer()
        {
            if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0 || 
                Input.GetAxisRaw(Axis.VERTICAL_AXIS) != 0)
            {
                _animator.SetTrigger(_run);
            }
            else _animator.SetBool(_run, false);
        }
    }
}

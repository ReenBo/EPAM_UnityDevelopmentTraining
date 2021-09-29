using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class PlayerMovingController : MonoBehaviour
    {
        #region Variables
        private AnimationsCharacters _animPlayer;
        private Rigidbody _rigidbody;
        private Camera _camera;
        private WeaponsController _weaponsController;

        private bool _shoot = false;
        private bool _reload = false;
        //private float reloadTimer = 0f;

        private float _coolDown = 0f;
        private int[] _bulletArray = new int[4] { 0, 1, 2, 3 };
        private int _bulletTypeNumber = 0;
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
        public int BulletTypeNumber { 
            get => _bulletTypeNumber; set => _bulletTypeNumber = Mathf.Clamp(value, 0, 3); }
        #endregion

        private void Awake()
        {
            _animPlayer = GetComponent<AnimationsCharacters>();
            Rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;
            _weaponsController = GetComponentInChildren<WeaponsController>();
        }

        private void FixedUpdate()
        {
            MovingAxis();
            RotationAxisY();
        }

        private void Update()
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
                _animPlayer.StopAnimation(_animPlayer.Shooting, false);

                Vector3 vector3 = new Vector3(
                    Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (_zSpeed),
                    Rigidbody.velocity.y,
                    Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (_xSpeed));

                Rigidbody.velocity = vector3;
                //reloadTimer = 0f;
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
                _animPlayer.PlayAnimation(_animPlayer.Shooting);
                StartCoroutine(_weaponsController.TakeShot(_bulletArray[BulletTypeNumber]));
            }
            else _shoot = false;
        }

        private void SwitchBullets()
        {
            float mouseScrollnumber = Input.GetAxis("Mouse ScrollWheel");

            if(mouseScrollnumber < 0)
            {
                if (_enumNumber < 3) _enumNumber++;
                else if (_enumNumber == 3) _enumNumber = 0;
            }
            
            if(mouseScrollnumber > 0)
            {
                if (_enumNumber == 0) _enumNumber = 3;
                else if (_enumNumber > 0 ) _enumNumber--;
            }

            BulletTypeNumber = _enumNumber;
        }

        private void ReloadAmmo()
        {
            if (Input.GetKey(KeyCode.R))
            {
                if (_weaponsController.AmmoCounter < 40 && _coolDown < _reloadTimeWeapon)
                {
                    _coolDown += Time.deltaTime;

                    _reload = true;
                    _animPlayer.PlayAnimation(_animPlayer.ReloadWeapons);
                    _weaponsController.ReloadingWeapons();
                }
            }
            else
            {
                _reload = false;
                _coolDown = 0f;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Weapons;

namespace ET.Player.Combat
{
    public class PlayerCombatController : MonoBehaviour
    {
        #region Variables
        private Animator _animator = null;
        private Rigidbody _rigidbody = null;
        private Camera _camera = null;
        private WeaponsController _weaponsController = null;

        private readonly string _fire1 = "Fire1";
        private readonly string _mouseScrollWheel = "Mouse ScrollWheel";

        private bool _shoot = false;
        //private bool _isRuning = false;

        private int[] _bulletArray = new int[4] { 0, 1, 2, 3 };
        private int _bulletIDNumber = 0;
        private int _enumNumber = 0;
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

        protected void Update()
        {
            ShootWeapon();
            ReloadAmmo();
            SwitchBullets();
        }

        private void ShootWeapon()
        {
            if (Input.GetButtonDown(_fire1))
            {
                _shoot = true;
                _animator.SetTrigger(_shooting);
                _weaponsController.TakeShot(_bulletArray[BulletIDNumber]);
            }
            else
            {
                _shoot = false;
            } 
        }

        private void SwitchBullets()
        {
            float mouseScrollNumber = Input.GetAxis(_mouseScrollWheel);

            //bool positiveValue = (mouseScrollNumber > 0) ? true : false;

            if (mouseScrollNumber > 0)
            {
                if (_enumNumber == 0)
                {
                    _enumNumber = 3;
                }
                else if (_enumNumber > 0)
                {
                    _enumNumber--;
                }
            }

            if (mouseScrollNumber < 0)
            {
                if (_enumNumber < 3)
                {
                    _enumNumber++;
                }
                else if (_enumNumber == 3)
                {
                    _enumNumber = 0;
                }
            }

            BulletIDNumber = _enumNumber;
        }

        private void ReloadAmmo()
        {
            if (Input.GetKey(KeyCode.R))
            {
                if (_weaponsController.AmmoCounter >= 30)
                {
                    return;
                }
                else
                {
                    _animator.SetTrigger(_reloadWeapons);
                    _weaponsController.ReloadingWeapons();
                }
            }
        }
    }
}
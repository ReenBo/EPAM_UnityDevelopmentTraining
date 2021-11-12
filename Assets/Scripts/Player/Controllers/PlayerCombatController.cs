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
        private WeaponsController _weaponsController = null;

        [Header("List of weapons")]
        [SerializeField] private List<GameObject> _weaponsList;

        private float _delayShoot = 0f;
        private float _timeDelay = 0f;

        private readonly string _fire1 = "Fire1";
        private readonly string _mouseScrollWheel = "Mouse ScrollWheel";

        private bool _isShooting = false;
        //private bool _isRuning = false;

        private int[] _bulletArray = new int[4] { 0, 1, 2, 3 };
        private int _bulletIDNumber = 0;
        private int _enumNumber = 0;
        private int numberBulletPlayerHas = 3;

        private WeapomType _weapomType;
        private string _nameWeapon = string.Empty;
        private int _amountBullets = 0;
        private int _amountAmmo = 0;

        private KeyCode[] _keyCodes;
        private int _selectedWeapon = 1;
        #endregion

        public int BulletIDNumber
        {
            get => _bulletIDNumber;
            set => _bulletIDNumber = Mathf.Clamp(value, 0, numberBulletPlayerHas);
        }

        #region Animations Hash Code
        private int _shooting = Animator.StringToHash(AnimationsTags.SHOOTING);
        private int _reloadWeapons = Animator.StringToHash(AnimationsTags.RELODING_WEAPONS);
        private int _changingWeapon = Animator.StringToHash(AnimationsTags.CHANGING_WEAPON);
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected void Start()
        {
            UpdateWeaponStats();
            DetermineTypeOfWeapon();

            _keyCodes = new KeyCode[]
            {
                KeyCode.None,
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                KeyCode.Alpha3,
            };

            //GameManager.Instance.WeaponView.DisplayWeapon(_timeDelay, _nameWeapon, ((int)_weapomType) - 1);
        }

        protected void Update()
        {
            TakeShot();
            ReloadAmmo();
            ChangingWeapon();
            SwitchBullets();
        }

        private void TakeShot()
        {
            if (_delayShoot <= 0)
            {
                if (Input.GetButton(_fire1))
                {
                    _isShooting = true;
                    _animator.SetTrigger(_shooting);

                    _weaponsController.Shoot(_bulletArray[BulletIDNumber]);

                    _delayShoot = _timeDelay;
                }
            }
            else
            {
                _isShooting = false;
                _delayShoot -= Time.deltaTime;
            }
        }

        private void ChangingWeapon()
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (Input.GetKey(_keyCodes[i]))
                {
                    foreach (var weapon in _weaponsList)
                    {
                        weapon.SetActive(false);
                    }

                    if(_selectedWeapon != i)
                    {
                        _animator.SetTrigger(_changingWeapon);
                    }

                    _weaponsList[i - 1].SetActive(true);

                    UpdateWeaponStats();
                    DetermineTypeOfWeapon();

                    _selectedWeapon = i;

                    //GameManager.Instance.WeaponView.DisplayWeapon(_timeDelay, _nameWeapon, i - 1);

                    //GameManager.Instance.PlayerStatsViem.SetAmmoCountViem(_amountBullets, _amountAmmo);
                }
            }
        }

        private void UpdateWeaponStats()
        {
            _weaponsController = GetComponentInChildren<WeaponsController>();

            _weapomType = _weaponsController.WeaponType;
            _timeDelay = _weaponsController.TimeDelay;
            _amountBullets = _weaponsController.NumberRoundsInMagazine;
            _amountAmmo = _weaponsController.AmmoCounter;
            var thisAudioSource = _weaponsController.AudioSource;

            _weaponsController.AudioSource = thisAudioSource;
            _weaponsController.AmmoCounter = _amountAmmo;
        }

        private void DetermineTypeOfWeapon()
        {
            _nameWeapon = _weapomType.ToString();
        }

        private void SwitchBullets()
        {
            float mouseScrollNumber = Input.GetAxis(_mouseScrollWheel);
            int numberBulletsPlayerHas = 3;

            if (mouseScrollNumber > 0)
            {
                if (_enumNumber == 0)
                {
                    _enumNumber = numberBulletsPlayerHas;
                }
                else if (_enumNumber > 0)
                {
                    _enumNumber--;
                }
            }

            if (mouseScrollNumber < 0)
            {
                if (_enumNumber < numberBulletsPlayerHas)
                {
                    _enumNumber++;
                }
                else if (_enumNumber == numberBulletsPlayerHas)
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
                if (_weaponsController.AmmoCounter >= _weaponsController.NumberRoundsInMagazine)
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

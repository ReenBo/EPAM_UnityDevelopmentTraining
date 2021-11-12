using ET.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Weapons
{
    public enum WeapomType
    {
        NONE,
        PISTOL,
        UZI,
        RIFLE,
        RPG,
        GUN_TURRET
    }

    public class WeaponsController : MonoBehaviour
    {
        #region Variables
        private AudioSource _audioSource = null;

        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _targetPos;

        [Header("WeaponType")]
        [SerializeField] private WeapomType _weaponType;
        [SerializeField] private int _numberRoundsInMagazine;
        [SerializeField] private float _timeDelay;

        [Header("WFX Effects")]
        [SerializeField] private GameObject _muzzleFlashesPrefab;
        [SerializeField] private Light _muzzleFlashesLight;

        [Header("Sound Effects")]
        [SerializeField] private AudioClip _shootingAudio;
        [SerializeField] private AudioClip _reloadingAudio;

        [SerializeField] private GameObject[] _arrayBullets = new GameObject[4];

        private ParticleSystem _muzzleFlashes = null;
        private Transform _bulletSpawn;
        private bool _getAmmo = true;

        private int _ammoCounter = 0;
        #endregion

        #region Properties
        public int AmmoCounter { get => _ammoCounter; set => _ammoCounter = value; }
        public WeapomType WeaponType { get => _weaponType; }
        public int NumberRoundsInMagazine { get => _numberRoundsInMagazine; }
        public float TimeDelay { get => _timeDelay; }
        public AudioSource AudioSource { get => _audioSource; set => _audioSource = value; }
        #endregion

        protected void Awake()
        {
            _ammoCounter = _numberRoundsInMagazine;
            _audioSource = GetComponent<AudioSource>();
        }

        protected void Start()
        {
            //GameManager.Instance.PlayerStatsViem.SetAmmoCountViem(_numberRoundsInMagazine, _ammoCounter);

            _muzzleFlashes = _muzzleFlashesPrefab.GetComponent<ParticleSystem>();
        }

        #region Metods
        private void CreateProjectile(GameObject bullet)
        {
            _bulletSpawn = Instantiate(bullet.transform, _shootPoint.position, 
                Quaternion.identity);
            _bulletSpawn.rotation = _shootPoint.rotation;

        }

        public void Shoot(int num)
        {
            if(_getAmmo)
            {
                _muzzleFlashes.Play();
                StartCoroutine(LightFlickering(0.2f));

                CreateProjectile(_arrayBullets[num]);
                CalculateAmmos();

                PlaySoundEffects(_shootingAudio);
            }
        }

        private IEnumerator LightFlickering(float timeMuzzleFlash)
        {
            float timer = timeMuzzleFlash;

            while (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
                _muzzleFlashesLight.enabled = true;
                yield return null;
            }

            _muzzleFlashesLight.enabled = false;
        }

        public void ReloadingWeapons()
        {
            PlaySoundEffects(_reloadingAudio);

            _ammoCounter = _numberRoundsInMagazine;
            _getAmmo = true;
            //GameManager.Instance.PlayerStatsViem.SetAmmoCountViem(_numberRoundsInMagazine, _ammoCounter);
        }

        private void CalculateAmmos()
        {
            if (_ammoCounter > 0)
            {
                _ammoCounter -= 1;
                //GameManager.Instance.PlayerStatsViem.SetAmmoCountViem(_numberRoundsInMagazine, _ammoCounter);
            }
            else _getAmmo = false;
        }

        private void PlaySoundEffects(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        #endregion
    }
}

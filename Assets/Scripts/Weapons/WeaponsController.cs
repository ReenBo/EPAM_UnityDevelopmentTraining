using ET.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Weapons
{
    public class WeaponsController : MonoBehaviour
    {
        #region Variables
        private AudioSource _audioSource = null;

        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _targetPos;

        [Header("Sound Effects")]
        [SerializeField] private AudioClip _shootingAudio = null;
        [SerializeField] private AudioClip _reloadingAudio = null;

        [SerializeField] private GameObject[] _arrayBullets = new GameObject[4];

        private Transform _bulletSpawn;
        private bool _getAmmo = true;

        private int _ammoCounter = 30;
        #endregion

        #region Properties
        public int AmmoCounter { get => _ammoCounter; set => _ammoCounter = value; }
        #endregion

        protected void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        #region Metods
        private void CreateProjectile(GameObject bullet)
        {
            _bulletSpawn = Instantiate(bullet.transform, _shootPoint.position, 
                Quaternion.identity);

            _bulletSpawn.rotation = _shootPoint.transform.rotation;
        }

        public void TakeShot(int num)
        {
            if(_getAmmo)
            {
                CreateProjectile(_arrayBullets[num]);
                CalculateAmmos();
                PlaySoundEffects(_shootingAudio);
            }
        }

        public void ReloadingWeapons()
        {
            PlaySoundEffects(_reloadingAudio);

            int countAmmo = 30;
            AmmoCounter = countAmmo;
            _getAmmo = true;
            GameManager.Instance.PlayerStatsViem.SetAmmoCountViem(AmmoCounter);
        }

        private void CalculateAmmos()
        {
            if (AmmoCounter > 0)
            {
                AmmoCounter -= 1;
                GameManager.Instance.PlayerStatsViem.SetAmmoCountViem(AmmoCounter);
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

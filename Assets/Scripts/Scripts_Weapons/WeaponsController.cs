using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class WeaponsController : MonoBehaviour
    {
        #region Variables
        PlayerViem _playerViem;

        [SerializeField] private Transform _shotPoint;
        [SerializeField] private Transform _targetPos;

        [SerializeField] private GameObject[] _arrayBullets = new GameObject[4];

        private Transform _bulletSpawn;
        private bool _getAmmo = true;

        private int _ammoCounter = 30;
        #endregion

        #region Properties
        public int AmmoCounter { get => _ammoCounter; private set => _ammoCounter = value; }
        #endregion

        private void Awake()
        {
            _playerViem = GetComponentInParent<PlayerViem>();
            _playerViem.SetAmmoCountViem(AmmoCounter);
        }

        #region Metods
        private void CreateProjectile(GameObject bullet)
        {
            _bulletSpawn = Instantiate(bullet.transform, _shotPoint.position, 
                Quaternion.identity);

            _bulletSpawn.rotation = _shotPoint.transform.rotation;
        }

        public IEnumerator TakeShot(int num)
        {
            if(_getAmmo)
            {
                CreateProjectile(_arrayBullets[num]);
                CalculateAmmos();

                //yield return new WaitForSeconds(_delayTime);
            }
            yield return null;
        }

        public void ReloadingWeapons()
        {
            int countAmmo = 30;
            AmmoCounter = (countAmmo - AmmoCounter) + AmmoCounter;
            _getAmmo = true;
            _playerViem.SetAmmoCountViem(AmmoCounter);
        }

        private void CalculateAmmos()
        {
            if (AmmoCounter > 0)
            {
                AmmoCounter -= 1;
                _playerViem.SetAmmoCountViem(AmmoCounter);
            }
            else _getAmmo = false;
        }
        #endregion
    }
}

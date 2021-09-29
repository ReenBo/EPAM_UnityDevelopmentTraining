using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class WeaponsController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private Transform _targetPos;

        [SerializeField] private GameObject[] _arrayBullets = new GameObject[4];

        private Transform _bulletSpawn;
        private float _delayTime = 1f;
        private bool _getAmmo = true;

        private int _ammoCounter = 40;
        #endregion

        #region Properties
        public int AmmoCounter { get => _ammoCounter; private set => _ammoCounter = value; }
        #endregion

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
                CalculateCartridges();

                //yield return new WaitForSeconds(_delayTime);
            }
            yield return null;
        }

        public void ReloadingWeapons()
        {
            int countAmmo = 40;
            AmmoCounter = (countAmmo - AmmoCounter) + countAmmo;
            _getAmmo = true;
        }

        private void CalculateCartridges()
        {
            if (AmmoCounter > 0)
            {
                AmmoCounter -= 1;
            }
            else _getAmmo = false;
        }
        #endregion
    }
}
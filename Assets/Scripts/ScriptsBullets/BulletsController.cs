using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class BulletsController : MonoBehaviour
    {
        #region Variables
        IDamageable damageable;

        private GameObject _enemyObject = null;

        [Header("Settings Bullet:")]
        [SerializeField] private float _speedBullet = 0f;
        [SerializeField] private float _lifeTimeBullet = 0f;
        [SerializeField] private float _damageBullet = 0f;
        #endregion

        #region Properties
        public GameObject EnemyObject { get => _enemyObject; set => _enemyObject = value; }
        public float DamageBullet { get => _damageBullet; }
        #endregion

        private void Awake()
        {
            EnemyObject = GameObject.FindGameObjectWithTag("EnemyZombie");

            if(this.gameObject != null)
            {
                Destroy(this.gameObject, _lifeTimeBullet);
            }
        }

        protected void Update()
        {
            BulletMovements();
        }

        public void BulletMovements()
        {
            transform.Translate(Vector3.right * _speedBullet * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider collider)
        {
            damageable = collider.GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageable.Damage(DamageBullet);

                EnemyObject.GetComponentInChildren<Renderer>().material.color =
                    Color.red;

                Destroy(this.gameObject);
            }
        }
    }
}

using System;
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

        protected void Awake()
        {
            EnemyObject = GameObject.FindGameObjectWithTag("EnemyZombie");

            if(gameObject != null)
            {
                Destroy(this.gameObject, _lifeTimeBullet);
            }
        }

        protected void FixedUpdate()
        {
            BulletMovements();
        }

        public void BulletMovements()
        {
            transform.Translate(Vector3.right * _speedBullet * Time.deltaTime);
        }

        protected void OnTriggerEnter(Collider collider)
        {
            try
            {
                damageable = collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    Destroy(gameObject);
                    damageable.Damage(DamageBullet);

                    //EnemyObject.GetComponentInChildren<Renderer>().material.color =
                    //    Color.red;
                }
            }
            catch(Exception message)
            {
                Debug.LogError(message.Source);
            }
        }
    }
}

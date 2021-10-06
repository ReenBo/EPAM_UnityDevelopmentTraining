using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Bullets
{
    public class BulletsController : MonoBehaviour
    {
        #region Variables
        IDamageable damageable;

        [Header("Settings Bullet:")]
        [SerializeField] private float _speedBullet = 0f;
        [SerializeField] private float _lifeTimeBullet = 0f;
        [SerializeField] private float _damageBullet = 0f;
        #endregion

        #region Properties
        public float DamageBullet { get => _damageBullet; }
        #endregion

        protected void Awake()
        {
            if(gameObject)
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
            damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(DamageBullet);
                Destroy(gameObject);
            }
        }
    }
}

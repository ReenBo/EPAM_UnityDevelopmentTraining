using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        #region Variables
        private EnemyStateController _enemyState = null;

        [Header("Parameters Object")]
        [Range(0, 100)]
        [SerializeField] private float _amountHealth = 0;
        [SerializeField] private float _amountDamage = 0;
        [SerializeField] private float _movementSpeed = 0;

        private bool _isDeath = false;
        //private bool _resurrection = false;
        #endregion

        #region Properties
        public float AmountHealth { get => _amountHealth; set => _amountHealth = Mathf.Clamp(value, 0f, 100f); }
        public float AmountDamage { get => _amountDamage; set => _amountDamage = value; }
        public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
        public EnemyStateController EnemyState { get => _enemyState; set => _enemyState = value; }
        public bool IsDeath { get => _isDeath; set => _isDeath = value; }
        #endregion

        private void Awake()
        {
            EnemyState = GetComponent<EnemyStateController>();
        }

        #region Methods
        public void Damage(float count)
        {
            if(this.gameObject != null)
            {
                if (IsDeath) return;
                else if (AmountHealth > 0)
                {
                    AmountHealth -= count;
                }
                else if (AmountHealth <= 0)
                {
                    AmountHealth = 0f;
                    EnemyIsDying();
                }
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            BulletsController bullet = collider.GetComponent<BulletsController>();

            Debug.Log(IsDeath);

            if (IsDeath) return;
            else if(bullet != null)
            {
                int num = Random.Range(0, 10);

                Debug.Log($"Probability = {num}");

                switch (num)
                {
                    case 1:
                        StartCoroutine(EnemyState.StateStandUp());
                        break;
                    case 2:
                        StartCoroutine(EnemyState.StateHit());
                        break;
                    default:
                        print("default");
                        break;
                }
            }
        }

        private void EnemyIsDying()
        {
            IsDeath = true;
            StopAllCoroutines();
            StartCoroutine(EnemyState.StateDeath());
        }
        #endregion
    }
}

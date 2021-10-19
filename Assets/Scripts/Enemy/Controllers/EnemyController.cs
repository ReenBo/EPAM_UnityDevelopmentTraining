using ET.Enemy.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        #region Variables
        private EnemyStateController _enemyState = null;
        private EnemyAttacksController[] _enemyAttacksController = null;
        private AudioSource _audioSource = null;

        [Header("Parameters Object")]
        [Range(0, 100)]
        [SerializeField] private float _amountHealth = 0;
        [SerializeField] private float _amountDamage = 0;
        [SerializeField] private int _amountExperience = 0;
        [SerializeField] private GameObject _ArmR;
        [SerializeField] private GameObject _ArmL;
        [SerializeField] private AudioClip _deadAudio;
        [SerializeField] private AudioClip _hitAudio;

        private bool _isDeath = false;
        //private bool _isResurrection = false;
        #endregion

        #region Properties
        public float AmountHealth { get => _amountHealth; set => _amountHealth = Mathf.Clamp(value, 0f, 100f); }
        public float AmountDamage { get => _amountDamage; }
        public bool IsDeath { get => _isDeath; set => _isDeath = value; }
        #endregion

        protected void Awake()
        {
            _enemyState = GetComponent<EnemyStateController>();
            _enemyAttacksController = GetComponentsInChildren<EnemyAttacksController>();
            _audioSource = GetComponent<AudioSource>();

            foreach (var item in _enemyAttacksController)
            {
                item.DamageArm = _amountDamage;
            }
        }

        #region Methods
        public void Damage(float count)
        {
            if(gameObject)
            {
                if (IsDeath) return;
                else if (AmountHealth > 0)
                {
                    _audioSource.clip = _hitAudio;
                    _audioSource.Play();

                    AmountHealth -= count;

                    int num = UnityEngine.Random.Range(0, 10);

                    switch (num)
                    {
                        case 1:
                            StartCoroutine(_enemyState.StateStandUp());
                            break;
                        case 2:
                            StartCoroutine(_enemyState.StateHit());
                            break;
                        default:
                            break;
                    }
                }
                else if (AmountHealth <= 0)
                {
                    AmountHealth = 0f;
                    EnemyIsDying();
                }
            }
        }

        private void EnemyIsDying()
        {
            GameManager.Instance.LevelSystem.CalculateExperiencePlayer(_amountExperience);

            _audioSource.clip = _deadAudio;
            _audioSource.Play();

            _ArmR.SetActive(false);
            _ArmL.SetActive(false);

            IsDeath = true;

            StopAllCoroutines();
            StartCoroutine(_enemyState.StateDeath());
        }
        #endregion
    }
}

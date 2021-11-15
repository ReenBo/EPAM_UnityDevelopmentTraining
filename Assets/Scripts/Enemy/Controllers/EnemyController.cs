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

        public event Action<float> onExperiencePlayerChange;

        private bool _isDeath = false;
        //private bool _isResurrection = false;
        #endregion

        public float AmountHealth { get => _amountHealth; set => _amountHealth = Mathf.Clamp(value, 0f, 100f); }
        public float AmountDamage { get => _amountDamage; }
        public bool isDeath { get => _isDeath; }

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

        public void Damage(float count)
        {
            if(gameObject)
            {
                if (_isDeath) return;

                if (AmountHealth > 1e-3)
                {
                    _audioSource.PlayOneShot(_hitAudio);

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
                else if (AmountHealth < 1e-3)
                {
                    AmountHealth = 0f;
                    EnemyIsDying();
                }
            }
        }

        private void EnemyIsDying()
        {
            GameManager.Instance.LevelSystem.CalculateExperiencePlayer(_amountExperience);

            _audioSource.PlayOneShot(_deadAudio);

            _ArmR.SetActive(false);
            _ArmL.SetActive(false);

            _isDeath = true;

            StopAllCoroutines();
            StartCoroutine(_enemyState.StateDeath());
        }
    }
}

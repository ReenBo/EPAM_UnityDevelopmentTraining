using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ET.Scenes;
using ET.Stats;
using System;

namespace ET.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Animator _animator = null;
        private BoxCollider _boxCollider = null;

        [Header("Parameters Object")]
        [Range(0, 100)]
        [SerializeField] private float _maxHealth = 0;
        [Range(0, 100)]
        [SerializeField] private float _maxArmor = 0;

        private float _currentHealth = 0;
        private float _currentArmor = 0;

        private float _currentLevel = 1;

        private bool _isDead = false;
        #endregion

        #region Properties
        #endregion

        protected void Awake()
        {
            _currentArmor = _maxArmor;
            _currentHealth = _maxHealth;

            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            Save();
        }

        public void Damage(float amount)
        {
            if (gameObject != null)
            {
                if (_isDead)
                {
                    return;
                } 

                if(_currentArmor > 0f)
                {
                    _currentArmor -= amount;
                    GameManager.Instance.PlayerViem.SetArmorView(amount, (int)_currentArmor);
                }
                else if(_currentArmor <= 0f)
                {
                    if (_currentHealth > 0f)
                    {
                        _currentArmor = 0f;

                        _currentHealth -= amount;
                        GameManager.Instance.PlayerViem.SetHealthView(amount, (int)_currentHealth);
                    }
                    else if (_currentHealth <= 0f)
                    {
                        _currentHealth = 0f;

                        PlayerIsDying();
                    }
                }
            }
        }

        private void Save()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                GameManager.Instance.Stats.CharacterStatsInitialize(
                    _currentHealth,
                    _currentArmor,
                    _currentLevel);
            }
        }

        public event Action<bool> OnPlayerIsDying; //!!!!

        private void PlayerIsDying()
        {
            if (_isDead)
            {
                return;
            }
            else
            {
                _isDead = true;

                OnPlayerIsDying?.Invoke(_isDead); //!!!!

                _boxCollider.isTrigger = true;
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                _animator.SetTrigger(AnimationsTags.DEATH_TRIGGER);

                GameManager.Instance.SceneController.GameOver();

                //Destroy(gameObject, 3);
            }
        }
    }
}

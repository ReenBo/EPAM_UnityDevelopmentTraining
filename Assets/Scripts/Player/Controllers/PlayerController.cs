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
        private PlayerViem _playerViem = null;
        private BoxCollider _boxCollider = null;

        [Header("Parameters Object")]
        [Range(0, 100)]
        [SerializeField] private float _maxHealth = 0;
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
            _currentHealth = _maxHealth;
            _currentArmor = _maxArmor;

            _animator = GetComponent<Animator>();
            _playerViem = GetComponent<PlayerViem>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            Save();
        }

        public void Damage(float count)
        {
            if (gameObject != null)
            {
                if (_isDead)
                {
                    return;
                } 
                else if (_currentHealth > 0)
                {
                    _currentHealth -= count;
                    _playerViem.SetHealthViem(count);
                }
                else if (_currentHealth <= 0)
                {
                    _currentHealth = 0f;

                    PlayerIsDying();
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

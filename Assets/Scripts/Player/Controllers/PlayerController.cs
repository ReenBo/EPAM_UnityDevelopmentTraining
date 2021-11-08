using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ET.Scenes;
using ET.Core.Stats;
using System;
using ET.Core.UIRoot;
using ET.Player.Skills;

namespace ET.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator = null;
        private BoxCollider _boxCollider = null;

        private PlayerSkillsController _playerSkills = null;

        [Header("Parameters Object")]
        [SerializeField] private Transform _playerPosition;
        [Range(0, 100)]
        [SerializeField] private float _maxHealth = 0;
        [Range(0, 100)]
        [SerializeField] private float _maxArmor = 0;

        private float _currentHealth = 0;
        private float _currentArmor = 0;

        private bool _isDead = false;

        public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
        public float CurrentArmor { get => _currentArmor; set => _currentArmor = value; }
        public Transform PlayerPosition { get => _playerPosition; }
        public PlayerSkillsController PlayerSkills { get => _playerSkills; }

        protected void Awake()
        {
            _currentArmor = _maxArmor;
            _currentHealth = _maxHealth;

            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
            _playerSkills = GetComponent<PlayerSkillsController>();
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
                    GameManager.Instance.PlayerStatsViem.SetArmorView(amount, (int)_currentArmor);
                }
                else if(_currentArmor <= 0f)
                {
                    if (_currentHealth > 0f)
                    {
                        _currentArmor = 0f;

                        _currentHealth -= amount;
                        GameManager.Instance.PlayerStatsViem.SetHealthView(amount, (int)_currentHealth);
                    }
                    else if (_currentHealth <= 0f)
                    {
                        _currentHealth = 0f;

                        PlayerIsDying();
                    }
                }
            }
        }

        public Action onPlayerDied;

        private void PlayerIsDying()
        {
            if (_isDead)
            {
                return;
            }
            else
            {
                _isDead = true;

                _boxCollider.isTrigger = true;
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                _animator.SetTrigger(AnimationsTags.DEATH_TRIGGER);

                onPlayerDied.Invoke();

                //UIRoot.Instance.GameOverWindow.FinishGame();

                //Destroy(gameObject, 3);
            }
        }
    }
}

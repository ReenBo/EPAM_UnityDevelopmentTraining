using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ET.Scene;
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
        [SerializeField] private float _amountHealth = 0;

        private bool _isDead = false;
        #endregion

        #region Properties
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerViem = GetComponent<PlayerViem>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void Damage(float count)
        {
            if (gameObject != null)
            {
                if (_isDead)
                {
                    return;
                } 
                else if (_amountHealth > 0)
                {
                    _amountHealth -= count;
                    _playerViem.SetHealthViem(count);
                }
                else if (_amountHealth <= 0)
                {
                    _amountHealth = 0f;

                    PlayerIsDying();
                }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ET.Player.States;
using ET.Scene;
using System;

namespace ET.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        ICheckingStateable _checkingStateable;
        private Animator _animator = null;
        private PlayerViem _playerViem = null;
        private BoxCollider _boxCollider = null;

        [Header("Parameters Object")]
        [SerializeField] private PLAYER_STATE _playerState = PLAYER_STATE.IS_LIVES;
        [Range(0, 100)]
        [SerializeField] private float _amountHealth = 0;
        private bool _isDeath = false;
        #endregion

        #region Properties
        public bool IsDeath { get => _isDeath; private set => _isDeath = value; }
        public PLAYER_STATE PlayerState { get => _playerState; private set => _playerState = value; }
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
                if (PlayerState == PLAYER_STATE.IS_DEAD)
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

                    StartCoroutine(PlayerIsDying());
                }
            }
        }

        private IEnumerator PlayerIsDying()
        {
            if (PlayerState == PLAYER_STATE.IS_DEAD)
            {
                yield break;
            }

            while(PlayerState == PLAYER_STATE.IS_LIVES)
            {
                _boxCollider.isTrigger = true;
                gameObject.GetComponent<PlayerMovingController>().enabled = false;
                _animator.SetTrigger(AnimationsTags.DEATH_TRIGGER);

                GameManager.Instance.SceneLoader.GetComponent<SceneController>().GameOver();

                PlayerState = PLAYER_STATE.IS_DEAD;
                //Destroy(gameObject, 3);
            }
            yield return null;
        }
    }
}

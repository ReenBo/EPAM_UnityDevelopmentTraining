using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Animator _animator = null;
        private PlayerViem _playerViem = null;

        [Header("Parameters Object")]
        [Range(0, 100)]
        [SerializeField] private float _amountHealth = 0;
        private bool _isDeath = false;
        #endregion

        #region Properties
        public bool IsDeath { get => _isDeath; private set => _isDeath = value; }
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerViem = GetComponent<PlayerViem>();
        }

        public void Damage(float count)
        {
            if (gameObject != null)
            {
                if (IsDeath) return;
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

        private void PlayerIsDying()
        {
            if (IsDeath) return;
            else
            {
                IsDeath = true;
                _animator.SetTrigger(AnimationsTags.DEATH_TRIGGER);
                
                gameObject.SetActive(false);
                Destroy(gameObject, 3);
            }
        }
    }
}

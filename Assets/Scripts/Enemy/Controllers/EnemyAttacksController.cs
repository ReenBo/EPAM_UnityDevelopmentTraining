using ET.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Enemy
{
    public class EnemyAttacksController : MonoBehaviour
    {
        #region Variables
        private PlayerController _playerController = null;
        private Animator _animator = null;

        private float _damageArm = 0f;
        #endregion

        public float DamageArm { get => _damageArm; set => _damageArm = value; }

        #region Hash Code Animations
        private int _attack1 = Animator.StringToHash(AnimationsTags.ATTACK_1_TRIGGER);
        private int _attack2 = Animator.StringToHash(AnimationsTags.ATTACK_2_TRIGGER);
        #endregion

        protected void Awake()
        {
            _animator = GetComponentInParent<Animator>();
        }

        protected void OnTriggerEnter(Collider collider)
        {
            _playerController = collider.GetComponent<PlayerController>();

            if (_playerController != null)
            {
                _playerController.Damage(DamageArm);
            }
        }

        public void SwitchAttackEnemy(int num)
        {
            switch (num)
            {
                case 0:
                    _animator.SetTrigger(_attack1);
                    break;
                case 1:
                    _animator.SetTrigger(_attack2);
                    break;
                default:
                    break;
            }
        }
    }
}


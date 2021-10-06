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
        private float _damageArm = 0f;
        #endregion

        #region Properties
        public float DamageArm { get => _damageArm; set => _damageArm = value; }
        #endregion

        protected void OnTriggerEnter(Collider collider)
        {
            _playerController = collider.GetComponent<PlayerController>();

            if (_playerController != null)
            {
                _playerController.Damage(DamageArm);
            }
        }
    }
}


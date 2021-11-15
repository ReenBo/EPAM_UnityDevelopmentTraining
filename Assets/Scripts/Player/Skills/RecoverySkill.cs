using ET.Interface.IComand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.Skills
{
    public class RecoverySkill : MonoBehaviour, ICommand
    {
        private PlayerController _playerController = null;

        private float _healthTimeCounter = 120f;
        private float _maxHealth = 100f;
        private bool _resetIsAvailable = true;

        public Action<float, int> onDisplaySkill;
        public Action<float, float> onHealthViewChange;

        protected void Start()
        {
            _playerController = GetComponentInParent<PlayerController>();
        }

        public void ExecuteCommand()
        {
            if (_resetIsAvailable)
            {
                StartCoroutine(RestoreHealth(_playerController.CurrentHealth));
                StartCoroutine(EnableResetTimer(_healthTimeCounter));
                onDisplaySkill.Invoke(_healthTimeCounter, 1);
            }
        }

        private IEnumerator RestoreHealth(float amountHealth)
        {
            float cooldownTime = 10f;

            while (amountHealth < _maxHealth)
            {
                amountHealth += cooldownTime;
                _playerController.CurrentHealth = Mathf.Clamp(amountHealth, 0, _maxHealth);

                onHealthViewChange.Invoke(Mathf.Clamp(amountHealth, 0, _maxHealth), cooldownTime);

                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        private IEnumerator EnableResetTimer(float time)
        {
            while (time > 1e-3)
            {
                _resetIsAvailable = false;

                time -= 1f;
                yield return new WaitForSeconds(1f);
            }

            _resetIsAvailable = true;
            yield return null;
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.Skills
{
    public class PlayerSkillsController : MonoBehaviour
    {
        private PlayerController _playerController = null;

        private KeyCode[] _keyCodes;

        private float _acceptableLevelOfHealthRecovery = 0f;
        private float _cooldownRecoverySkill = 120f;

        private float _maxHealth = 100f;
        private bool _cooldownHealth = true;

        private float _maxArmor = 50f;
        private bool _cooldownArmor = true;

        private bool _isResetTime = true;

        protected void Start()
        {
            _playerController = GetComponent<PlayerController>();

            _keyCodes = new KeyCode[]
            {
                KeyCode.None,
                KeyCode.Q,
                KeyCode.E,
            };
        }

        private void Update()
        {
            ApplySkills();
        }

        private void ApplySkills()
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (Input.GetKey(_keyCodes[i]) && _isResetTime)
                {
                    RestoreHealth(i);
                    GameManager.Instance.PlayerSkillsView.DisplaySkills(_cooldownRecoverySkill, i);
                    StartCoroutine(ResetTime(_cooldownRecoverySkill));
                }
            }
        }

        private void RestoreHealth(int indexSkills)
        {
            if(indexSkills == 1 && _cooldownHealth)
            {
                _acceptableLevelOfHealthRecovery = _playerController.CurrentHealth;
               StartCoroutine(SetRecovery(_acceptableLevelOfHealthRecovery));
               _cooldownHealth = false;
            }
        }

        private IEnumerator SetRecovery(float amountHealth)
        {
            float cooldownTime = 1f;

            while (amountHealth < _maxHealth)
            {
                amountHealth += cooldownTime;
                GameManager.Instance.PlayerStatsViem.SetHealthView(amountHealth, cooldownTime);
                _playerController.CurrentHealth = amountHealth;
                yield return new WaitForSeconds(0.5f);
            }
            _cooldownHealth = true;
            _acceptableLevelOfHealthRecovery = 0f;

            yield return null;
        }

        private IEnumerator ResetTime(float time)
        {
            while (time > 0)
            {
                _isResetTime = false;

                time -= 1f;
                yield return new WaitForSeconds(1f);
            }

            _isResetTime = true;
            yield return null;
        }
    }
}

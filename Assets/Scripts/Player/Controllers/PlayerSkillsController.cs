using ET.Interface.IComand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.Skills
{
    public class PlayerSkillsController : MonoBehaviour, ICommand
    {
        private PlayerController _playerController = null;

        [SerializeField] private RecoverySkill _recoverySkill;

        private KeyCode[] _keyCodes;

        private float _healthTimeCounter = 120f;

        private float _maxHealth = 100f;
        private bool _healthIsRestored = true;

        private float _maxArmor = 50f;
        private bool _armorIsRestored = true;

        private bool _resetIsAvailable = true;

        public RecoverySkill RecoverySkill { get => _recoverySkill; }

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
                if (Input.GetKey(_keyCodes[i]) && _resetIsAvailable)
                {
                    ActivateSkill(i);
                    GameManager.Instance.PlayerSkillsView.DisplaySkills(_healthTimeCounter, i);
                    StartCoroutine(EnableResetTimer(_healthTimeCounter));
                }
            }
        }

        private void ActivateSkill(int indexSkills)
        {
            if(indexSkills == 1 && _healthIsRestored)
            {
               StartCoroutine(RestoreHealth(_playerController.CurrentHealth));
               _healthIsRestored = false;
            }
        }
        private IEnumerator RestoreHealth(float amountHealth)
        {
            float cooldownTime = 10f;

            while (amountHealth < _maxHealth)
            {
                amountHealth += cooldownTime;
                _playerController.CurrentHealth = Mathf.Clamp(amountHealth, 0, _maxHealth);

                GameManager.Instance.PlayerStatsViem.SetHealthView(
                    Mathf.Clamp(amountHealth, 0, _maxHealth), cooldownTime);

                yield return new WaitForSeconds(0.5f);
            }
            _healthIsRestored = true;

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

        //-------------------

        public void ExecuteCommand()
        {
            print("Active command 1");
        }
    }
}

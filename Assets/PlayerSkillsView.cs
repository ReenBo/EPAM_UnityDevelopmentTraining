using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.UI.SkillsView
{
    public class PlayerSkillsView : MonoBehaviour
    {
        [Header("References to the UI objects")]
        private Dictionary<int, Transform> _skillsPosition;

        [SerializeField] private Transform _recoverySkillPos;
        [SerializeField] private Transform _protectionSkillPos;
        [SerializeField] private Transform _annihilationSkillPos;
        [SerializeField] private Transform _bleedingSkillPos;

        [Header("References to the UI objects")]
        [SerializeField] private Image _cooldownHighground;
        [SerializeField] private Text _cooldownTimer;

        protected void Start()
        {
            _skillsPosition = new Dictionary<int, Transform>
            {
                {1,  _recoverySkillPos},
                {2,  _protectionSkillPos},
                {3,  _annihilationSkillPos},
                {4,  _bleedingSkillPos}
            };
        }

        private void SetPositionCooldownObjects(int skillKey)
        {
            foreach (var skillsPos in _skillsPosition)
            {
                if (skillsPos.Key == skillKey)
                {
                    _cooldownHighground.transform.localPosition = skillsPos.Value.localPosition;
                }
            }
        }

        public void DisplaySkills(float cooldownTime, int skillIndex)
        {
            SetPositionCooldownObjects(skillIndex);

            _cooldownHighground.gameObject.SetActive(true);

            StartCoroutine(Timer(cooldownTime));
        }

        private IEnumerator Timer(float time)
        {
            float timeStep = time;

            while (time > 0)
            {
                time -= 1f;

                _cooldownTimer.text = $"{((int)time)}";
                float num = _cooldownHighground.fillAmount / timeStep;
                _cooldownHighground.fillAmount -= num;

                yield return new WaitForSeconds(1f);
            }

            _cooldownHighground.gameObject.SetActive(false);
            _cooldownHighground.fillAmount = 1f;

            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Player.UI.ExperienceView
{
    public class PlayerExperienceView : MonoBehaviour
    {
        [Header("Links to ExperienceView")]
        [SerializeField] private Image _imageExp;
        [SerializeField] private Text _textExp;
        [SerializeField] private Text _textLevel;

        public void SetExperience(float exp, float currentExp, int maxExp, int level)
        {
            _textExp.text = $"XP {currentExp} / {maxExp}";
            _textLevel.text = $"LEVEL {level}";

            if (_imageExp.fillAmount >= 1f)
            {
                _imageExp.fillAmount = 0f;
            }

            _imageExp.fillAmount += exp / 100f;
        }
    }
}

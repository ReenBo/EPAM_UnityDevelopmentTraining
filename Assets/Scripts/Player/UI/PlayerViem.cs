using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Player
{
    public class PlayerViem : MonoBehaviour
    {
        [Header("Links to ExperienceView")]
        [SerializeField] private Image _imageExp;
        [SerializeField] private Text _textExp;
        [SerializeField] private Text _textLevel;

        [Header("Links to HealthView")]
        [SerializeField] private Image _imageHealth;
        [SerializeField] private Text _textHealth;

        [Header("Links to ArmorView")]
        [SerializeField] private Image _imageArmor;
        [SerializeField] private Text _textArmor;

        [Header("Links to AmmoView")]
        [SerializeField] private Image _imageAmmo;
        [SerializeField] private Text _textAmmo;

        private int _numberBulletsInOneMagazine = 30;

        public void SetExpView(float exp, float currentExp, int maxExp, int level)
        {
            _textExp.text = $"XP {currentExp} / {maxExp}";
            _textLevel.text = $"LEVEL {level}";

            if(_imageExp.fillAmount >= 1f)
            {
                _imageExp.fillAmount = 0f;
            }

            _imageExp.fillAmount += exp / 100f;
        }

        public void SetAmmoCountViem(int amount)
        {
            _textAmmo.text = $"30/{amount}";
            _imageAmmo.fillAmount = (float)amount / _numberBulletsInOneMagazine;
        }

        public void SetArmorView(float damage, int armor)
        {
            _textArmor.text = $"50/{armor}";
            _imageArmor.fillAmount -= damage / 50f;
        }

        public void SetHealthView(float damage, int health)
        {
            _textHealth.text = $"100/{health}";
            _imageHealth.fillAmount -= damage / 100f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Player.UI.StatsView
{
    public class PlayerStatsView : MonoBehaviour
    {
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

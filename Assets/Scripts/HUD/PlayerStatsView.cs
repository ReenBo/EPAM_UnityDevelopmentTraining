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

        public void SetAmmoCountViem(int amountBulletsInMagazine, int currentAmountBullets)
        {
            _textAmmo.text = $"{amountBulletsInMagazine}/{currentAmountBullets}";
            _imageAmmo.fillAmount = (float)currentAmountBullets / amountBulletsInMagazine;
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

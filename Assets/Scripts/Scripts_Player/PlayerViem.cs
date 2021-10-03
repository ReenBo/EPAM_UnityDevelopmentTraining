using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class PlayerViem : MonoBehaviour
    {
        private Image _playerHealthViem;
        private Text _playerAmmoViem;


        protected void Awake()
        {
            _playerHealthViem = GameObject.FindGameObjectWithTag(
                Tags.PLAYER_HEALTH_VIEW).GetComponent<Image>();

            _playerAmmoViem = GameObject.FindGameObjectWithTag(
                Tags.PLAYER_AMMO_VIEW).GetComponent<Text>();
        }

        public void SetAmmoCountViem(int amount)
        {
            _playerAmmoViem.text = "AMMO: " + amount.ToString();
        }

        public void SetHealthViem(float amount)
        {
            amount /= 100f;
            if (amount < 0f) amount = 0f;

            _playerHealthViem.fillAmount -= amount;
        }
    }
}

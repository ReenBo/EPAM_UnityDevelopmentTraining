using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class PlayerViem : MonoBehaviour
    {
        private Image _playerHealthViem;
        private Text _playerAmmoViemText;
        private Image _playerAmmoViemImage;

        protected void Awake()
        {
            _playerHealthViem = GameObject.FindGameObjectWithTag(
                Tags.PLAYER_HEALTH_VIEW).GetComponent<Image>();

            _playerAmmoViemText = GameObject.FindGameObjectWithTag(
                Tags.PLAYER_AMMO_TEXT_VIEW).GetComponent<Text>();

            //_playerAmmoViemImage = GameObject.FindGameObjectWithTag(
            //    Tags.PLAYER_AMMO_IMAGE_VIEW).GetComponent<Image>();
        }

        //public void SetAmmoViem(float amount)
        //{
        //    amount /= 1000f;
        //    if (amount < 0f) amount = 0f;

        //    _playerAmmoViemImage.fillAmount -= amount * 2;
        //}

        public void SetAmmoCountViem(int amount)
        {
            _playerAmmoViemText.text = "30/" + amount.ToString();
        }

        public void SetHealthViem(float amount)
        {
            amount /= 100f;
            if (amount < 0f) amount = 0f;

            _playerHealthViem.fillAmount -= amount;
        }
    }
}

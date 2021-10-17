using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Player
{
    public class PlayerViem : MonoBehaviour
    {
        [SerializeField] private Image _imageExp;
        [SerializeField] private Text _textExp;
        [SerializeField] private Text _textLevel;

        private Image _playerHealthViem;
        private Text _playerAmmoViemText;
        //private Image _playerAmmoViemImage;

        protected void Awake()
        {
            _playerHealthViem = GameObject.FindGameObjectWithTag(
                Tags.PLAYER_HEALTH_VIEW).GetComponent<Image>();

            _playerAmmoViemText = GameObject.FindGameObjectWithTag(
                Tags.PLAYER_AMMO_TEXT_VIEW).GetComponent<Text>();
        }

        public void SetExp(float exp, float currentExp, int maxExp, int level)
        {
            _textExp.text = $"XP {currentExp} / {maxExp}";
            _textLevel.text = $"LEVEL {level}";

            exp /= 100f;

            if(_imageExp.fillAmount >= 1f)
            {
                _imageExp.fillAmount = 0f;
            }

            _imageExp.fillAmount += exp;
        }

        public void SetAmmoCountViem(int amount)
        {
            _playerAmmoViemText.text = ($"30 / {amount}");
        }

        public void SetHealthViem(float amount)
        {
            amount /= 100f;
            if (amount < 0f) amount = 0f;

            _playerHealthViem.fillAmount -= amount;
        }
    }
}

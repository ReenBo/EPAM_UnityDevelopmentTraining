using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.UI.WeaponView
{
    public class WeaponView : MonoBehaviour
    {
        [Header("References to the UI objects")]
        [SerializeField] private Text _rateOfFire;
        [SerializeField] private Text _nameWeapon;
        [SerializeField] private List<Image> _iconWeapons;

        public void DisplayWeapon(float rateOfFire, string nameWeapon, int numberWeapon)
        {
            _rateOfFire.text = $"Rate of fire: {rateOfFire}";
            _nameWeapon.text = $"Model: {nameWeapon}";

            foreach (var icon in _iconWeapons)
            {
                icon.gameObject.SetActive(false);
            }

            _iconWeapons[numberWeapon].gameObject.SetActive(true);
        }
    }
}

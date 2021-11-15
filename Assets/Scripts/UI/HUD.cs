using ET.Interface.UI;
using ET.Player.Combat;
using ET.Player.UI.ExperienceView;
using ET.Player.UI.StatsView;
using ET.UI.SkillsView;
using ET.UI.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI.HUD
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private PlayerExperienceView _playerExperienceView;
        [SerializeField] private PlayerSkillsView _playerSkillsView;
        [SerializeField] private WeaponView _weaponView;

        //private List<Action> _listActions = new List<Action>()
        //{
        //    { GameManager.Instance.PlayerCombatController.onPlayerStatsViewChange },
        //};

        public PlayerStatsView PlayerStatsView { get => _playerStatsView; set => _playerStatsView = value; }
        public PlayerExperienceView PlayerExperienceView { get => _playerExperienceView; set => _playerExperienceView = value; }
        public PlayerSkillsView PlayerSkillsView { get => _playerSkillsView; set => _playerSkillsView = value; }
        public WeaponView WeaponView { get => _weaponView; set => _weaponView = value; }

        public void InvolveDisplay(bool isLaunched)
        {
            gameObject.SetActive(isLaunched);
        }

        public void ReceiveStatusOfSubscribersHandler(bool status)
        {
            if (status)
            {
                GameManager.Instance.PlayerCombatController.onPlayerStatsViewChange +=
                    PlayerStatsView.SetAmmoCountViem;

                GameManager.Instance.PlayerController.onArmorViewChange += PlayerStatsView.SetArmorView;
                GameManager.Instance.PlayerController.onHealthViewChange += PlayerStatsView.SetHealthView;

                GameManager.Instance.LevelSystem.onExperiencePlayerChange += 
                    PlayerExperienceView.SetExperience;

                GameManager.Instance.WeaponsController.onAmmoCountViemChange +=
                    PlayerStatsView.SetAmmoCountViem;

                GameManager.Instance.PlayerCombatController.onWeaponViewChange +=
                    WeaponView.DisplayWeapon;

                GameManager.Instance.RecoverySkill.onDisplaySkill += PlayerSkillsView.DisplaySkills;

                GameManager.Instance.RecoverySkill.onHealthViewChange += PlayerStatsView.SetHealthView;

            }
            else
            {
                GameManager.Instance.PlayerCombatController.onPlayerStatsViewChange -=
                    PlayerStatsView.SetAmmoCountViem;

                GameManager.Instance.PlayerController.onArmorViewChange -= PlayerStatsView.SetArmorView;
                GameManager.Instance.PlayerController.onHealthViewChange -= PlayerStatsView.SetHealthView;

                GameManager.Instance.LevelSystem.onExperiencePlayerChange -=
                    PlayerExperienceView.SetExperience;

                GameManager.Instance.WeaponsController.onAmmoCountViemChange -=
                    PlayerStatsView.SetAmmoCountViem;

                GameManager.Instance.PlayerCombatController.onWeaponViewChange -=
                    WeaponView.DisplayWeapon;

                GameManager.Instance.RecoverySkill.onDisplaySkill -= PlayerSkillsView.DisplaySkills;

                GameManager.Instance.RecoverySkill.onHealthViewChange -= PlayerStatsView.SetHealthView;
            }
        }
    }
}

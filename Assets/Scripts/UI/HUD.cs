using ET.Interface.UI;
using ET.Player.UI.ExperienceView;
using ET.Player.UI.StatsView;
using ET.UI.SkillsView;
using ET.UI.Weapon;
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

        public PlayerStatsView PlayerStatsView { get => _playerStatsView; set => _playerStatsView = value; }
        public PlayerExperienceView PlayerExperienceView { get => _playerExperienceView; set => _playerExperienceView = value; }
        public PlayerSkillsView PlayerSkillsView { get => _playerSkillsView; set => _playerSkillsView = value; }

        public void InvolveDisplay(bool isLaunched)
        {
            gameObject.SetActive(isLaunched);
        }
    }
}

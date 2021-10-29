using System.Collections.Generic;
using UnityEngine;
using ET;
using ET.Player;
using ET.Weapons;
using ET.Core.LevelSystem;

namespace ET.Core.Stats
{
    [System.Serializable]
    public class CharacterStats
    {
        private float _health;
        private float _armor;
        private int _amountCartridges;

        private int _level;
        private float _experience;

        public float[] PositionPlayer = new float[3];

        public CharacterStats(PlayerController player, LevelSystem.LevelSystem progress)
        {
            _health = player.CurrentHealth;
            _armor = player.CurrentArmor;
            //_amountCartridges = weapon.AmmoCounter;

            _level = progress.CurrentLevel;
            _experience = progress.CurrentExperience;

            PositionPlayer[0] = player.transform.position.x;
            PositionPlayer[1] = player.transform.position.y;
            PositionPlayer[2] = player.transform.position.z;
        }

        public float Health { get => _health; set => _health = value; }
        public float Armor { get => _armor; set => _armor = value; }
        //public int AmountCartridges { get => _amountCartridges; set => _amountCartridges = value; }
        public int Level { get => _level; set => _level = value; }
        public float Experience { get => _experience; set => _experience = value; }

        //private Dictionary<string, float> _statsContainer = null;

        //public void CharacterStatsInitialize(float Health, float Armor, int Level)
        //{
        //    _health = Health;
        //    _armor = Armor;
        //    _level = Level;

        //    Filling();
        //}

        //private void Filling()
        //{
        //    _statsContainer?.Clear();

        //    _statsContainer = new Dictionary<string, float>(3)
        //    {
        //        { "Health", _health },
        //        { "Armor", _armor },
        //        { "Level", _level }
        //    };
        //}

        //private float GetValueContainer(string key)
        //{
        //    float value = _statsContainer[key];

        //    return value;
        //}

        //public void SetValueContainer(string key, float value)
        //{
        //    foreach (var item in _statsContainer)
        //    {
        //        if(item.Key == key)
        //        {
        //            _statsContainer[key] = value;
        //        }
        //    }
        //}
    }
}

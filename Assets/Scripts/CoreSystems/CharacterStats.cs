using System.Collections.Generic;
using UnityEngine;
using ET.Player;

namespace ET.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public struct Stats
        {
            public int _currentLevel;
            public float _currentExperience;

            public float currentHealth;
            public float currentArmor;

            public float currentAmountCartridges;
        }

        /// <summary>
        /// ƒŒ¡¿¬‹ ¬—≈ —“¿“€. Õ¿◊Õ» — —Œ’–¿Õ≈Õ»ﬂ ¬ —“–” “”–”, ≈® —≈–»¿À»«¿÷»≈… ¬ ¡»Õ¿– ”
        /// </summary>

        private Dictionary<string, float> _statsContainer = null;

        private float _health;
        private float _armor;
        private float _level;

        public float Health { get => _health; }
        public float Armor { get => _armor; }
        public float Level { get => _level; }

        protected void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void CharacterStatsInitialize(float Health, float Armor, float Level)
        {
            _health = Health;
            _armor = Armor;
            _level = Level;

            Filling();
        }

        private void Filling()
        {
            _statsContainer?.Clear();

            _statsContainer = new Dictionary<string, float>(3)
            {
                { "Health", _health },
                { "Armor", _armor },
                { "Level", _level }
            };
        }

        private float GetValueContainer(string key)
        {
            float value = _statsContainer[key];

            return value;
        }

        public void SetValueContainer(string key, float value)
        {
            foreach (var item in _statsContainer)
            {
                if(item.Key == key)
                {
                    _statsContainer[key] = value;
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class HealthSystem
    {
        private float _health = 0f;

        public HealthSystem(float health)
        {
            health = Mathf.Clamp(_health, 0, 100);
        }

        public float GetHealth()
        {
            return _health;
        }

        public void Heal(int healAmount)
        {
            _health += healAmount;
        }
    }
}

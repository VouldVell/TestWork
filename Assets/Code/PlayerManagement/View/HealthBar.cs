using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerManagement
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;

        public void Damage(int damage)
        {
            healthBar.value -= damage;
            
        }

        public void FullHP()
        {
            healthBar.value = 100;
        }
    }
}
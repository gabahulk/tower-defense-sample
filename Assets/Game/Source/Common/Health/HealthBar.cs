using System;
using UnityEngine;
using UnityEngine.UI;
namespace TowerDefence.Common.Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image bar = default;
        private Camera mainCamera;
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void SetHealthBarFillAmount(float fillAmount)
        {
            bar.fillAmount = fillAmount;
        }

        public void OnHealthChanged(float healthPercentage)
        {
            SetHealthBarFillAmount(healthPercentage);
        }

    }
}


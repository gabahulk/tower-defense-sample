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

        private void LateUpdate()
        {
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0);
        }

        public void OnHealthChanged(float healthPercentage)
        {
            SetHealthBarFillAmount(healthPercentage);
        }

    }
}


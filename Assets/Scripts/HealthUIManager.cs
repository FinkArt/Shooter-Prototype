using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerController1
{
    public class HealthUIManager : MonoBehaviour
    {
        [SerializeField] private Text _healthText;

        [SerializeField] private Image _healthBarImage;
        [SerializeField] private Button _damageButton;
        [SerializeField] private KeyCode _keyCode;

        private HealthController _healthController;

        private void Awake()
        {
            // HealthTest healthTest = gameObject.AddComponent<HealthTest>();
            // healthTest.TestHealth = 10;
            _healthController = new HealthController();
            UpdatePlayerOnHealth(_healthController.Health);
            _healthController.OnHealthChanged += UpdatePlayerOnHealth;
            _damageButton.onClick.AddListener(() => _healthController.ApplyDamage(10f));
            _someObj = GetComponent<HealthUIManager>();
        }

        private HealthUIManager _someObj;

        private void Update()
        {
            //HealthController healthController = new HealthController();

            if (Input.GetKeyDown(KeyCode.Space))
            {

                _someObj._damageButton.interactable = !_someObj._damageButton.interactable;
            }

            if (Input.GetKeyDown(_keyCode))
                _healthController.ApplyDamage(10f);
        }

        private void UpdatePlayerOnHealth(float finalHealth)
        {
            if (finalHealth >= 0)
            {
                _healthText.text = $"{finalHealth.ToString()} / 100";
                _healthBarImage.fillAmount = finalHealth / 100;
            }
        }
    }

    public class HealthController
    {
        public float Health = 100f;
        public event Action<float> OnHealthChanged;

        public void ApplyDamage(float damage)
        {
            Health -= damage;
            OnHealthChanged?.Invoke(Health);
        }

    }

}


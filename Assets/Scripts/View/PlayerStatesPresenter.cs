using System;
using Core;
using Gameplay.PlayerStates;
using TMPro;
using UnityEngine;

namespace View
{
    public class PlayerStatesPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthValueText;
        [SerializeField] private TextMeshProUGUI treasureValueText;
        [SerializeField] private TextMeshProUGUI weaponValueText;
        [SerializeField] private TextMeshProUGUI bottleValueText;
        [SerializeField] private TextMeshProUGUI keysValueText;

        private void Start()
        {
            var playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
            playerStatesCapability.OnHealthChange += OnHealthChange;
            playerStatesCapability.OnTreasureChange += OnTreasureChange;
            playerStatesCapability.OnWeaponChange += OnWeaponChange;
            playerStatesCapability.OnBottleChange += OnBottleChange;
            playerStatesCapability.OnKeysChange += OnKeysChange;
        }

        private void OnDestroy()
        {
            var playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
            playerStatesCapability.OnHealthChange -= OnHealthChange;
            playerStatesCapability.OnTreasureChange -= OnTreasureChange;
            playerStatesCapability.OnWeaponChange -= OnWeaponChange;
            playerStatesCapability.OnBottleChange -= OnBottleChange;
            playerStatesCapability.OnKeysChange -= OnKeysChange;
        }

        private void OnHealthChange(int value)
        {
            healthValueText.text = value.ToString();
        }
        
        private void OnTreasureChange(int value)
        {
            treasureValueText.text = value.ToString();
        }
        
        private void OnWeaponChange(int value)
        {
            weaponValueText.text = value.ToString();
        }
        
        private void OnBottleChange(int value)
        {
            bottleValueText.text = value.ToString();
        }
        
        private void OnKeysChange(int value)
        {
            keysValueText.text = value.ToString();
        }
    }
}
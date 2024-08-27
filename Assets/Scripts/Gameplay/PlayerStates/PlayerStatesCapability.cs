using System;
using Core;

namespace Gameplay.PlayerStates
{
    public class PlayerStatesCapability : Capability
    {
        public Action<int> OnHealthChange;
        public Action<int> OnTreasureChange;
        public Action<int> OnWeaponChange;
        public Action<int> OnBottleChange;
        public Action<int> OnKeysChange;
        
        private int _health;
        private int _treasure;
        private int _weapon;
        private int _bottle;
        private int _keys;

        public int Health
        {
            get => _health;
            internal set
            {
                if (value >= 0) OnHealthChange?.Invoke(_health = value);
            }
        }

        public int Treasure
        {
            get => _treasure;
            set
            {
                if (value >= 0) OnTreasureChange?.Invoke(_treasure = value);
            }
        }

        public int Weapon
        {
            get => _weapon;
            internal set
            {
                if (value >= 0) OnWeaponChange?.Invoke(_weapon = value);
            }
        }

        public int Bottle
        {
            get => _bottle;
            internal set
            {
                if (value >= 0) OnBottleChange?.Invoke(_bottle = value);
            }
        }

        public int Keys
        {
            get => _keys;
            internal set
            {
                if (value >= 0) OnKeysChange?.Invoke(_keys = value);
            }
        }

        public override void Update()
        {
        }
    }
}
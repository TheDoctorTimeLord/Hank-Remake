using Core;
using Gameplay.PlayerStates;
using Gameplay.PlayerStates.Events;
using UnityEngine;

namespace Mechanics.ScriptableObjects.DamageStrategy
{
    [CreateAssetMenu(fileName = "PlayerDamageStrategy", menuName = "Damaging/PlayerDamageStrategy")]
    public class PlayerDamageStrategySO : DamageStrategySO
    {
        private PlayerStatesCapability _playerStatesCapability;

        public override void Initialize()
        {
            _playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
        }

        public override bool Damage(int damagePoints)
        {
            var ev = Simulation.Schedule<ChangeHealthEvent>();
            ev.ChangeHealth = -damagePoints;

            return _playerStatesCapability.Health - damagePoints <= 0;
        }
    }
}
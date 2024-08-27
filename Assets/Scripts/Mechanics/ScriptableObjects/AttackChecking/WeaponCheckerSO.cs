using Core;
using Gameplay.PlayerStates;
using Gameplay.PlayerStates.Events;
using UnityEngine;

namespace Mechanics.ScriptableObjects.AttackChecking
{
    [CreateAssetMenu(fileName = "WeaponChecker", menuName = "AttackChecker/WeaponChecker")]
    public class WeaponCheckerSO : AttackCheckerSO
    {
        private PlayerStatesCapability _playerStatesCapability;
        
        public override void Initialize()
        {
            _playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
        }

        public override bool CanAttack()
        {
            if (_playerStatesCapability.Weapon <= 0) return false;

            Simulation.Schedule<ChangeWeaponEvent>().ChangeWeapon = -1;
            return true;
        }
    }
}
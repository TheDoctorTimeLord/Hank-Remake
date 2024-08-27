using System.Collections;
using Core;
using Gameplay.Damaging;
using UnityEngine;

namespace Mechanics
{
    public class Temporary : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy;

        private void Start()
        {
            StartCoroutine(SetTimeToDestroy());
        }

        private IEnumerator SetTimeToDestroy()
        {
            yield return new WaitForSeconds(timeToDestroy);
            var ev = Simulation.Schedule<KillEvent>();
            ev.Killed = gameObject;
        }
    }
}
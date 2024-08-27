using UnityEngine;

namespace Gameplay.Level
{
    public interface IIdResolver
    {
        public int Resolve(GameObject gameObject);
        public GameObject Resolve(int id);
    }
}
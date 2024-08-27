using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(PlacedInGameField))]
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject spawnable;
        [SerializeField] private bool spawnOnes;
        
        private GameObject _spawnedEntity;

        private void Start()
        {
            SpawnEntity();
        }

        private void Update()
        {
            if (spawnOnes) return;
            
            SpawnEntity();
        }

        private void SpawnEntity()
        {
            _spawnedEntity = Instantiate(spawnable);
            _spawnedEntity.transform.position = transform.position;

            var placedEntity = _spawnedEntity.GetComponent<PlacedInGameField>();
            if (placedEntity) placedEntity.useAutomaticStartPosition = true;
        }
    }
}
using System;
using System.Collections.Generic;
using Core.Utils;
using UnityEngine;

namespace Gameplay.Level
{
    public class EntityIdentifiersStorage : IIdResolver
    {
        private const int MaxLoopTicks = 100;
        private readonly Dictionary<int, GameObject> _entitiesById = new();
        private readonly Dictionary<GameObject, int> _idByEntities = new();

        public int RegisterObject(GameObject registeringObject)
        {
            var loopCount = 0;
            int generatedId;
            do
            {
                generatedId = IdUtils.GenerateIdInt<GameObject>();
                if (++loopCount >= MaxLoopTicks) 
                    throw new ArithmeticException($"Too many try to generate Id (> {MaxLoopTicks}). Generated count of id already = {_entitiesById.Count}");
            } while (_entitiesById.ContainsKey(generatedId));
            
            _entitiesById[generatedId] = registeringObject;
            _idByEntities[registeringObject] = generatedId;
            return generatedId;
        }

        public int Resolve(GameObject gameObject)
        {
            if (!_idByEntities.TryGetValue(gameObject, out var resolved))
            {
                throw new ArgumentException($"Entity [{gameObject}] was not registered");
            }
            return resolved;
        }

        public GameObject Resolve(int id)
        {
            if (!_entitiesById.TryGetValue(id, out var resolved))
            {
                throw new ArgumentException($"Unknown id [{id}]");
            }
            return resolved;
        }

        public int RemoveObject(GameObject removing)
        {
            var id = Resolve(removing);

            _entitiesById.Remove(id);
            _idByEntities.Remove(removing);

            return id;
        }
    }
}
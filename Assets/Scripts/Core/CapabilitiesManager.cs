using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Events;
using JetBrains.Annotations;

namespace Core
{
    public class CapabilitiesManager
    {
        private readonly Dictionary<Type, Capability> _capabilities = new();
        
        public CapabilitiesManager RegisterCapability(Capability capability)
        {
            if (!_capabilities.TryAdd(capability.GetType(), capability))
            {
                throw new ArgumentException($"Capability [{capability}] was registered already!");
            }
            return this;
        }

        public CapabilitiesManager RegisterCapabilityIfAbsent(Capability capability)
        {
            _capabilities.TryAdd(capability.GetType(), capability);
            return this;
        }

        public void UnregisterCapability(Capability capability)
        {
            _capabilities.Remove(capability.GetType());
        }

        [CanBeNull]
        public T Get<T>() where T : Capability
        {
            return (T)_capabilities.GetValueOrDefault(typeof(T), null);
        }

        public bool Has<T>() where T : Capability
        {
            return _capabilities.ContainsKey(typeof(T));
        }

        public void UpdateAllCapabilities()
        {
            foreach (var (_, capability) in _capabilities)
            {
                capability.Update();
            }
        }
    }
    
    public abstract class Capability
    {
        public abstract void Update();
    }
}
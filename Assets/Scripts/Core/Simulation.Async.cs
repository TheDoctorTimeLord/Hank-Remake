using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static partial class Simulation
    {
        public class AsyncModule
        {
            private ICoroutineRunner _coroutineRunner;
            private readonly List<QueueItem> _coroutineQueue = new();
            
            public CoroutinePlaceholder StartCoroutine(IEnumerator routine)
            {
                var placeholder = new CoroutinePlaceholder();
                if (_coroutineRunner == null)
                {
                    _coroutineQueue.Add(new QueueItem(placeholder, routine));
                }
                else
                {
                    placeholder.Coroutine = _coroutineRunner.StartCoroutine(routine);
                }

                return placeholder;
            }

            public void StopCoroutine(IEnumerator routine)
            {
                if (_coroutineRunner == null)
                {
                    _coroutineQueue.Remove(new QueueItem(null, routine));
                }
                else
                {
                    _coroutineRunner.StopCoroutine(routine);
                }
            }

            public void StopCoroutine(Coroutine routine)
            {
                if (_coroutineRunner == null) 
                    throw new NotSupportedException($"Lazy stop of {nameof(Coroutine)} is not supported. CoroutineRunner" +
                                                    $" is null. Use {nameof(StopCoroutine)}(IEnumerator routine)");
                _coroutineRunner.StopCoroutine(routine);
            }

            internal void SetCoroutineRunner(ICoroutineRunner coroutineRunner)
            {
                _coroutineRunner = coroutineRunner;
                if (_coroutineQueue.Count == 0) return;
                
                foreach (var item in _coroutineQueue)
                {
                    item.Placeholder.Coroutine = _coroutineRunner.StartCoroutine(item.Enumerator);
                }
            }
        }
        
        public interface ICoroutineRunner
        {
            public Coroutine StartCoroutine(IEnumerator routine);

            public void StopCoroutine(IEnumerator routine);
            public void StopCoroutine(Coroutine routine);
        }
        
        public class CoroutinePlaceholder
        {
            public Coroutine Coroutine { get; internal set; }
            public bool HasCoroutine => Coroutine == null;
        }
        
        public readonly struct QueueItem
        {
            public readonly CoroutinePlaceholder Placeholder;
            public readonly IEnumerator Enumerator;

            public QueueItem(CoroutinePlaceholder placeholder, IEnumerator enumerator)
            {
                Placeholder = placeholder;
                Enumerator = enumerator;
            }

            public override bool Equals(object obj)
            {
                return obj is QueueItem item && Equals(item);
            }

            public bool Equals(QueueItem other)
            {
                return Equals(Enumerator, other.Enumerator);
            }

            public override int GetHashCode()
            {
                return (Enumerator != null ? Enumerator.GetHashCode() : 0);
            }
        }
    }
}
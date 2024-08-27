using System;
using System.Collections.Generic;
using Mechanics;
using Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    /// <summary>
    /// The Simulation class implements the discrete event simulator pattern.
    /// Events are pooled, with a default capacity of 4 instances.
    /// </summary>
    public static partial class Simulation
    {
        public const float MinimalDelay = .01f;

        private static readonly HeapQueue<Event> EventQueue = new();
        private static readonly Dictionary<Type, Stack<Event>> EventPools = new();
        public static readonly AsyncModule Async = new();

        public static void Initialize(Object gameController)
        {
            if (gameController is ICoroutineRunner runner)
                Async.SetCoroutineRunner(runner);
            else
                Debug.LogWarning("Game Controller is not ICoroutineRunner");
        }

        /// <summary>
        /// Create a new event of type T and return it, but do not schedule it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T New<T>() where T : Event, new()
        {
            if (!EventPools.TryGetValue(typeof(T), out var pool))
            {
                pool = new Stack<Event>(4);
                pool.Push(new T());
                EventPools[typeof(T)] = pool;
            }

            if (pool.Count > 0)
                return (T)pool.Pop();
            return new T();
        }

        /// <summary>
        /// Clear all pending events and reset the tick to 0.
        /// </summary>
        public static void Clear()
        {
            EventQueue.Clear();
        }

        public static void ScheduleBatch(float initialDelay, params Event[] events)
        {
            var startDelay = Time.time + initialDelay;

            for (var i = 0; i < events.Length; i++)
            {
                var ev = events[i];
                ev.tick = startDelay + i * MinimalDelay;
                EventQueue.Push(ev);
            }
        }

        /// <summary>
        /// Schedule an event for a future tick, and return it. Order of execution events is not guarantied.
        /// If you want to execute events in any order then use <see cref="ScheduleBatch(float, Event[])"/>
        /// </summary>
        /// <returns>The scheduling event.</returns>
        /// <param name="tick">Seconds to execute event.</param>
        /// <typeparam name="T">The event type parameter.</typeparam>
        public static T Schedule<T>(float tick = 0) where T : Event, new()
        {
            var ev = New<T>();
            ev.tick = Time.time + tick;
            EventQueue.Push(ev);
            return ev;
        }

        /// <summary>
        /// Reschedule an existing event for a future tick, and return it.
        /// </summary>
        /// <returns>The event.</returns>
        /// <param name="gameEvent">Game scheduling event</param>
        /// <param name="tick">Seconds to execute event.</param>
        /// <typeparam name="T">The event type parameter.</typeparam>
        public static T Reschedule<T>(T gameEvent, float tick) where T : Event, new()
        {
            gameEvent.tick = Time.time + tick;
            EventQueue.Push(gameEvent);
            return gameEvent;
        }

        /// <summary>
        /// Return the simulation model instance for a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetModel<T>() where T : class, new()
        {
            return InstanceRegister<T>.Instance;
        }

        /// <summary>
        /// Set a simulation model instance for a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void SetModel<T>(T instance) where T : class, new()
        {
            InstanceRegister<T>.Instance = instance;
        }

        public static T GetCapability<T>() where T : Capability
        {
            return GetModel<MainGameModel>().GetCapability<T>();
        }

        /// <summary>
        /// Destroy the simulation model instance for a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void DestroyModel<T>() where T : class, new()
        {
            InstanceRegister<T>.Instance = null;
        }

        public static int Tick()
        {
            var time = Time.time;
            while (EventQueue.Count > 0 && EventQueue.Peek().tick <= time)
            {
                var ev = EventQueue.Pop();
                var tick = ev.tick;

                ev.ExecuteEvent();

                //event was rescheduled, so do not return it to the pool.
                if (ev.tick > tick) continue;

                ev.Cleanup();
                try
                {
                    EventPools[ev.GetType()].Push(ev);
                }
                catch (KeyNotFoundException)
                {
                    //This really should never happen inside a production build.
                    Debug.LogError($"No Pool for: {ev.GetType()}");
                }
            }

            return EventQueue.Count;
        }
    }
}
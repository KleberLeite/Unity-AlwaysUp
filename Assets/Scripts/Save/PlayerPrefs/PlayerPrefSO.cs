using System;
using UnityEngine;

namespace AlwaysUp.Save
{
    public abstract class PlayerPrefSO<T> : ScriptableObject, IPlayerPrefSO<T>
    {
        [Header("Settings")]
        [SerializeField] protected string _key;
        [SerializeField] protected T _defaultValue;

        public string Key { get => _key; }
        public T DefaultValue => _defaultValue;

        abstract public T Get();
        abstract public void Set(T value);

        abstract public void Reset();
    }
}

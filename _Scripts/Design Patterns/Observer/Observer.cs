using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace DesignPatterns
{
    /// <summary>
    /// A generic observer implemented using Unity Events.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Observer<T>
    {
        [SerializeField] T _value;
        [SerializeField] UnityEvent<T> _onValueChanged;

        public T Value { get { return _value; } set { Set(value); }}
        public static implicit operator T(Observer<T> observer) => observer._value; // Useful, but does have overhead so use sparingly.

        public Observer(T value, UnityAction<T> callback = null)
        {
            _value = value;
            _onValueChanged = new UnityEvent<T>();
            if (callback!= null) _onValueChanged.AddListener(callback);
        }

        public void Set(T value)
        {
            if (Equals(_value, value)) return;
            _value = value;
            Invoke();
        }

        public void Invoke()
        {
            Debug.Log($"Invoking {_onValueChanged.GetPersistentEventCount()} listeners");
            _onValueChanged.Invoke(_value);
        }

        public void AddListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (_onValueChanged == null) _onValueChanged = new UnityEvent<T>();

#if UNITY_EDITOR
            UnityEventTools.AddPersistentListener(_onValueChanged, callback);
#else
            _onValueChanged.AddListener(callback);
#endif
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (_onValueChanged == null) _onValueChanged = new UnityEvent<T>();

#if UNITY_EDITOR
            UnityEventTools.RemovePersistentListener(_onValueChanged, callback);
#else
            _onValueChanged.RemoveListener(callback);
#endif
        }

        public void RemoveAllListeners()
        {
            if (_onValueChanged == null) return;

#if UNITY_EDITOR
            // The field "m_PersistentCalls" in the class UnityEventBase 
            // is a private field, so use Reflection to retrieve it.
            FieldInfo fieldInfo = typeof(UnityEventBase).GetField("m_PersistentCalls", BindingFlags.Instance | BindingFlags.NonPublic);
            object value = fieldInfo.GetValue(_onValueChanged);
            value.GetType().GetMethod("Clear").Invoke(_value, null);
#else
            _onValueChanged.RemoveListener(callback);
#endif
        }

        public void Dispose()
        {
            RemoveAllListeners();
            _onValueChanged = null;
            _value = default;
        }
    }
}

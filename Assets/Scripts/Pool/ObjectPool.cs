using System.Collections.Generic;
using UnityEngine;

namespace AlwaysUp.Utils
{
    public class ObjectPool<T> : MonoBehaviour, IObjectPool<T> where T : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private T _prefab;
        [SerializeField][Min(0)] private int _count;

        private List<T> _unusedObjects = new List<T>();

        private void Awake()
        {
            for (int i = 0; i < _count; i++)
            {
                T @object = InstantiateObject();
                _unusedObjects.Add(@object);

                @object.gameObject.SetActive(false);
            }
        }

        private T InstantiateObject()
        {
            T @object = Instantiate(_prefab, transform);
            @object.gameObject.SetActive(false);

            return @object;
        }

        public T Get()
        {
            if (_unusedObjects.Count == 0)
            {
                T newObject = InstantiateObject();
                _unusedObjects.Add(newObject);

                return newObject;
            }

            T unusedObject = _unusedObjects[0];
            _unusedObjects.RemoveAt(0);

            return unusedObject;
        }

        public void GiveBack(T @object)
        {
            _unusedObjects.Add(@object);
            @object.gameObject.SetActive(false);
            @object.transform.parent = transform;
        }
    }
}

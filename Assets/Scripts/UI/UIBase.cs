using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    public class UIBase : MonoBehaviour
    {
        protected readonly Dictionary<Type, Object[]> Objects = new Dictionary<Type, Object[]>();

        protected void Bind<T>(Type type) where T : Object
        {
            string[] names = Enum.GetNames(type);
            Object[] objects = new Object[names.Length];
            Objects.Add(typeof(T), objects);

            for (int i = 0; i < names.Length; i++)
            {
                var go = FindInChildren(transform, names[i]);
                if (go != null)
                {
                    objects[i] = go.GetComponent<T>();
                }
                else
                {
                    Debug.LogError($"Could not find object {names[i]} for type {typeof(T).Name}");
                    objects[i] = null;
                }
            }
        }
        
        // Recursed Function
        Transform FindInChildren(Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name)
                    return child;
                var result = FindInChildren(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        protected T Get<T>(int idx) where T : Object
        {
            if (!Objects.TryGetValue(typeof(T), out var objects))
            {
                Debug.LogError($"Objects dictionary does not contain key of type {typeof(T).Name}");
                return null;
            }

            if (objects.Length <= idx)
            {
                Debug.LogError($"Index {idx} is out of range for objects array of type {typeof(T).Name}");
                return null;
            }

            if (objects[idx] == null)
            {
                Debug.LogError($"Object at index {idx} of type {typeof(T).Name} is null");
                return null;
            }
            
            return objects[idx] as T;
        }
    }
}
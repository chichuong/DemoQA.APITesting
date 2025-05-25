#nullable enable
using System.Collections.Generic;

namespace DemoQA.Core
{
    public static class DataStorage
    {
        private static readonly Dictionary<string, object> Store = new Dictionary<string, object>();

        public static void SetData(string key, object value)
        {
            Store[key] = value;
        }

        public static T? GetData<T>(string key)
        {
            if (Store.TryGetValue(key, out object? storedValue))
            {
                if (storedValue is T variable)
                {
                    return variable;
                }
            }
            return default;
        }

        public static bool ContainsKey(string key)
        {
            return Store.ContainsKey(key);
        }

        public static void ClearData(string key)
        {
            Store.Remove(key);
        }

        public static void ClearAll()
        {
            Store.Clear();
        }
    }
}
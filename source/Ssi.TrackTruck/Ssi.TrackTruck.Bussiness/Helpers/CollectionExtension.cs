using System.Collections.Generic;

namespace Ssi.TrackTruck.Bussiness.Helpers
{
    public static class CollectionExtension
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (IsNull(key) || !dictionary.ContainsKey(key))
            {
                return default(TValue);
            }
            return dictionary[key];
        }

        private static bool IsNull<TKey>(TKey key)
        {
            return !typeof(TKey).IsValueType && key == null;
        }
    }
}

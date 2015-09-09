using System.Collections.Generic;

namespace Ssi.TrackTruck.Bussiness.Helpers
{
    public static class CollectionExtension
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            dictionary.TryGetValue(key, out value);
            return value;
        }
    }
}

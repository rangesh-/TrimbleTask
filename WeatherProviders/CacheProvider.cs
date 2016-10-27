using System;
using System.Runtime.Caching;
using WeatherApp.Interfaces;

namespace WeatherProviders
{
    /// <summary>
    /// Default Meomery Cache Implementation with get and put items.
    /// </summary>
    public class CacheProvider : ICaching
    {
        private static ObjectCache _cache;
        public CacheProvider()
        {
            _cache = MemoryCache.Default;
        }

        public T Get<T>(string key) where T : class
        {
            return _cache.Get(key) as T;
        }
        public bool Put<T>(string key, object value, TimeSpan timeSpan)
        {
            var cacheitempolicy = new CacheItemPolicy();
            return PutItem(key, value, cacheitempolicy);

        }

        private bool PutItem(string key, object value, CacheItemPolicy policy)
        {
            bool retValue = false;

            if (_cache.Contains(key))
            {
                _cache.Remove(key);
            }
            if (!_cache.Contains(key))
            {
                _cache.Add(key, value, policy);
                retValue = true;
            }

            return retValue;
        }
    }
}

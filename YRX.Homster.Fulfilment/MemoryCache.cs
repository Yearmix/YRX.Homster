using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YRX.Homster.Fulfilment
{
    class MemoryCache<T>
    {
        private static ConcurrentDictionary<string, T> _cache = new ConcurrentDictionary<string, T>();

        private Func<T> Callback; 

        private readonly int _duration;

        public MemoryCache() : this(1000 * 60 * 5, null)
        {}            

        public MemoryCache(int duration, Func<T> callback = null)
        {
            Callback = callback;
            _duration = duration;
        }


        public T this[string key]
        {
            get {
                if (!_cache.ContainsKey(key))
                    return default(T);

                T value;
                var result = _cache.TryGetValue(key, out value);
                return result ? value : default(T);
            }
            set {
                var success = true;
                if (!_cache.ContainsKey(key))
                {
                    _cache[key] = value;
                }
                else {
                    success = _cache.TryAdd(key,value);
                }
                if (Callback != null && success)
                {
                    var item = _cache[key];
                    new Timer((s) => _cache[key] = Callback(), null, 0, _duration);
                }               
            }
        }  
    }
}

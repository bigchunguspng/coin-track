using System;
using System.Collections.Generic;

namespace CoinTrack.Services;

public class CacheService
{
    private Dictionary<string, ExpiringData> Cache { get; } = new();


    public void Register(string key, TimeSpan lifetime)
    {
        Cache[key] = new ExpiringData(lifetime);
    }

    public void Update(string key, object data)
    {
        Cache[key].Update(data);
    }

    public bool IsRelevant<T>(string key, out T data)
    {
        var relevant = Cache.TryGetValue(key, out var c) && c.IsRelevant();
        
        data = relevant ? (T)Cache[key].Data : default!;
        return relevant;
    }


    private class ExpiringData
    {
        public ExpiringData(TimeSpan lifetime)
        {
            Lifetime = lifetime;
        }

        public object Data { get; private set; } = null!;

        private TimeSpan Lifetime { get; }

        private DateTime Created { get; set; }


        public bool IsRelevant() => Created + Lifetime > DateTime.Now;

        public void Update(object data)
        {
            Data = data;
            Created = DateTime.Now;
        }
    }
}
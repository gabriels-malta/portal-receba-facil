using Microsoft.Extensions.Caching.Memory;
using RecebaFacil.Portal.Services.Interfaces;
using System;

namespace RecebaFacil.Portal.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T CriarOuObter<T>(string chave, T valor)
        {
            return _cache.GetOrCreate(chave, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(16);
                return valor;
            });
        }

        public T Obter<T>(string chave) => _cache.Get<T>(chave);

        public void Limpar(string chave) => _cache.Remove(chave);
    }
}

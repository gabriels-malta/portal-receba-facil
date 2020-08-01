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


        public T Obter<T>(string chave) => _cache.Get<T>(chave);

        public void Limpar(string chave) => _cache.Remove(chave);

        public bool TentarObter<T>(string chave, out T valor)
        {
            valor = default;
            var entry = _cache.Get(chave);

            if (entry != null)
            {
                valor = (T)entry;
                return true;
            }
            return false;

        }

        public T CriarOuObter<T>(string chave, T valor, TimeSpan expiraEm)
        {
            return _cache.GetOrCreate(chave, entry =>
            {
                entry.SlidingExpiration = expiraEm;
                return valor;
            });
        }
        public T CriarOuObter<T>(string chave, T valor) => CriarOuObter(chave, valor, TimeSpan.FromMinutes(16));
    }
}

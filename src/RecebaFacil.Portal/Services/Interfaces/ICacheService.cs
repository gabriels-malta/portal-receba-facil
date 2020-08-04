using System;

namespace RecebaFacil.Portal.Services.Interfaces
{
    public interface ICacheService
    {
        bool TentarObter<T>(string chave, out T valor);
        T Obter<T>(string chave);
        T CriarOuObter<T>(string chave, T valor);
        T CriarOuObter<T>(string chave, T valor, TimeSpan expiraEm);
        void Limpar(string chave);
    }
}
namespace RecebaFacil.Portal.Services.Interfaces
{
    public interface ICacheService
    {
        T Obter<T>(string chave);
        T CriarOuObter<T>(string chave, T valor);        
        void Limpar(string chave);
    }
}
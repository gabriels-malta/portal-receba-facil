using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IGrupoService
    {
        Grupo ObterPorId(short id);
    }
}

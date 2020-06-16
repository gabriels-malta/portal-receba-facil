using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceGrupo
    {
        Grupo ObterPorId(short id);
    }
}

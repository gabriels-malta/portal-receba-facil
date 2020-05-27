
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IPreRegistroService : IServiceBase<PreRegistro, System.Guid>
    {
        int Salvar(PreRegistro preRegistro);
    }
}

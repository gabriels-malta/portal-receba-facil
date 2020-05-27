
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IEmpresaService : IServiceBase<Empresa, int>
    {
        Empresa BuscarPorId(int id, bool carregarEndereco);
    }
}

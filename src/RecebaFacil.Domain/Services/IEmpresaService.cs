
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IEmpresaService
    {
        Empresa ObterPorId(int id);
        Empresa ObterPorId(int id, bool carregarEndereco);
    }
}

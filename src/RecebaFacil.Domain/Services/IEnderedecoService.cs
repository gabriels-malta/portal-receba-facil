
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IEnderedecoService : IServiceBase<Endereco, int>
    {
        Endereco ObterPorEmpresa(int empresaId);
        int Salvar(Endereco endereco);
    }
}

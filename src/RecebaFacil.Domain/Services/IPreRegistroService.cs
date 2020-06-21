
using RecebaFacil.Domain.Entities;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IPreRegistroService
    {
        Task Salvar(PreRegistro preRegistro);
    }
}

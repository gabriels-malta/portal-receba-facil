using RecebaFacil.Portal.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Services.Interfaces
{
    public interface IIBGEMicrorregioesService
    {
        Task<IEnumerable<Microrregioes>> ObterTodos();
    }
}
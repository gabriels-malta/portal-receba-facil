using System.Collections.Generic;

namespace RecebaFacil.Domain.Services
{
    public interface IEnumService
    {
        IEnumerable<KeyValuePair<int, string>> TipoMovimentoList();
    }
}

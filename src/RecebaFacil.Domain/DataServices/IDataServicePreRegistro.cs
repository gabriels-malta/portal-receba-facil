using RecebaFacil.Domain.Entities;
using System;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServicePreRegistro : IDataServiceBase<Guid>
    {
        int Salvar(PreRegistro registro);
    }
}

using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;

namespace RecebaFacil.Service
{
    public class GrupoService : IGrupoService
    {
        private readonly IDataServiceGrupo _DataServiceGrupo;

        public GrupoService(IDataServiceGrupo dataServiceGrupo)
        {
            _DataServiceGrupo = dataServiceGrupo;
        }

        public Grupo ObterPorId(short id)
        {
            return _DataServiceGrupo.ObterPorId(id);
        }
    }
}

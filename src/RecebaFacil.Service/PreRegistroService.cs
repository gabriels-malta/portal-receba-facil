using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;

namespace RecebaFacil.Service
{
    public class PreRegistroService : IPreRegistroService
    {
        private readonly IDataServicePreRegistro _PreRegistroService;

        public PreRegistroService(IDataServicePreRegistro preRegistroService)
        {
            _PreRegistroService = preRegistroService;
        }

        public int Salvar(PreRegistro entity)
        {
            return _PreRegistroService.Salvar(entity);
        }
    }
}

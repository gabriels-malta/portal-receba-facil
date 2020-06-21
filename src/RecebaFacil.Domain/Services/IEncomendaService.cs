
using RecebaFacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IEncomendaService
    {
        Task Salvar(Encomenda encomenda);        
        Task<IList<Encomenda>> ObterPorPontoVenda(Guid pontoVendaId);
        Task<IList<Encomenda>> ObterPorPontoDeRetirada(Guid pontoRetiradaId);
        Task Movimentar(EncomendaHistoria historia);
    }
}

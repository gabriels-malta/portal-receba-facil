
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IEncomendaService
    {
        Task<Encomenda> ObterPorId(Guid id);
        Task<Guid> Salvar(Encomenda encomenda);        
        Task<IList<Encomenda>> ObterPorPontoVenda(Guid pontoVendaId);
        Task<IList<Encomenda>> ObterPorPontoDeRetirada(Guid pontoRetiradaId);
        Task MovimentarPorPontoVenda(Guid encomendaId, Guid pontoVendaId);
        
        Task AdicionarMovimento(Guid encomendaId, Guid empresaId, TipoMovimento movimento);
    }
}

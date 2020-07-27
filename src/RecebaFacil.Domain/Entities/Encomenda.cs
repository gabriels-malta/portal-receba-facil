using RecebaFacil.Domain.Core.BaseEntities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecebaFacil.Domain.Entities
{
    public class Encomenda : IEntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PontoVendaId { get; set; }
        public Guid PontoRetiradaId { get; set; }
        public string NotaFiscal { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime DataPedido { get; set; }

        private List<EncomendaHistoria> _historia = new List<EncomendaHistoria>();
        public IReadOnlyList<EncomendaHistoria> Historia => _historia;

        public void AdicionarHistoria(EncomendaHistoria historia)
        {
            if (_historia.Any(x => x.Equals(historia)))
                throw new RecebaFacilException("Movimento não permitido para esta encomenda");

            _historia.Add(historia);
        }

        public bool PodeMovimentar() => !_historia.Select(x => x.TipoMovimento).Where(x => FimDaEsteira.Contains(x)).Any();

        public IEnumerable<TipoMovimento> ObterMovimentosPermitidos(TipoEmpresa tipoEmpresa)
        {
            switch (tipoEmpresa)
            {
                case TipoEmpresa.PontoVenda:
                    foreach (TipoMovimento item in Enum.GetValues(typeof(TipoMovimento)))
                    {
                        if (!NaoAtendePontoVenda.Contains(item))
                        {
                            if (!_historia.Select(x => x.TipoMovimento).Contains(item))
                                yield return item;
                        }
                    }
                    break;
                case TipoEmpresa.PontoRetirada:
                    foreach (TipoMovimento item in Enum.GetValues(typeof(TipoMovimento)))
                    {
                        if (!NaoAtendePontoRetirada.Contains(item))
                        {
                            if (!_historia.Select(x => x.TipoMovimento).Contains(item))
                                yield return item;
                        }
                    }
                    break;
                case TipoEmpresa.SemDados:
                default: break;
            }
        }

        private static TipoMovimento[] FimDaEsteira => new TipoMovimento[]
        {
            TipoMovimento.EsteiraFinalizada,
            TipoMovimento.DevolvidoPontoVenda,
            TipoMovimento.RetiradoClienteFinal
        };

        private static TipoMovimento[] NaoAtendePontoVenda => new TipoMovimento[]
        {
            TipoMovimento.DevolvidoPontoVenda,
            TipoMovimento.EsteiraIniciada,
            TipoMovimento.EsteiraFinalizada,
            TipoMovimento.AguardandoClienteFinal,
            TipoMovimento.AguardandoClienteFinal7Dias,
            TipoMovimento.AguardandoClienteFinal14Dias,
            TipoMovimento.RetiradoClienteFinal,
            TipoMovimento.RecebidoPontoRetirada
        };

        private static TipoMovimento[] NaoAtendePontoRetirada => new TipoMovimento[]
        {
            TipoMovimento.EsteiraIniciada,
            TipoMovimento.EnviadoPontoRetirada,
            TipoMovimento.NotaFiscalAlterada,
            TipoMovimento.NumeroPedidoAlterado,
            TipoMovimento.EsteiraFinalizada
        };
    }
}

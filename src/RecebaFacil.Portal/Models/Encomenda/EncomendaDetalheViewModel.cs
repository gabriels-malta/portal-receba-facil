using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RecebaFacil.Portal.Models.Encomenda
{
    public class EncomendaDetalheViewModel
    {
        public string Codigo => Regex.Replace(Id.ToString(), "-", "");
        public Guid Id { get; internal set; }
        public DateTime DataPedido { get; internal set; }
        public string NotaFiscal { get; internal set; }
        public string NumeroPedido { get; internal set; }
        public string PontoVendaNome { get; internal set; }
        public Guid PontoVendaId { get; internal set; }
        public bool PermiteMovimentar { get; internal set; }
        public IEnumerable<EncomendaHistoriaViewModel> Movimentacao { get; internal set; }
        public IEnumerable<SelectListItem> MovimentosPermitidos { get; set; }
    }
}

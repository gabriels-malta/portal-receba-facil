using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RecebaFacil.Portal.Models.Encomenda
{
    public class NovaEncomendaViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o número do pedido")]
        public string NumeroPedido { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe a nota fiscal")]
        public string NotaFiscal { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Selecione o ponto de retirada")]
        public Guid PontoRetiradaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Data do pedido é obrigatório")]
        public DateTime? DataPedido { get; set; }

        public IEnumerable<SelectListItem> ListaPontoRetirada { get; set; }

        public NovaEncomendaViewModel()
        {
            ListaPontoRetirada = Enumerable.Empty<SelectListItem>();
        }
    }
}

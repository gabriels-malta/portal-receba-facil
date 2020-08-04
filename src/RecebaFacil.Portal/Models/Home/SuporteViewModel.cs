using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RecebaFacil.Portal.Models.Home
{
    public class SuporteViewModel
    {
        public Guid EmpresaId { get; internal set; }
        public string PartialViewName { get; internal set; }

        public SuporteNovoEnderecoViewModel NovoEnderecoViewModel { get; set; }
    }

    public class SuporteNovoEnderecoViewModel
    {
        public SuporteNovoEnderecoViewModel()
        {
            Municipios = Enumerable.Empty<SelectListItem>();
        }
        public Guid EmpresaId { get; internal set; }
        public string EmpresaNome { get; internal set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatório")]
        public string Cep { get; internal set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatório")]
        public string Logradouro { get; internal set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatório")]
        public string Numero { get; internal set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatório")]
        public string Bairro { get; internal set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatório")]
        public string Cidade { get; internal set; }
        public string Uf => "SP";
        public IEnumerable<SelectListItem> Municipios { get; internal set; }
    }
}

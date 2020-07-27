using Microsoft.AspNetCore.Mvc.Rendering;
using RecebaFacil.Portal.Custom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace RecebaFacil.Portal.Models.Home
{
    public class CadastrarViewModel
    {
        public CadastrarViewModel()
        {
            Municipios = Enumerable.Empty<SelectListItem>();
        }

        [Required(ErrorMessage = "Selecione seu objetivo")]
        public string Objetivo { get; set; }

        [Display(Description = "Nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Description = "E-mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Display(Description = "Telefone")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatorio")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }

        [Display(Description = "Nome da Empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatorio")]
        [MaxLength(100, ErrorMessage = "Máx. de 100 caracteres")]
        public string NomeEmpresa { get; set; }

        [Display(Description = "Cidade")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatorio")]
        [MaxLength(80, ErrorMessage = "Máx. de 80 caracteres")]
        public string Cidade { get; set; }

        [Display(Description = "Endereço")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatorio")]
        [MaxLength(100, ErrorMessage = "Máx. de 150 caracteres")]
        public string Endereco { get; set; }

        [Display(Description = "CNPJ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatorio")]
        [Cnpj(ErrorMessage = "CNPJ inválido")]
        public string Cnpj { get; set; }

        public IEnumerable<SelectListItem> Municipios { get; internal set; }

        internal void MontarListaDeMunicipios(IEnumerable<IBGEMicrorregioes> microrregioes)
            => Municipios = new SelectListItem[] { new SelectListItem(text: "", value: "", true) }.Concat(microrregioes.Select(x => new SelectListItem(x.Nome, x.Nome)));
    }

    public struct IBGEMicrorregioes
    {
        public IBGEMicrorregioes(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        [JsonPropertyName("id")]
        public string Id { get; private set; }
        [JsonPropertyName("nome")]
        public string Nome { get; private set; }
    }
}

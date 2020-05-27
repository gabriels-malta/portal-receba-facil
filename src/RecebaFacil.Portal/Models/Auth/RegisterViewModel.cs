using RecebaFacil.Portal.Custom;
using System.ComponentModel.DataAnnotations;

namespace RecebaFacil.Portal.Models.Auth
{
    public class RegisterViewModel
    {
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
    }
}

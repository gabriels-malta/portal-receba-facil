using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RecebaFacil.Portal.Models.Auth
{
    public class LogonViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string LoginName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres")]
        public string Senha { get; set; }
    }
}

using System;

namespace RecebaFacil.Portal.Models
{
    public class InicioViewModel
    {
        public string Hoje => DateTime.Now.ToShortDateString();
        public string NomeEmpresa { get; set; }
        public string NomeUsuario { get; set; }
    }
}

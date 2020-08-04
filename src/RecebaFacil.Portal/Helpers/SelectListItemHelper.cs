using Microsoft.AspNetCore.Mvc.Rendering;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Portal.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecebaFacil.Portal.Helpers
{
    public class SelectListItemHelper
    {
        private IEnumerable<SelectListItem> AdicionarPlaceHolder(IEnumerable<SelectListItem> lista, string placeholder) => lista.Prepend(new SelectListItem(text: placeholder, value: "", true));
        internal IEnumerable<SelectListItem> MontarListaDeMunicipios(IEnumerable<Microrregioes> microrregioes, string placeholder = null)
        {
            var lista = microrregioes.Select(x => new SelectListItem(x.Nome, x.Nome));
            if (!string.IsNullOrWhiteSpace(placeholder))
                lista = AdicionarPlaceHolder(lista, placeholder);

            return lista;
        }

        internal IEnumerable<SelectListItem> DiasSemana(string placeholder = null)
        {
            var lista = new SelectListItem[]
            {
                new SelectListItem(text:DiaSemana.Segunda.GetDescription(),value:((int)DiaSemana.Segunda).ToString()),
                new SelectListItem(text:DiaSemana.Terca.GetDescription(),value:((int)DiaSemana.Terca).ToString()),
                new SelectListItem(text:DiaSemana.Quarta.GetDescription(),value:((int)DiaSemana.Quarta).ToString()),
                new SelectListItem(text:DiaSemana.Quinta.GetDescription(),value:((int)DiaSemana.Quinta).ToString()),
                new SelectListItem(text:DiaSemana.Sexta.GetDescription(),value:((int)DiaSemana.Sexta).ToString()),
                new SelectListItem(text:DiaSemana.Sabado.GetDescription(),value:((int)DiaSemana.Sabado).ToString()),
                new SelectListItem(text:DiaSemana.Domingo.GetDescription(),value:((int)DiaSemana.Domingo).ToString()),
            }.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(placeholder))
                lista = AdicionarPlaceHolder(lista, placeholder);

            return lista;
        }
    }
}

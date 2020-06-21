using RecebaFacil.Domain;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using System;
using System.Collections.Generic;

namespace RecebaFacil.Service
{
    public class EnumService : IEnumService
    {
        public IEnumerable<KeyValuePair<int, string>> TipoMovimentoList()
        {
            foreach (TipoMovimento item in Enum.GetValues(typeof(TipoMovimento)))
            {
                yield return new KeyValuePair<int, string>((int)item, item.GetDescription());
            }
        }
    }
}

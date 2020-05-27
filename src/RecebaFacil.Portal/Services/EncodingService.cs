using RecebaFacil.Portal.Services.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace RecebaFacil.Portal.Services
{
    public class EncodingService : IEncodingService
    {
        public string DecodeFromBase64(string valor)
        {
            if (Regex.IsMatch(valor, @"^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$"))
                return Encoding.UTF8.GetString(Convert.FromBase64String(valor));

            return null;
        }

        public string EncodeToBase64(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return null;

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(valor));
        }

        public string EncodeToBase64(int valor) => EncodeToBase64(valor.ToString());

        public string EncodeToBase64(decimal valor) => EncodeToBase64(valor.ToString(CultureInfo.InvariantCulture));
    }
}

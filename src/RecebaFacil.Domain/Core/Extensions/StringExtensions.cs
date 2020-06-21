using System.Text.RegularExpressions;

namespace RecebaFacil.Domain
{
    public static class StringExtensions
    {
        public static string FormatarCelular(this string valor)
        {
            string area = valor.Substring(0, 2);
            string inicio = valor.Substring(2, 5);
            string fim = valor.Substring(7);
            return $"({area}) {inicio}-{fim}";
        }
        public static string FormatarTelefoneFixo(this string valor)
        {
            string area = valor.Substring(0, 2);
            string inicio = valor.Substring(2, 4);
            string fim = valor.Substring(6);
            return $"({area}) {inicio}-{fim}";
        }
        public static string FormatarCnpj(this string valor)
        {
            return Regex.Replace(valor, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", @"$1.$2.$3/$4-$5");
        }
    }
}

using System;

namespace RecebaFacil.Domain
{
    public static class NumberExtensions
    {
        public static string ToHex(this int valor) => valor.ToString("x2");
        public static int FromHex(this string valor) => Convert.ToInt32(valor, 16);
    }
}

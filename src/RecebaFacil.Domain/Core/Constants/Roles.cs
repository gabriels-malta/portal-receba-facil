namespace RecebaFacil.Domain
{
    public static class Roles
    {
        public const string ADMINISTRADOR = "ADMINISTRADOR";
        public const string PONTO_RETIRADA = "PONTO_RETIRADA";
        public const string PONTO_VENDA = "PONTO_VENDA";
    }

    public static class AuthorizedRoles
    {
        public const string Get = "PONTO_VENDA,PONTO_RETIRADA";
    }
}

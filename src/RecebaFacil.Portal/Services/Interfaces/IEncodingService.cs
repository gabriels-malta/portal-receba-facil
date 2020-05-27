namespace RecebaFacil.Portal.Services.Interfaces
{
    public interface IEncodingService
    {
        string EncodeToBase64(string valor);
        string EncodeToBase64(int valor);
        string EncodeToBase64(decimal valor);

        string DecodeFromBase64(string valor);        
    }
}
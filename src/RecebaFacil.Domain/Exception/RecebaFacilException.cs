namespace RecebaFacil.Domain.Exception
{

    public class RecebaFacilException : System.Exception
    {
        public RecebaFacilException()
        {
        }

        public RecebaFacilException(string message) 
            : base(message)
        {
        }

        public RecebaFacilException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

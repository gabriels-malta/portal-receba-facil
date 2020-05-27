using System;
using System.Text.RegularExpressions;

namespace RecebaFacil.Domain.Entities
{
    public class Grupo : EntityBase<byte>
    {
        public string Nome { get; set; }

        private string _role;
        public string Role
        {
            get { return _role; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Invalid user role");

                _role = Regex.Replace(value, " ", "_").ToUpper();
            }
        }

    }
}

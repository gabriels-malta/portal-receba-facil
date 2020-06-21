using RecebaFacil.Domain.Core.BaseEntities;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class Grupo : IEntityBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; }
    }
}

using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecebaFacil.Domain.Entities
{
    public class PontoRetirada : Empresa
    {
        public IReadOnlyList<Expediente> Expediente => _expediente;

        private List<Expediente> _expediente = new List<Expediente>();

        public PontoRetirada(string razaoSocial,
            string nomeFantasia,
            string cnpj)
            : base(TipoEmpresa.PontoRetirada, razaoSocial, nomeFantasia, cnpj)
        { }

        public PontoRetirada(string razaoSocial,
            string nomeFantasia,
            string cnpj,
            DateTime dataCadastro)
            : base(TipoEmpresa.PontoRetirada, razaoSocial, nomeFantasia, cnpj, dataCadastro)
        { }

        public void AdicionarExpediente(Expediente expediente)
        {
            if (_expediente.Any(x => x.DiaSemana == expediente.DiaSemana))
                throw new RecebaFacilException("Já existe um expediente para esse dia da semana");

            _expediente.Add(expediente);
        }
    }
}

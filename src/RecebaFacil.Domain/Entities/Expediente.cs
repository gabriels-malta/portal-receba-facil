﻿using RecebaFacil.Domain.Core.BaseEntities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Exception;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RecebaFacil.Domain.Entities
{
    public class Expediente : IEntityBase, IEquatable<Expediente>
    {
        public Guid Id { get; set; }
        public Guid PontoRetiradaId { get; set; }
        public DiaSemana DiaSemana { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraEncerramento { get; set; }

        public virtual PontoRetirada PontoRetirada { get; set; }

        public void ValidarHorarios()
        {
            if (HoraAbertura.CompareTo(HoraEncerramento) >= 0)
                throw new RecebaFacilException($"Horário de funcionamento inválido");
        }

        public void ValidarDiaSemana()
        {
            if (!Enum.IsDefined(typeof(DiaSemana), DiaSemana))
                throw new RecebaFacilException("Dia da semana inválido");
        }

        public bool Equals([AllowNull] Expediente other)
        {
            if (other == null)
                return false;

            return DiaSemana == other.DiaSemana
                && HoraAbertura.CompareTo(HoraEncerramento) == 0;
        }
    }
}

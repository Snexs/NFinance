﻿using NFinance.ViewModel.ResgatesViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NFinance.Domain
{
    public class Resgate
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        [Required]
        public Guid IdInvestimento { get; set; }

        [ForeignKey("Id")]
        [Required]
        public Guid IdCliente { get; set; }

        [Required]
        [Range(0 ,999999999999, ErrorMessage = "Valor {0} deve estar entre {1} e {2}")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Motivo deve conter no minimo 10 letras e no maximo 100")]
        [StringLength(100, MinimumLength = 10)]
        public string MotivoResgate { get; set; }

        [Required]
        [Range(typeof(DateTime), "01/01/1950", "12/31/2100", ErrorMessage = "Data {0} deve estar entre {1} e {2}")]
        public DateTime DataResgate { get; set; }

        public Resgate() { }

        public Resgate(Resgate resgate)
        {
            Id = Guid.NewGuid();
            IdInvestimento = resgate.IdInvestimento;
            IdCliente = resgate.IdCliente;
            Valor = resgate.Valor;
            MotivoResgate = resgate.MotivoResgate;
            DataResgate = resgate.DataResgate;
        }

        public Resgate(RealizarResgateViewModel.Request request)
        {
            Id = Guid.NewGuid();
            IdInvestimento = request.IdInvestimento;
            IdCliente = request.IdCliente;
            Valor = request.Valor;
            MotivoResgate = request.MotivoResgate;
            DataResgate = request.DataResgate;
        }
    }
}

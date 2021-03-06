﻿using System;
using System.ComponentModel.DataAnnotations;
using NFinance.Domain.ViewModel.ClientesViewModel;

namespace NFinance.Domain
{
    public class Cliente
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100,MinimumLength = 10)]
        public string Nome { get; set; }

        [Required]
        [StringLength(14,MinimumLength = 11)]
        public string CPF { get; set; }
        
        [Required]
        [StringLength(120,MinimumLength = 15)]
        public string Email { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 10)]
        public string Senha { get; set; }

        [StringLength(400)]
        public string LogoutToken { get; set; }

        public Cliente() { }

        public Cliente(CadastrarClienteViewModel.Request clienteRequest)
        {
            Id = Guid.NewGuid();
            Nome = clienteRequest.Nome;
            CPF = clienteRequest.Cpf;
            Email = clienteRequest.Email;
            Senha = clienteRequest.Senha;
        }

        public Cliente(Guid id,AtualizarClienteViewModel.Request request)
        {
            Id = id;
            Nome = request.Nome;
            CPF = request.Cpf;
            Email = request.Email;
            Senha = request.Senha;
        }

        public Cliente(Cliente cliente, string token)
        {
            Id = cliente.Id;
            Nome = cliente.Nome;
            CPF = cliente.CPF;
            Email = cliente.Email;
            LogoutToken = token;
        }

        public Cliente(ConsultarClienteViewModel.Response cliente, string token)
        {
            Id = cliente.Id;
            Nome = cliente.Nome;
            CPF = cliente.Cpf;
            Email = cliente.Email;
            LogoutToken = token;
        }
    }
}

﻿using NFinance.Domain.Exceptions;
using NFinance.Domain.Exceptions.Gasto;
using NFinance.Domain.Interfaces.Repository;
using NFinance.Domain.Interfaces.Services;
using NFinance.Model.GastosViewModel;
using System;
using System.Threading.Tasks;
using NFinance.Domain.ViewModel.ClientesViewModel;

namespace NFinance.Domain.Services
{
    public class GastosService : IGastosService
    {
        private readonly IGastosRepository _gastosRepository;
        private readonly IClienteService _clienteService;
        public GastosService(IGastosRepository gastosRepository, IClienteService clienteService)
        {
            _gastosRepository = gastosRepository;
            _clienteService = clienteService;
        }

        public async Task<AtualizarGastoViewModel.Response> AtualizarGasto(Guid id, AtualizarGastoViewModel.Request request)
        {
            if (Guid.Empty.Equals(id)) throw new IdException("ID gasto invalido");
            if (Guid.Empty.Equals(request.IdCliente)) throw new IdException("ID cliente invalido");
            if (string.IsNullOrWhiteSpace(request.NomeGasto)) throw new NomeGastoException("Nome nao deve ser vazio,branco ou nulo");
            if (request.ValorTotal <= 0) throw new ValorGastoException("Valor deve ser maior que zero");
            if (request.QuantidadeParcelas <= 0 || request.QuantidadeParcelas >= 1000) throw new QuantidadeParcelaException("Valor deve ser maior que zero e menor que mil");
            if (request.DataDoGasto > DateTime.MaxValue.AddYears(-7899) || request.DataDoGasto < DateTime.MinValue.AddYears(1949)) throw new DataGastoException();

            var gasto = new Gastos(id,request);
            var cliente = await _clienteService.ConsultarCliente(request.IdCliente);
            var atualizado = await _gastosRepository.AtualizarGasto(id, gasto);
            var response = new AtualizarGastoViewModel.Response() {Id = atualizado.Id, Cliente = new ClienteViewModel.Response() {Id = cliente.Id,Nome = cliente.Nome } , NomeGasto = atualizado.NomeGasto,DataDoGasto = atualizado.DataDoGasto, QuantidadeParcelas = atualizado.QuantidadeParcelas,ValorTotal = atualizado.ValorTotal };
            return response;
        }

        public async Task<CadastrarGastoViewModel.Response> CadastrarGasto(CadastrarGastoViewModel.Request request)
        {
            if (Guid.Empty.Equals(request.IdCliente)) throw new IdException("ID cliente invalido");
            if (string.IsNullOrWhiteSpace(request.NomeGasto)) throw new NomeGastoException("Nome nao deve ser vazio,branco ou nulo");
            if (request.ValorTotal <= 0) throw new ValorGastoException("Valor deve ser maior que zero");
            if (request.QuantidadeParcelas <= 0 || request.QuantidadeParcelas >= 1000) throw new QuantidadeParcelaException("Valor deve ser maior que zero e menor que mil");
            if (request.DataDoGasto > DateTime.MaxValue.AddYears(-7899) || request.DataDoGasto < DateTime.MinValue.AddYears(1949)) throw new DataGastoException();

            var gasto = new Gastos(request);
            var cliente = await _clienteService.ConsultarCliente(request.IdCliente);
            var cadastro = await _gastosRepository.CadastrarGasto(gasto);
            var response = new CadastrarGastoViewModel.Response() { Id = cadastro.Id, Cliente = new ClienteViewModel.Response() { Id = cliente.Id, Nome = cliente.Nome }, NomeGasto = cadastro.NomeGasto, DataDoGasto = cadastro.DataDoGasto, QuantidadeParcelas = cadastro.QuantidadeParcelas, ValorTotal = cadastro.ValorTotal };
            return response;
        }

        public async Task<ConsultarGastoViewModel.Response> ConsultarGasto(Guid id)
        {
            if (Guid.Empty.Equals(id)) throw new IdException("ID gasto invalido");

            var gasto = await _gastosRepository.ConsultarGasto(id);
            var cliente = await _clienteService.ConsultarCliente(gasto.IdCliente);
            var response = new ConsultarGastoViewModel.Response() { Id = gasto.Id, Cliente = new ClienteViewModel.Response() { Id = cliente.Id, Nome = cliente.Nome }, NomeGasto = gasto.NomeGasto, DataDoGasto = gasto.DataDoGasto, QuantidadeParcelas = gasto.QuantidadeParcelas, ValorTotal = gasto.ValorTotal };
            return response;
        }

        public async Task<ExcluirGastoViewModel.Response> ExcluirGasto(ExcluirGastoViewModel.Request request)
        {
            if (Guid.Empty.Equals(request.IdGasto)) throw new IdException("ID gasto invalido");
            if (Guid.Empty.Equals(request.IdCliente)) throw new IdException("ID gasto invalido");
            if (string.IsNullOrWhiteSpace(request.MotivoExclusao)) throw new NomeGastoException("Motivo exclusao nao pode ser vazio,branco ou nulo");

            var excluir = await _gastosRepository.ExcluirGasto(request.IdGasto);
            if(excluir)
                return new ExcluirGastoViewModel.Response() { StatusCode = 200, DataExclusao = DateTime.UtcNow, Mensagem = "Excluido Com Sucesso" };
             else
                return new ExcluirGastoViewModel.Response() { StatusCode = 400, DataExclusao = DateTime.UtcNow, Mensagem = "Ocorreu um erro ao Excluir" };
        }

        public async Task<ListarGastosViewModel.Response> ListarGastos()
        {
            var listaGastos = await _gastosRepository.ListarGastos();
            var response = new ListarGastosViewModel.Response(listaGastos);
            return response;
        }
    }
}

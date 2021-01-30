﻿using NFinance.Domain;
using NFinance.Domain.Exceptions;
using NFinance.Domain.Exceptions.Investimento;
using NFinance.Domain.Interfaces.Repository;
using NFinance.Domain.Interfaces.Services;
using NFinance.Domain.Services;
using NFinance.Model.ClientesViewModel;
using NFinance.Model.InvestimentosViewModel;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace NFinance.Tests.Service
{
    public class InvestimentoServiceTests
    {
        private IInvestimentosRepository _investimentosRepository;
        private IClienteService _clienteService;

        public InvestimentoServiceTests()
        {
            _clienteService = Substitute.For<IClienteService>();
            _investimentosRepository = Substitute.For<IInvestimentosRepository>();
        }

        public InvestimentosService InicializaServico()
        {
            return new InvestimentosService(_investimentosRepository, _clienteService);
        }

        [Fact]
        public async void InvestimentoService_RealizarInvestimento_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new RealizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.RealizarInvestimento(Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Act
            var response = await services.RealizarInvestimento(investimentoRequest);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.NomeInvestimento);
            Assert.NotNull(response.Cliente);
            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.NotEqual(Guid.Empty, response.Cliente.Id);
            Assert.NotEqual(0, response.Valor);
            Assert.NotEqual("00/00/00", response.DataAplicacao.ToString());
            Assert.Equal(id, response.Id);
            Assert.Equal(nomeInvestimento, response.NomeInvestimento);
            Assert.Equal(data, response.DataAplicacao);
            Assert.Equal(valorInvestido, response.Valor);
            Assert.Equal(idCliente, response.Cliente.Id);
            Assert.Equal(nomeCliente, response.Cliente.Nome);
        }

        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(),Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Act
            var response = await services.AtualizarInvestimento(id,investimentoRequest);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.NomeInvestimento);
            Assert.NotNull(response.Cliente);
            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.NotEqual(Guid.Empty, response.Cliente.Id);
            Assert.NotEqual(0, response.Valor);
            Assert.NotEqual("00/00/00", response.DataAplicacao.ToString());
            Assert.Equal(id, response.Id);
            Assert.Equal(nomeInvestimento, response.NomeInvestimento);
            Assert.Equal(data, response.DataAplicacao);
            Assert.Equal(valorInvestido, response.Valor);
            Assert.Equal(idCliente, response.Cliente.Id);
            Assert.Equal(nomeCliente, response.Cliente.Nome);
        }

        [Fact]
        public async void InvestimentoService_RealizarInvestimento_ComIdCliente_Invalido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.Empty;
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new RealizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.RealizarInvestimento(Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<IdException>(() => /*Act*/ services.RealizarInvestimento(investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_RealizarInvestimento_ComNomeInvestimento_Vazio()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new RealizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.RealizarInvestimento(Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<NomeInvestimentoException>(() => /*Act*/ services.RealizarInvestimento(investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_RealizarInvestimento_ComNomeInvestimento_EmBranco()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "  ";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new RealizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.RealizarInvestimento(Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<NomeInvestimentoException>(() => /*Act*/ services.RealizarInvestimento(investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_RealizarInvestimento_ComNomeInvestimento_Nulo()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new RealizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = null, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.RealizarInvestimento(Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = null, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<NomeInvestimentoException>(() => /*Act*/ services.RealizarInvestimento(investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_RealizarInvestimento_ComValor_Invalido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeCliente = "Jorge Nunes";
            var nomeInvestimento = "Teste@Sucesso";
            decimal valorInvestido = 0;
            var data = DateTime.Today;
            var investimentoRequest = new RealizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.RealizarInvestimento(Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<ValorInvestimentoException>(() => /*Act*/ services.RealizarInvestimento(investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_RealizarInvestimento_ComDataAplicacao_Invalido_Maior_Permitido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 1548421.18454M;
            var data = DateTime.Today.AddYears(120);
            var investimentoRequest = new RealizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.RealizarInvestimento(Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<DataInvestimentoException>(() => /*Act*/ services.RealizarInvestimento(investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_RealizarInvestimento_ComDataAplicacao_Invalido_Menor_Permitido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 1548421.18454M;
            var data = DateTime.Today.AddYears(-120);
            var investimentoRequest = new RealizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.RealizarInvestimento(Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<DataInvestimentoException>(() => /*Act*/ services.RealizarInvestimento(investimentoRequest));
        }


        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComIdInvestimento_Invalido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idInvestimentoInvalido = Guid.Empty;
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(), Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<IdException>(() => /*Act*/ services.AtualizarInvestimento(idInvestimentoInvalido, investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComIdCliente_Invalido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.Empty;
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(), Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<IdException>(() => /*Act*/ services.AtualizarInvestimento(id, investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComNomeInvestimento_Nulo()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = null, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(), Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = null, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<NomeInvestimentoException>(() => /*Act*/ services.AtualizarInvestimento(id, investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComNomeInvestimento_Vazio()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(), Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<NomeInvestimentoException>(() => /*Act*/ services.AtualizarInvestimento(id, investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComNomeInvestimento_EmBranco()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "  ";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(), Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<NomeInvestimentoException>(() => /*Act*/ services.AtualizarInvestimento(id, investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComValor_Invalido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Invest";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 0;
            var data = DateTime.Today;
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(), Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<ValorInvestimentoException>(() => /*Act*/ services.AtualizarInvestimento(id, investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComDataAplicacao_Menor_Permitido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Invest";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 12354561.2131M;
            var data = DateTime.Today.AddYears(-110);
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(), Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<DataInvestimentoException>(() => /*Act*/ services.AtualizarInvestimento(id, investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_AtualizarInvestimento_ComDataAplicacao_Maior_Permitido()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Invest";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 12354561.2131M;
            var data = DateTime.Today.AddYears(110);
            var investimentoRequest = new AtualizarInvestimentoViewModel.Request() { IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            _investimentosRepository.AtualizarInvestimento(Arg.Any<Guid>(), Arg.Any<Investimentos>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<DataInvestimentoException>(() => /*Act*/ services.AtualizarInvestimento(id, investimentoRequest));
        }

        [Fact]
        public async void InvestimentoService_ConsultarInvestimento_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            _investimentosRepository.ConsultarInvestimento(Arg.Any<Guid>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Act
            var response = await services.ConsultarInvestimento(id);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.NomeInvestimento);
            Assert.NotNull(response.Cliente);
            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.NotEqual(Guid.Empty, response.Cliente.Id);
            Assert.NotEqual(0, response.Valor);
            Assert.NotEqual("00/00/00", response.DataAplicacao.ToString());
            Assert.Equal(id, response.Id);
            Assert.Equal(nomeInvestimento, response.NomeInvestimento);
            Assert.Equal(data, response.DataAplicacao);
            Assert.Equal(valorInvestido, response.Valor);
            Assert.Equal(idCliente, response.Cliente.Id);
            Assert.Equal(nomeCliente, response.Cliente.Nome);
        }

        [Fact]
        public async void InvestimentoService_ConsultarInvestimento_ComId_Vazio()
        {
            //Arrange
            var id = Guid.Empty;
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            _investimentosRepository.ConsultarInvestimento(Arg.Any<Guid>()).Returns(new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data });
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Assert
            await Assert.ThrowsAsync<IdException>(() => /*Act*/ services.ConsultarInvestimento(id));
        }

        [Fact]
        public async void InvestimentoService_ListarInvestimentos_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeInvestimento = "Teste@Sucesso";
            var nomeCliente = "Jorge Nunes";
            decimal valorInvestido = 553484.215M;
            var data = DateTime.Today;
            var investimento = new Investimentos() { Id = id, IdCliente = idCliente, NomeInvestimento = nomeInvestimento, Valor = valorInvestido, DataAplicacao = data };
            var listaInvestimento = new List<Investimentos>();
            listaInvestimento.Add(investimento);
            _investimentosRepository.ListarInvestimentos().Returns(new List<Investimentos>(listaInvestimento));
            _clienteService.ConsultarCliente(Arg.Any<Guid>()).Returns(new ConsultarClienteViewModel.Response() { Id = idCliente, Nome = nomeCliente });
            var services = InicializaServico();

            //Act
            var response = await services.ListarInvestimentos();

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Find(i => i.Id == id).NomeInvestimento);
            Assert.NotEqual(Guid.Empty, response.Find(i => i.Id == id).Id);
            Assert.NotEqual(Guid.Empty, response.Find(i => i.Id == id).IdCliente);
            Assert.NotEqual(0, response.Find(i => i.Id == id).Valor);
            Assert.NotEqual("00/00/00", response.Find(i => i.Id == id).DataAplicacao.ToString());
            Assert.Equal(id, response.Find(i => i.Id == id).Id);
            Assert.Equal(nomeInvestimento, response.Find(i => i.Id == id).NomeInvestimento);
            Assert.Equal(data, response.Find(i => i.Id == id).DataAplicacao);
            Assert.Equal(valorInvestido, response.Find(i => i.Id == id).Valor);
            Assert.Equal(idCliente, response.Find(i => i.Id == id).IdCliente);
        }
    }
}
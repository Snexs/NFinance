﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NFinance.Domain;
using NFinance.Domain.Interfaces.Services;
using NFinance.ViewModel.GastosViewModel;
using NFinance.WebApi.Controllers;
using NSubstitute;
using System;
using System.Collections.Generic;
using NFinance.Domain.ViewModel.ClientesViewModel;
using Xunit;
using NFinance.Domain.Services;

namespace NFinance.Tests.WebApi
{
    public class GastosControllerTests
    {
        private readonly IGastoService _gastosService;
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly ILogger<GastosController> _logger;
        public GastosControllerTests()
        {
            _gastosService = Substitute.For<IGastoService>();
            _autenticacaoService = Substitute.For<IAutenticacaoService>();
            _logger = Substitute.For<ILogger<GastosController>>();
        }

        private GastosController InicializarGastosController()
        {
            return new GastosController(_logger, _gastosService,_autenticacaoService);
        }

        [Fact]
        public void GastosController_CadastrarGasto_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var nomeGasto = "Cartao De Credito";
            decimal valorTotal = 1238.12M;
            var dataGasto = DateTime.Today;
            var parcelas = 64;
            var idCliente = Guid.NewGuid();
            var nomeCliente = "Alberto Junior";
            var cliente = new ClienteViewModel.SimpleResponse { Id = idCliente, Nome = nomeCliente };
            _gastosService.CadastrarGasto(Arg.Any<CadastrarGastoViewModel.Request>())
                .Returns(new CadastrarGastoViewModel.Response
                {
                    Id = id,
                    NomeGasto = nomeGasto,
                    QuantidadeParcelas = parcelas,
                    DataDoGasto = dataGasto,
                    Valor = valorTotal,
                    Cliente = cliente
                });
            var controller = InicializarGastosController();
            var gasto = new CadastrarGastoViewModel.Request
            {
                NomeGasto = nomeGasto,
                QuantidadeParcelas = parcelas,
                DataDoGasto = dataGasto,
                Valor = valorTotal,
                IdCliente = idCliente
            };
            var token = TokenService.GerarToken(new Cliente { Id = idCliente, CPF = "12345678910", Email = "teste@teste.com", Nome = "teste da silva" });

            //Act
            var teste = controller.CadastrarGasto(token,gasto);
            var okResult = teste.Result as ObjectResult;
            var cadastrarGastoViewModel = Assert.IsType<CadastrarGastoViewModel.Response>(okResult.Value);

            //Assert
            Assert.NotNull(teste);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(id, cadastrarGastoViewModel.Id);
            Assert.Equal(nomeGasto, cadastrarGastoViewModel.NomeGasto);
            Assert.Equal(parcelas, cadastrarGastoViewModel.QuantidadeParcelas);
            Assert.Equal(dataGasto, cadastrarGastoViewModel.DataDoGasto);
            Assert.Equal(valorTotal, cadastrarGastoViewModel.Valor);
            Assert.Equal(idCliente, cadastrarGastoViewModel.Cliente.Id);
            Assert.Equal(nomeCliente, cadastrarGastoViewModel.Cliente.Nome);
        }

        [Fact]
        public void GastosController_AtualizarGasto_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var nomeGasto = "Cartao De Credito";
            decimal valorTotal = 1238.12M;
            var dataGasto = DateTime.Today;
            var parcelas = 64;
            var idCliente = Guid.NewGuid();
            var nomeCliente = "Alberto Junior";
            var cliente = new ClienteViewModel.SimpleResponse { Id = idCliente, Nome = nomeCliente };
            _gastosService.AtualizarGasto(Arg.Any<Guid>(),Arg.Any<AtualizarGastoViewModel.Request>())
                .Returns(new AtualizarGastoViewModel.Response
                {
                    Id = id,
                    NomeGasto = nomeGasto,
                    QuantidadeParcelas = parcelas,
                    DataDoGasto = dataGasto,
                    Valor = valorTotal,
                    Cliente = cliente
                });
            var controller = InicializarGastosController();
            var gasto = new AtualizarGastoViewModel.Request
            {
                NomeGasto = nomeGasto,
                QuantidadeParcelas = parcelas,
                DataDoGasto = dataGasto,
                Valor = valorTotal,
                IdCliente = idCliente
            };
            var token = TokenService.GerarToken(new Cliente { Id = idCliente, CPF = "12345678910", Email = "teste@teste.com", Nome = "teste da silva" });

            //Act
            var teste = controller.AtualizarGasto(token,id,gasto);
            var okResult = teste.Result as ObjectResult;
            var atualizarGastoViewModel = Assert.IsType<AtualizarGastoViewModel.Response>(okResult.Value);

            //Assert
            Assert.NotNull(teste);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(id, atualizarGastoViewModel.Id);
            Assert.Equal(nomeGasto, atualizarGastoViewModel.NomeGasto);
            Assert.Equal(parcelas, atualizarGastoViewModel.QuantidadeParcelas);
            Assert.Equal(dataGasto, atualizarGastoViewModel.DataDoGasto);
            Assert.Equal(valorTotal, atualizarGastoViewModel.Valor);
            Assert.Equal(idCliente, atualizarGastoViewModel.Cliente.Id);
            Assert.Equal(nomeCliente, atualizarGastoViewModel.Cliente.Nome);
        }

        [Fact]
        public void GastosController_ConsultarGasto_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var nomeGasto = "Cartao De Credito";
            decimal valorTotal = 1238.12M;
            var dataGasto = DateTime.Today;
            var parcelas = 64;
            var idCliente = Guid.NewGuid();
            var nomeCliente = "Alberto Junior";
            var cliente = new ClienteViewModel.SimpleResponse { Id = idCliente, Nome = nomeCliente };
            _gastosService.ConsultarGasto(Arg.Any<Guid>())
                .Returns(new ConsultarGastoViewModel.Response
                {
                    Id = id,
                    NomeGasto = nomeGasto,
                    QuantidadeParcelas = parcelas,
                    DataDoGasto = dataGasto,
                    Valor = valorTotal,
                    Cliente = cliente
                });
            var controller = InicializarGastosController();
            var token = TokenService.GerarToken(new Cliente { Id = idCliente, CPF = "12345678910", Email = "teste@teste.com", Nome = "teste da silva" });

            //Act
            var teste = controller.ConsultarGasto(token,id);
            var okResult = teste.Result as ObjectResult;
            var consultarGastoViewModel = Assert.IsType<ConsultarGastoViewModel.Response>(okResult.Value);

            //Assert
            Assert.NotNull(teste);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(id, consultarGastoViewModel.Id);
            Assert.Equal(nomeGasto, consultarGastoViewModel.NomeGasto);
            Assert.Equal(parcelas, consultarGastoViewModel.QuantidadeParcelas);
            Assert.Equal(dataGasto, consultarGastoViewModel.DataDoGasto);
            Assert.Equal(valorTotal, consultarGastoViewModel.Valor);
            Assert.Equal(idCliente, consultarGastoViewModel.Cliente.Id);
            Assert.Equal(nomeCliente, consultarGastoViewModel.Cliente.Nome);
        }

        [Fact]
        public void GastosController_ExcluirGasto_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var dataExclusao = DateTime.UtcNow;
            var idCliente = Guid.NewGuid();
            _gastosService.ExcluirGasto(Arg.Any<ExcluirGastoViewModel.Request>())
                .Returns(new ExcluirGastoViewModel.Response
                {
                    Mensagem = "Excluido Com Sucesso",
                    StatusCode = 200,
                    DataExclusao = dataExclusao
                });
            var controller = InicializarGastosController();
            var gasto = new ExcluirGastoViewModel.Request
            {
                IdGasto = id,
                MotivoExclusao = "Finalizado Pagamento",
                IdCliente = idCliente
            };
            var token = TokenService.GerarToken(new Cliente { Id = idCliente, CPF = "12345678910", Email = "teste@teste.com", Nome = "teste da silva" });

            //Act
            var teste = controller.ExcluirGasto(token,gasto);
            var okResult = teste.Result as ObjectResult;
            var excluirGastoViewModel = Assert.IsType<ExcluirGastoViewModel.Response>(okResult.Value);

            //Assert
            Assert.NotNull(teste);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(dataExclusao, excluirGastoViewModel.DataExclusao);
            Assert.Equal("Excluido Com Sucesso", excluirGastoViewModel.Mensagem);
            Assert.Equal(200, excluirGastoViewModel.StatusCode);
        }

        [Fact]
        public void GastosController_ConsultarGastos_ComSucesso()
        {
            //Arrange
            var id = Guid.NewGuid();
            var id1 = Guid.NewGuid();
            var idCliente = Guid.NewGuid();
            var nomeGasto = "uasduhasha";
            var valor = 1238.12M;
            var dataGasto = DateTime.Today;
            var listaGasto = new List<Gasto>();
            var gasto = new Gasto
            {
                Id = id,
                IdCliente = idCliente,
                NomeGasto = nomeGasto,
                Valor = valor,
                QuantidadeParcelas = 5,
                DataDoGasto = dataGasto
            };
            var gasto1 = new Gasto
            {
                Id = id1,
                IdCliente = idCliente,
                NomeGasto = nomeGasto,
                Valor = valor,
                QuantidadeParcelas = 5,
                DataDoGasto = dataGasto
            };
            listaGasto.Add(gasto);
            listaGasto.Add(gasto1);
            var listarGastos = new ConsultarGastosViewModel.Response(listaGasto);
            _gastosService.ConsultarGastos(Arg.Any<Guid>()).Returns(listarGastos);
            var controller = InicializarGastosController();
            var token = TokenService.GerarToken(new Cliente { Id = idCliente, CPF = "12345678910", Email = "teste@teste.com", Nome = "teste da silva" });

            //Act
            var teste = controller.ConsultarGastos(token,idCliente);
            var okResult = teste.Result as ObjectResult;
            var consultarGastosViewModel = Assert.IsType<ConsultarGastosViewModel.Response>(okResult.Value);

            //Assert
            Assert.NotNull(teste);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            //verificar o gasto
            var gastoTest = consultarGastosViewModel.Find(g => g.Id == id);
            Assert.Equal(id, gastoTest.Id);
            Assert.Equal(idCliente, gastoTest.IdCliente);
            Assert.Equal(nomeGasto, gastoTest.NomeGasto);
            Assert.Equal(valor, gastoTest.Valor);
            Assert.Equal(5, gastoTest.QuantidadeParcelas);
            Assert.Equal(dataGasto, gastoTest.DataDoGasto);
            //verificar o gasto1
            var gastoTest1 = consultarGastosViewModel.Find(g => g.Id == id1);
            Assert.Equal(id1, gastoTest1.Id);
            Assert.Equal(idCliente, gastoTest1.IdCliente);
            Assert.Equal(nomeGasto, gastoTest1.NomeGasto);
            Assert.Equal(valor, gastoTest1.Valor);
            Assert.Equal(5, gastoTest1.QuantidadeParcelas);
            Assert.Equal(dataGasto, gastoTest1.DataDoGasto);
        }
    }
}

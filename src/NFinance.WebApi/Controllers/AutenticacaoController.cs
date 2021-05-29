﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFinance.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using NFinance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NFinance.Application.ViewModel.AutenticacaoViewModel;

namespace NFinance.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoApp _autenticacaoApp;
        private readonly ILogger<AutenticacaoController> _logger;

        public AutenticacaoController(ILogger<AutenticacaoController> logger, IAutenticacaoApp autenticacaoApp)
        {
            _logger = logger;
            _autenticacaoApp = autenticacaoApp;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginViewModel.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApplicationException), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(DomainException), (int)HttpStatusCode.InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar(LoginViewModel request)
        {
            _logger.LogInformation("Iniciando Login!");
            var response = await _autenticacaoApp.EfetuarLogin(request);
            _logger.LogInformation("Login Realizado Com Sucesso!");
            return Ok(response);
        }

        [HttpPost("Logout/{id}")]
        [ProducesResponseType(typeof(LogoutViewModel.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApplicationException), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(DomainException), (int)HttpStatusCode.InternalServerError)]
        [Authorize]
        public async Task<IActionResult> Deslogar([FromHeader] string authorization, LogoutViewModel logout)
        {
            _logger.LogInformation("Validando Bearer Token!");
            await _autenticacaoApp.ValidaTokenRequest(authorization);
            _logger.LogInformation("Bearer Token Validado!");
            _logger.LogInformation("Iniciando Logout!");
            var response = await _autenticacaoApp.EfetuarLogoff(logout);
            _logger.LogInformation("Logot Realizado Com Sucesso!");
            return Ok(response);
        }
    }
}

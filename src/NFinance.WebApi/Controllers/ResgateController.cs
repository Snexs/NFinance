﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NFinance.Domain;
using NFinance.Domain.Interfaces.Services;
using NFinance.Model.ResgatesViewModel;

namespace NFinance.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResgateController : ControllerBase
    {
        private readonly ILogger<ResgateController> _logger;
        private readonly IResgateService _resgateService;

        public ResgateController(ILogger<ResgateController> logger, IResgateService resgateService)
        {
            _logger = logger;
            _resgateService = resgateService;
        }

        [HttpGet("/api/Resgates/Listar")]
        [ProducesResponseType(typeof(ListarResgatesViewModel.Response), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Exception), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ListarResgates()
        {
            try
            {
                var response = await _resgateService.ListarResgates();
                _logger.LogInformation("Resgates Listados Com Sucesso!");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex,"Falha ao listar resgates");
                return BadRequest(ex);
            }
        }

        [HttpGet("/api/Resgate/Consultar/{id}")]
        [ProducesResponseType(typeof(Resgate), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Exception), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConsultarResgate(Guid id)
        {
            try
            {
                var resgates = await _resgateService.ConsultarResgate(id);
                _logger.LogInformation("Resgate Encontrado Com Sucesso!");
                return Ok(resgates);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex,"Falha ao consultar resgate");
                return BadRequest(ex);
            }
        }

        [HttpPost("/api/Resgate/Resgatar")]
        [ProducesResponseType(typeof(RealizarResgateViewModel.Response), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Exception), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RealizarResgate([FromBody] RealizarResgateViewModel.Request resgateRequest)
        {
            try
            {
                var resgateResponse = await _resgateService.RealizarResgate(resgateRequest);
                _logger.LogInformation("Resgate Realizado Com Sucesso!");
                return Ok(resgateResponse);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex,"Falha ao cadastrar investimento");
                return BadRequest(ex);
            }
        }
    }
}
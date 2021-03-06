﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NFinance.Domain.Interfaces.Services;
using NFinance.Domain.Services;

namespace NFinance.Domain
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterServices();
            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IGastoService, GastoService>();
            services.AddTransient<IGanhoService, GanhoService>();
            services.AddTransient<IResgateService, ResgateService>();
            services.AddTransient<IInvestimentoService, InvestimentoService>();
            services.AddTransient<ITelaInicialService, TelaInicialService>();
            services.AddTransient<IAutenticacaoService, AutenticacaoService>();
            services.AddTransient<IRedisService, RedisService>();
        }
    }
}
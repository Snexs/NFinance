﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NFinance.Domain.Interfaces.Repository
{
    public interface IResgateRepository : IDisposable
    {
        Task<Resgate> RealizarResgate(int idInvestimento, Resgate resgate);
        Task<List<Resgate>> ListarResgates();
        Task<Resgate> ConsultarResgate(int id);
    }
}

﻿using System;

namespace NFinance.ViewModel.InvestimentosViewModel
{
    public class AtualizarInvestimentoViewModel
    {
        public class Request
        {
            public Guid IdCliente { get; set; }
        
            public string NomeInvestimento { get; set; }
        
            public decimal Valor { get; set; }
        
            public DateTime DataAplicacao { get; set; }
        }
        
        public class Response : InvestimentoViewModel { };
    }
}
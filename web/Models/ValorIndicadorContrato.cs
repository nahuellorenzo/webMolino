﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class ValorIndicadorContrato
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int ContratoId { get; set; }
        public int IndicadorCalidadId { get; set; }

        public virtual Contrato Contrato { get; set; }
        public virtual IndicadorCalidad IndicadorCalidad { get; set; }
    }
}
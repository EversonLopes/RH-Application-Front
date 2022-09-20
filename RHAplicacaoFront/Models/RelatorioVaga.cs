using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RHAplicacaoFront.Models
{
    public class RelatorioVaga
    {
        public Candidato Candidato { get; set; }
        public Vaga Vaga { get; set; }

        [Display(Name = "Percentual")]
        public decimal percentual { get; set; } 
    }
}
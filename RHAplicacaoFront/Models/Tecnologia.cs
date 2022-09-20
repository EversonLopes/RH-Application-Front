using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RHAplicacaoFront.Models
{
    public class Tecnologia
    {
        [Display(Name = "Código")]
        public int TecnologiaID { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
        public List<VagasTecnologias> VagasTecnologias { get; set; }
        public List<Candidato> Candidatos { get; set; }
    }
}
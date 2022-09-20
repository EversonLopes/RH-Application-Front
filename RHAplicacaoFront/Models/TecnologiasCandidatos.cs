using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHAplicacaoFront.Models
{
    public class TecnologiasCandidatos
    {
        public int Id { get; set; }
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
        public int TecnologiaId { get; set; }
        public Tecnologia Tecnologia { get; set; }
    }
}
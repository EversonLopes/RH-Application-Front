using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RHAplicacaoFront.Models
{
    public class VagasTecnologias
    {
        public int Id { get; set; }
        public int VagaId { get; set; }
        public Vaga Vaga { get; set; }

        public int TecnologiaId { get; set; }
        public Tecnologia Tecnologia { get; set; }

        [Display(Name = "Peso da tecnologia")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [Range(0, 10, ErrorMessage = "O valor não está entre 0 e 10.")]
        public int PesoTecnologia { get; set; }
    }
}
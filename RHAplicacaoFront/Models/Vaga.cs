using RHAplicacaoFront.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RHAplicacaoFront.Models
{
    public class Vaga
    {
        [Display(Name = "Código")]
        [Key]
        public int VagaId { get; set; }

        [Display(Name = "Descrição da Vaga")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public string DescricaoVaga { get; set; }

        public List<Candidato> Candidatos { get; set; }

        [Display(Name = "Tecnologias")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public List<VagasTecnologias> VagasTecnologias { get; set; }

        public List<CheckBoxModel> CheckBoxItems { get; set; }

        public Vaga()
        {
        }
    }
}
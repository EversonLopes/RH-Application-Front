using RHAplicacaoFront.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RHAplicacaoFront.Models
{
    public class Candidato
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Tecnologias")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public List<TecnologiasCandidatos> TecnologiasCandidatos { get; set; }

        [Display(Name = "Vaga")]
        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public int VagaID_FK { get; set; }
        public virtual Vaga Vaga { get; set; }

        public List<CheckBoxModel> CheckBoxItems { get; set; }

        public Candidato()
        {
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHAplicacaoFront.Util
{
    //Modelo utilizados para fazer a listagens das tecnologias nas telas
    public class CheckBoxModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public bool Checked { get; set; }
    }
}
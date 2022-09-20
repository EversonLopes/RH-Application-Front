using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace RHAplicacaoFront.Util
{
    //Classe com as definições de url e métodos das chamadas das web api.
    public class RestApi
    {
        //Métodos dos modelo candidato
        public string ListarCandidatos = "Candidatoes";
        public string BuscarCandidato = "Candidatoes/{0}";
        public string SalvarCandidato = "Candidatoes";
        public string EditarCandidato = "Candidatoes/{0}";
        public string DeletarCandidato = "Candidatoes/{0}";

        //Métodos dos modelo tecnologia
        public string ListarTecnologias = "Tecnologias";
        public string BuscarTecnologia = "Tecnologias/{0}";
        public string SalvarTecnologia = "Tecnologias";
        public string EditarTecnologia = "Tecnologias/{0}";
        public string DeletarTecnologia = "Tecnologias/{0}";

        //Métodos dos modelo vaga
        public string ListarVagas = "Vagas";
        public string BuscarVaga = "Vagas/{0}";
        public string SalvarVaga = "Vagas";
        public string EditarVaga = "Vagas/{0}";
        public string DeletarVaga = "Vagas/{0}";

        //Método que defini a URI, que é informada no arquivo web.config e o tipo de texto que será transferido
        public HttpClient BaseUrl()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["RestApi"].ToString());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

    }
}
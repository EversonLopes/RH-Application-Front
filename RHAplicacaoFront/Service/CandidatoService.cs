using Newtonsoft.Json;
using RHAplicacaoFront.Models;
using RHAplicacaoFront.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace RHAplicacaoFront.Service
{
    //Classe responsável por fazer a comunicação com as web api, referente ao modelo candidato
    public class CandidatoService
    {
        static RestApi restApi = new RestApi();
        HttpClient baseUrl = new HttpClient();


        //Chamada do método que busca todos os candidato
        public List<Candidato> ListarCandidatos()
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.ListarCandidatos));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<List<Candidato>>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }

            }
            catch {

                throw new Exception("Erro ao pesquisar os candidatos!");
            }

            return null;
        }

        //Chamada do método que busca candidato por id
        public Candidato BuscarCandidato(int id)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.BuscarCandidato, id));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Candidato>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                        
                }

            }
            catch 
            {
                throw new Exception("Erro ao pesquisar o candidato!");
            }

            return null;
        }

        //Chamada do método que cadastra um candidato
            public Candidato IncluirCandidato(Candidato candidato)
            {
                baseUrl = restApi.BaseUrl();

                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format(baseUrl.BaseAddress + restApi.SalvarCandidato));

                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/json";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string json = serializer.Serialize(candidato);
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var retorno = JsonConvert.DeserializeObject<Candidato>(streamReader.ReadToEnd());

                        if (retorno != null)
                        {
                            return retorno;
                        }
                    }
                }
                catch
                {
                    throw new Exception("Erro ao cadastrar o candidato!");
                }

                return null;
            }

        //Chamada do método que edita um candidato
        public Candidato EditarCandidato(int id, Candidato candidato)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format(baseUrl.BaseAddress + restApi.EditarCandidato, id));

                httpWebRequest.Method = "PUT";
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(candidato);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Candidato>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }
            }
            catch
            {
                throw new Exception("Erro ao editar o candidato!");
            }
            
            return null;
        }

        //Chamada do método que exclui um candidato
        public Candidato ExcluirCandidato(int id)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.DeletarCandidato, id));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "DELETE";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Candidato>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }

                }
            }
            catch
            {
                throw new Exception("Erro ao excluir o candidato!");
            }
            
            return null;
        }
    }
}
using Newtonsoft.Json;
using RHAplicacaoFront.Models;
using RHAplicacaoFront.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace RHAplicacaoFront.Service
{
    //Classe responsável por fazer a comunicação com as web api, referente ao modelo vaga
    public class VagaService
    {
        static RestApi restApi = new RestApi();
        HttpClient baseUrl = new HttpClient();

        //Chamada do método que busca todos as vagas
        public List<Vaga> ListarVagas()
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.ListarVagas));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<List<Vaga>>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }

            }
            catch
            {

                throw new Exception("Erro ao pesquisar as vagas!");
            }

            return null;
        }

        //Chamada do método que busca vaga por id
        public Vaga BuscarVaga(int id)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.BuscarVaga, id));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Vaga>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }

                }

            }
            catch
            {
                throw new Exception("Erro ao pesquisar a vaga!");
            }

            return null;
        }

        //Chamada do método que cadastra uma vaga
        public Vaga IncluirVaga(Vaga vaga)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format(baseUrl.BaseAddress + restApi.SalvarVaga));

                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(vaga);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Vaga>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }
            }
            catch
            {
                throw new Exception("Erro ao cadastrar a vaga!");
            }

            return null;
        }

        //Chamada do método que edita uma vaga
        public Vaga EditarVaga(int id, Vaga vaga)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format(baseUrl.BaseAddress + restApi.EditarVaga, id));

                httpWebRequest.Method = "PUT";
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(vaga);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Vaga>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }
            }
            catch
            {
                throw new Exception("Erro ao editar a vaga!");
            }

            return null;
        }

        //Chamada do método que exclui uma vaga
        public Vaga ExcluirVaga(int id)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.DeletarVaga, id));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "DELETE";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Vaga>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }
            }
            catch
            {
                throw new Exception("Erro ao excluir a vaga!");
            }

            return null;
        }
    }
}
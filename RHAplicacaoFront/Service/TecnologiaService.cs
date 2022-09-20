using Newtonsoft.Json;
using RHAplicacaoFront.Models;
using RHAplicacaoFront.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace RHAplicacaoFront.Service
{
    //Classe responsável por fazer a comunicação com as web api, referente ao modelo tecnologia
    public class TecnologiaService
    {
        static RestApi restApi = new RestApi();
        HttpClient baseUrl = new HttpClient();

        //Chamada do método que busca todos as tecnologias
        public List<Tecnologia> ListarTecnologias()
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.ListarTecnologias));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<List<Tecnologia>>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }

            }
            catch
            {

                throw new Exception("Erro ao pesquisar os tecnologias!");
            }

            return null;
        }

        //Chamada do método que busca tecnologia por id
        public Tecnologia BuscarTecnologia(int id)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.BuscarTecnologia, id));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Tecnologia>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }

                }

            }
            catch
            {
                throw new Exception("Erro ao pesquisar a tecnologia!");
            }

            return null;
        }

        //Chamada do método que cadastra uma tecnologia
        public Tecnologia IncluirTecnologia(Tecnologia tecnologia)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format(baseUrl.BaseAddress + restApi.SalvarTecnologia));

                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(tecnologia);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Tecnologia>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }
            }
            catch
            {
                throw new Exception("Erro ao cadastrar a tecnologia!");
            }

            return null;
        }

        //Chamada do método que edita uma tecnologia
        public Tecnologia EditarTecnologia(int id, Tecnologia tecnologia)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(String.Format(baseUrl.BaseAddress + restApi.EditarTecnologia, id));

                httpWebRequest.Method = "PUT";
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(tecnologia);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Tecnologia>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        return retorno;
                    }
                }
            }
            catch
            {
                throw new Exception("Erro ao editar a tecnologia!");
            }

            return null;
        }

        //Chamada do método que exclui uma tecnologia
        public Tecnologia ExcluirTecnologia(int id)
        {
            baseUrl = restApi.BaseUrl();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(baseUrl.BaseAddress + restApi.DeletarTecnologia, id));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "DELETE";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<Tecnologia>(streamReader.ReadToEnd());

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
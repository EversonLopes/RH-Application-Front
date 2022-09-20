using RHAplicacaoFront.Models;
using RHAplicacaoFront.Service;
using RHAplicacaoFront.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RHAplicacaoFront.Controllers
{
    public class VagaController : Controller
    {
        //Serviços utilizados no controller
        VagaService vagaService = new VagaService();
        TecnologiaService tecnologiaService = new TecnologiaService();
        CandidatoService candidatoService = new CandidatoService();

        //Buscando todas as vagas e retornando na view Index
        // GET: Vaga
        public ActionResult Index()
        {
            var vagas = vagaService.ListarVagas();

            return View(vagas);
        }

        //Buscando as vagas, carregando a lista de check box e retornando para a view Details
        // GET: Vaga/Details/5
        public ActionResult Details(int id)
        {
            var vaga = vagaService.BuscarVaga(id); 

            vaga.CheckBoxItems = new List<CheckBoxModel>();

            foreach (var item in tecnologiaService.ListarTecnologias())
            {
                CheckBoxModel check = new CheckBoxModel();
                check.Id = item.TecnologiaID;
                check.ItemName = item.Nome;

                var existVaga = vaga.VagasTecnologias.Where(v => v.TecnologiaId == item.TecnologiaID).FirstOrDefault();

                //Se já existir cadastrado nas tecnologias da vaga, recebe checked true.
                if (existVaga != null)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }

                vaga.CheckBoxItems.Add(check);
            }

            return View(vaga);
        }

        //Chamada da view Create, para cadastrar nova vaga
        // GET: Vaga/Create
        public ActionResult Create()
        {
            Vaga vaga = new Vaga();

            vaga.VagasTecnologias = new List<VagasTecnologias>();

            vaga.CheckBoxItems = new List<CheckBoxModel>();

            //Buscando todas as tecnologias cadastradas, para retornar as opções como checkbox
            foreach (var item in tecnologiaService.ListarTecnologias())
            {
                CheckBoxModel check = new CheckBoxModel();
                check.Id = item.TecnologiaID;
                check.ItemName = item.Nome;
                check.Checked = false;

                vaga.CheckBoxItems.Add(check);
            }

            return View(vaga);
        }

        //Cadastrar o candidato
        // POST: Vaga/Create
        [HttpPost]
        public ActionResult Create(Vaga vaga)
        {
            vaga.VagasTecnologias = new List<VagasTecnologias>();

            //Fazendo a validção se foi selecionado alguma tecnolgia para a vaga
            bool selecionado = false;

            //Remove a validação do atributo VagasTecnologias caso tenha sido preenchido
            foreach (var item in vaga.CheckBoxItems)
            {
                if (item.Checked)
                {
                    ModelState.Remove("VagasTecnologias");

                    selecionado = true; 

                    break;
                }

            }

            //Se o atributo VagasTecnologias não foi preenchido, insere a validação novamente
            if (selecionado == false && !ModelState.ContainsKey("VagasTecnologias"))
            {
                ModelState.Add("VagasTecnologias", new ModelState());
            }

            if (ModelState.IsValid)
            {

                foreach (var item in vaga.CheckBoxItems)
                {
                    if (item.Checked)
                    {
                        VagasTecnologias vagaTecnologia = new VagasTecnologias();
                        vagaTecnologia.VagaId = vaga.VagaId;
                        vagaTecnologia.TecnologiaId = item.Id;

                        vaga.VagasTecnologias.Add(vagaTecnologia);
                    }
                }

                //Efetivando a inclusão da vaga, utilizando o serviço de edição
                var vagaCreate = vagaService.IncluirVaga(vaga);

                return RedirectToAction(nameof(Index));
            }

            return View(vaga);
        }

        //Chamada da view Edit, para editar uma vaga
        // GET: Vaga/Edit/5
        public ActionResult Edit(int id)
        {
            //Realizando a busca do candidato, utilizando o serviço de busca
            var vagaEdit = vagaService.BuscarVaga(id);

            vagaEdit.CheckBoxItems = new List<CheckBoxModel>();

            foreach (var item in tecnologiaService.ListarTecnologias())
            {
                CheckBoxModel check = new CheckBoxModel();
                check.Id = item.TecnologiaID;
                check.ItemName = item.Nome;

                //Se já existir cadastrado nas tecnologias da vaga, recebe checked true
                var existVaga = vagaEdit.VagasTecnologias.Where(v => v.TecnologiaId == item.TecnologiaID).FirstOrDefault();

                if (existVaga != null)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }

                vagaEdit.CheckBoxItems.Add(check);
            }

            if (vagaEdit != null)
            {
                return View(vagaEdit);
            }
            return View(nameof(Index));
        }

        //Editar o candidato
        // POST: Vaga/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Vaga vaga)
        {
            vaga.VagasTecnologias = new List<VagasTecnologias>();

            //Fazendo a validção se foi selecionado alguma tecnolgia para o candidato
            bool selecionado = false;

            //Remove a validação do atributo VagasTecnologias caso tenha sido preenchido
            foreach (var item in vaga.CheckBoxItems)
            {
                if (item.Checked)
                {
                    ModelState.Remove("VagasTecnologias");

                    selecionado = true;

                    break;
                }

            }

            //Se o atributo VagasTecnologias não foi preenchido, insere a validação novamente
            if (selecionado == false && !ModelState.ContainsKey("VagasTecnologias"))
            {
                ModelState.Add("VagasTecnologias", new ModelState());
            }

            if (ModelState.IsValid)
            {

                foreach (var item in vaga.CheckBoxItems)
                {
                    if (item.Checked)
                    {
                        VagasTecnologias vagaTecnologia = new VagasTecnologias();
                        vagaTecnologia.VagaId = vaga.VagaId;
                        vagaTecnologia.TecnologiaId = item.Id;

                        vaga.VagasTecnologias.Add(vagaTecnologia);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                //Efetivando a edição da vaga, utilizando o serviço de edição
                var vagaEdit = vagaService.EditarVaga(id, vaga);

                return RedirectToAction(nameof(Index));
            }

            return View(vaga);
        }

        //Chamada da view Delete, para exclusão de uma vaga
        // GET: Vaga/Delete/5
        public ActionResult Delete(int id)
        {
            var vagaDelete = vagaService.BuscarVaga(id);

            vagaDelete.CheckBoxItems = new List<CheckBoxModel>();

            //Buscar as lista de tecnologias
            foreach (var item in tecnologiaService.ListarTecnologias())
            {
                CheckBoxModel check = new CheckBoxModel();
                check.Id = item.TecnologiaID;
                check.ItemName = item.Nome;

                var existVaga = vagaDelete.VagasTecnologias.Where(v => v.TecnologiaId == item.TecnologiaID).FirstOrDefault();

                //Se estiver nas tecnologias da vaga, recebe checked true
                if (existVaga != null)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }

                vagaDelete.CheckBoxItems.Add(check);
            }

            return View(vagaDelete);
        }

        //Excluir o candidato
        // POST: Vaga/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Efetivando a exclusão do candidato, utilizando o serviço de exclusão
                var vagaDelete = vagaService.ExcluirVaga(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Chamda da view Triagem, no qual será cadastrados o peso de cada tecnologia por vaga
        public ActionResult Triagem(int id)
        {
            //Busca a vaga
            var vaga = vagaService.BuscarVaga(id);

            //Atualiza os dados da lista referente a objeto vaga. 
            for (int i = 0; i < vaga.VagasTecnologias.Count; i++)
            {
                vaga.VagasTecnologias[i].Vaga = new Vaga();
                vaga.VagasTecnologias[i].Vaga.VagaId = vaga.VagaId;
                vaga.VagasTecnologias[i].Vaga.DescricaoVaga = vaga.DescricaoVaga;

                i = i++;
            }

            var lista = vaga.VagasTecnologias;

            //retorna a lista de tecnologias da vaga, para ser apresentada na view, e ter a opções de cadastro de peso.
            return View(lista);
        }

        //Salva o peso da tecnologia por vaga, recebe um formcollection como parâmetro, por ser uma lista no form da view.
        [HttpPost]
        public ActionResult Triagem(FormCollection formCollection)
        {
            if(ModelState.IsValid)
            {
                List<VagasTecnologias> lista = new List<VagasTecnologias>();

                //Setando em lista os parametros recebidos por parâmetro.
                var tecnologias = formCollection["item.TecnologiaId"].Split(',').ToList();
                var pesos = formCollection["item.PesoTecnologia"].Split(',').ToList();

                //Transformando as lista criadas anteriormente no objeto vagasTecnologias
                for (int i = 0; i < tecnologias.Count(); i++)
                {
                    VagasTecnologias vagasTecnologias = new VagasTecnologias();

                    vagasTecnologias.VagaId = Convert.ToInt32(formCollection["Vaga.VagaId"]);
                    vagasTecnologias.TecnologiaId = Convert.ToInt32(tecnologias[i]);
                    vagasTecnologias.PesoTecnologia = Convert.ToInt32(pesos[i]);

                    //Inserindo o objeto criado na lista.
                    lista.Add(vagasTecnologias);

                    i = i++;
                }

                var vaga = vagaService.BuscarVaga(Convert.ToInt32(formCollection["Vaga.VagaId"]));

                //Setando a nova lista criada, com os objetos já convertidos na vaga  que está no processo de triagem.
                vaga.VagasTecnologias = lista;

                //Atualizando os dados que foram alterados, utilizando o serviço de edição
                var vagaEdit = vagaService.EditarVaga(vaga.VagaId, vaga);

                return RedirectToAction(nameof(Index));
            }
            
            return View("Index");
        }

        //Chamda da view Relatorio, no qual será apresentados os candidatos ordenandos por percentual de nível de conhecimento
        public ActionResult Relatorio(int id)
        {
            List<RelatorioVaga> listaRelaorio = new List<RelatorioVaga>();

            var vaga = vagaService.BuscarVaga(id);

            //Realização do loop por candidato
            foreach (var item in vaga.Candidatos)
            {
                //Instanciando novo objeto relatório
                RelatorioVaga relatorio = new RelatorioVaga();

                //Setando vaga e candidato
                relatorio.Vaga = vaga;
                relatorio.Candidato = item;

                //Buscando as vagas do candidato
                var tecnologiasCandidato = candidatoService.BuscarCandidato(item.Id);

                int somaTecnologiasVaga = 0;
                int somaTecnologiasCandidato = 0;

                //Realizando o soma do total de peso do candidatos referente a vaga que será gerado o relatório
                foreach (var i in tecnologiasCandidato.TecnologiasCandidatos)
                {
                    var tecnologiaExist = vaga.VagasTecnologias.Where(vt => vt.TecnologiaId == i.TecnologiaId).FirstOrDefault();
                    //Se a tecnologia do candidato for pre requisito da vaga, soma a pontos
                    if (tecnologiaExist != null)
                    {
                        somaTecnologiasCandidato = somaTecnologiasCandidato + tecnologiaExist.PesoTecnologia;
                    }
                }

                //Realizando o soma do total de peso das tecnologias da vaga.
                foreach (var i in vaga.VagasTecnologias)
                {

                    somaTecnologiasVaga = somaTecnologiasVaga + i.PesoTecnologia;
                }

                //Setando e realizando o cálculo do percentual do candidato referente a vaga que será gerado o relatório

                if(somaTecnologiasVaga != 0) { //Se não foi setado peso para as tecnologias, impede a divisão por zero
                relatorio.percentual = (somaTecnologiasCandidato * 100) / somaTecnologiasVaga;
                    }
                listaRelaorio.Add(relatorio);

            }

            var listaOrdenada = listaRelaorio.OrderByDescending(r => r.percentual).ToList();

            return View(listaOrdenada);
        }
    }
}

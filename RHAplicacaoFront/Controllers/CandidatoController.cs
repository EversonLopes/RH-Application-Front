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
    public class CandidatoController : Controller
    {
        //Serviços utilizados no controller
        CandidatoService candidatoService = new CandidatoService();
        VagaService vagasService = new VagaService();
        TecnologiaService tecnologiaService = new TecnologiaService();

        //Buscando todos os candidatos e retornando na view Index
        // GET: Candidato
        public ActionResult Index()
        {
            List<Candidato> listaCandidatos = new List<Candidato>();

            var candidatos = candidatoService.ListarCandidatos();

            return View(candidatos);
        }

        //Buscando o candidatos, carregando a lista de check box e retornando para a view Details
        // GET: Candidato/Details/5
        public ActionResult Details(int id)
        {
            var candidato = candidatoService.BuscarCandidato(id);

            candidato.CheckBoxItems = new List<CheckBoxModel>();

            foreach (var item in tecnologiaService.ListarTecnologias())
            {
                CheckBoxModel check = new CheckBoxModel();
                check.Id = item.TecnologiaID;
                check.ItemName = item.Nome;

                var existCandidato = candidato.TecnologiasCandidatos.Where(v => v.TecnologiaId == item.TecnologiaID).FirstOrDefault();

                //Se já existis cadastrado nas tecnologias do candidato, recebe checked true.
                if (existCandidato != null)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }

                candidato.CheckBoxItems.Add(check);
            }

            return View(candidato);
        }

        //Chamada da view Create, para cadastrar novo candidato
        // GET: Candidato/Create
        public ActionResult Create()
        {
            //Preenchedo a ViewBag com as vagas, para carregar no dropdownlist
            ViewBag.Vagas = new SelectList(vagasService.ListarVagas(), "VagaId", "DescricaoVaga");

            Candidato candidato = new Candidato();

            candidato.TecnologiasCandidatos = new List<TecnologiasCandidatos>();

            candidato.CheckBoxItems = new List<CheckBoxModel>();

            //Buscando todas as tecnologias cadastradas, para retornar as opções como checkbox
            foreach (var item in tecnologiaService.ListarTecnologias())
            {
                CheckBoxModel check = new CheckBoxModel();
                check.Id = item.TecnologiaID;
                check.ItemName = item.Nome;
                check.Checked = false;

                candidato.CheckBoxItems.Add(check);
            }

            return View(candidato);
        }

        //Cadastrar o candidato
        // POST: Candidato/Create
        [HttpPost]
        public ActionResult Create(Candidato candidato)
        {
            candidato.TecnologiasCandidatos = new List<TecnologiasCandidatos>();

            //Fazendo a validção se foi selecionado alguma tecnolgia para o candidato
            bool selecionado = false;

            //Remove a validação do atributo TecnologiasCandidatos caso tenha sido preenchido
            foreach (var item in candidato.CheckBoxItems)
            {
                if (item.Checked)
                {
                    ModelState.Remove("TecnologiasCandidatos");

                    selecionado = true;

                    break;
                }

            }

            //Se o atributo TecnologiasCandidatos não foi preenchido, insere a validação novamente
            if (selecionado == false && !ModelState.ContainsKey("TecnologiasCandidatos"))
            {
                ModelState.Add("TecnologiasCandidatos", new ModelState());
            }

            if (ModelState.IsValid)
            {
                foreach (var item in candidato.CheckBoxItems)
                {
                    if (item.Checked)
                    {
                        TecnologiasCandidatos tecnologiasCandidatos = new TecnologiasCandidatos();
                        tecnologiasCandidatos.CandidatoId = candidato.Id;
                        tecnologiasCandidatos.TecnologiaId = item.Id;

                        candidato.TecnologiasCandidatos.Add(tecnologiasCandidatos);
                    }
                }

                //Efetivando o cadastro do candidato, utilizando o serviço de inclusão
                var candidatoCreate = candidatoService.IncluirCandidato(candidato);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Vagas = new SelectList(vagasService.ListarVagas(), "VagaId", "DescricaoVaga");

            return View(candidato);
        }

        //Chamada da view Edit, para editar um candidato
        // GET: Candidato/Edit/5
        public ActionResult Edit(int id)
        {
            //Preenchedo a ViewBag com as vagas, para carregar no dropdownlist
            ViewBag.Vagas = new SelectList(vagasService.ListarVagas(), "VagaId", "DescricaoVaga");

            //Realizando a busca do candidato, utilizando o serviço de busca
            var candidatoEdit = candidatoService.BuscarCandidato(id);

            candidatoEdit.CheckBoxItems = new List<CheckBoxModel>();

            foreach (var item in tecnologiaService.ListarTecnologias())
            {
                CheckBoxModel check = new CheckBoxModel();
                check.Id = item.TecnologiaID;
                check.ItemName = item.Nome;
                
                //Se já existir cadastrado nas tecnologias do candidato, recebe checked true
                var existCandidato = candidatoEdit.TecnologiasCandidatos.Where(v => v.TecnologiaId == item.TecnologiaID).FirstOrDefault();

                if (existCandidato != null)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }

                candidatoEdit.CheckBoxItems.Add(check);
            }

            if (candidatoEdit != null)
            {
                return View(candidatoEdit);
            }
            return View(nameof(Index));
        }

        //Editar o candidato
        // POST: Candidato/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Candidato candidato)
        {
            candidato.TecnologiasCandidatos = new List<TecnologiasCandidatos>();

            //Fazendo a validção se foi selecionado alguma tecnolgia para o candidato
            bool selecionado = false;

            //Remove a validação do atributo TecnologiasCandidatos caso tenha sido preenchido
            foreach (var item in candidato.CheckBoxItems)
            {
                if (item.Checked)
                {
                    ModelState.Remove("TecnologiasCandidatos");

                    selecionado = true;

                    break;
                }
            }

            //Se o atributo TecnologiasCandidatos não foi preenchido, insere a validação novamente
            if (selecionado == false && !ModelState.ContainsKey("TecnologiasCandidatos"))
            {
                ModelState.Add("TecnologiasCandidatos", new ModelState());
            }

            if (ModelState.IsValid)
            {

                foreach (var item in candidato.CheckBoxItems)
                {
                    if (item.Checked)
                    {
                        TecnologiasCandidatos tecnologiasCandidatos = new TecnologiasCandidatos();
                        tecnologiasCandidatos.CandidatoId = candidato.Id;
                        tecnologiasCandidatos.TecnologiaId = item.Id;

                        candidato.TecnologiasCandidatos.Add(tecnologiasCandidatos);
                    }
                }
            }
            
            if (ModelState.IsValid)
            {
                //Efetivando a edição do candidato, utilizando o serviço de edição
                var candidatoEdit = candidatoService.EditarCandidato(id, candidato);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Vagas = new SelectList(vagasService.ListarVagas(), "VagaId", "DescricaoVaga");

            return View(candidato);
        }

        //Chamada da view Delete, para exclusão de um candidato
        // GET: Candidato/Delete/5
        public ActionResult Delete(int id)
        {
            var candidatoDelete = candidatoService.BuscarCandidato(id);

            candidatoDelete.CheckBoxItems = new List<CheckBoxModel>();

            //Buscar as lista de tecnologias
            foreach (var item in tecnologiaService.ListarTecnologias())
            {
                CheckBoxModel check = new CheckBoxModel();
                check.Id = item.TecnologiaID;
                check.ItemName = item.Nome;

                var existCandidato = candidatoDelete.TecnologiasCandidatos.Where(v => v.TecnologiaId == item.TecnologiaID).FirstOrDefault();

                //Se estiver nas tecnologias do candidato, recebe checked true
                if (existCandidato != null)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }

                candidatoDelete.CheckBoxItems.Add(check);
            }

            if (candidatoDelete != null)
            {
                return View(candidatoDelete);
            }
            return View(nameof(Index));

        }

        //Excluir o candidato
        // POST: Candidato/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Candidato collection)
        {
            try
            {
                // Efetivando a exclusão do candidato, utilizando o serviço de exclusão
                var candidatoDelete = candidatoService.ExcluirCandidato(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

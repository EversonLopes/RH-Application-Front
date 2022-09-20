using RHAplicacaoFront.Models;
using RHAplicacaoFront.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RHAplicacaoFront.Controllers
{
    
    public class TecnologiaController : Controller
    {
        //Serviços utilizados no controller
        TecnologiaService tecnologiaService = new TecnologiaService();

        //Buscando todos os tecnologias e retornando na view Index
        // GET: Tecnologia
        [HttpGet]
        public ActionResult Index()
        {
            var tecnologias = tecnologiaService.ListarTecnologias();

            return View(tecnologias);
        }

        //Buscando a tecnologia e retornando para a view Details
        // GET: Tecnologia/Details/5
        public ActionResult Details(int id)
        {
            return View(tecnologiaService.BuscarTecnologia(id));
        }

        //Chamada da view Create, para cadastrar nova tecnologia
        // GET: Tecnologia/Create
        public ActionResult Create()
        {
            return View(nameof(Create));
        }

        //Cadastrar o candidato
        // POST: Tecnologia/Create
        [HttpPost]
        public ActionResult Create(Tecnologia tecnologia)
        {
            if (ModelState.IsValid)
            {
                //Efetivando o cadastro da tecnologia, utilizando o serviço de inclusão
                var tecnologiaCreate = tecnologiaService.IncluirTecnologia(tecnologia);

                return RedirectToAction(nameof(Index));
            }

            return View(tecnologia);

        }

        //Chamada da view Edit, para editar uma tecnologia
        // GET: Tecnologia/Edit/5
        public ActionResult Edit(int id)
        {
            var tecnologiatoEdit = tecnologiaService.BuscarTecnologia(id);

            if (tecnologiatoEdit != null)
            {
                return View(tecnologiatoEdit);
            }
            return View(nameof(Index));
        }

        //Editar a tecnologia
        // POST: Tecnologia/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tecnologia tecnologia)
        {
            if (ModelState.IsValid)
            {
                //Efetivando a edição da tecnologia, utilizando o serviço de edição
                var tecnologiaEdit = tecnologiaService.EditarTecnologia(id, tecnologia);

                return RedirectToAction(nameof(Index));
            }

            return View(tecnologia);
        }

        //Chamada da view Delete, para exclusão de uma tecnologia
        // GET: Tecnologia/Delete/5
        public ActionResult Delete(int id)
        {
            var tecnoligiaDelete = tecnologiaService.BuscarTecnologia(id);

            return View(tecnoligiaDelete);
        }

        //Excluir a tecnologia
        // POST: Tecnologia/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Candidato collection)
        {
            try
            {
                // Efetivando a exclusão da tecnologia, utilizando o serviço de exclusão
                var tecnologiaDelete = tecnologiaService.ExcluirTecnologia(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

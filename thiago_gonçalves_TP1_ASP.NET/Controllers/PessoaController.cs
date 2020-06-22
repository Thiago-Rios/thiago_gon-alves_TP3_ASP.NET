using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using thiago_gonçalves_TP1_ASP.NET.Dados;
using thiago_gonçalves_TP1_ASP.NET.Models;
using thiago_gonçalves_TP1_ASP.NET.Repository;

namespace thiago_gonçalves_TP1_ASP.NET.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaRepository PessoaRepository { get; set; }

        public PessoaController(PessoaRepository pessoaRepository)
        {
            this.PessoaRepository = pessoaRepository;
        }

        // GET: Pessoa
        [Route("Pessoas/Aniversariantes")]
        public ActionResult AniversariantesDoDia()
        {
            DateTime dataDeHoje = DateTime.Today;
            var pessoa = PessoaRepository.GetAll().Where(pessoa => pessoa.DataDeAniversario.Day.Equals(dataDeHoje.Day) && pessoa.DataDeAniversario.Month.Equals(dataDeHoje.Month));

            return View(pessoa);
        }

        // GET: Pessoa
        [Route("Pessoas/")]
        public ActionResult Pessoas()
        {
            var pessoa = PessoaRepository.ListaOrdenada();
            return View(pessoa);
        }

        // GET: Pessoa/Details/5
        [Route("Pessoas/Mostrar/{id}")]
        public ActionResult MostrarPessoa(int id)
        {
            var pessoa = this.PessoaRepository.GetById(id);
            return View(pessoa);
        }

        //GET: Pessoa/Buscar
        [Route("Pessoas/Buscar")]
        public ActionResult BuscarPeloNome(string nome)
        {
            var pessoa = PessoaRepository.GetByName(nome);
            return View(pessoa);
        }

        // GET: Pessoa/Create
        [Route("Pessoas/Adiciona")]
        public ActionResult AdicionaPessoa()
        {
            return View();
        }

        // POST: Pessoa/Create
        [Route("Pessoas/Adiciona")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionaPessoa(PessoaModel pessoa)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View();

                pessoa.DiasRestantes = pessoa.ProximoAniversario();
                PessoaRepository.Save(pessoa);

                return RedirectToAction(nameof(Pessoas));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Edit/5
        [Route("Pessoas/Editar/{id}")]
        public ActionResult EditarPessoa(int id)
        { 
            var pessoa = this.PessoaRepository.GetById(id);

            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        [Route("Pessoas/Editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPessoa(int id, PessoaModel pessoa)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View();

                var pessoaEdit = PessoaRepository.GetById(id);

                pessoaEdit.Nome = pessoa.Nome;
                pessoaEdit.DataDeAniversario = pessoa.DataDeAniversario;

                PessoaRepository.Update(pessoaEdit);
                return RedirectToAction(nameof(Pessoas));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Delete/5
        [Route("Pessoas/Deletar/{id}")]
        public ActionResult DeletarPessoa(int id)
        {
            var pessoa = this.PessoaRepository.GetById(id);
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [Route("Pessoas/Deletar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarPessoa(PessoaModel pessoa)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View();

                PessoaRepository.Delete(pessoa);

                return RedirectToAction(nameof(Pessoas));
            }
            catch
            {
                return View();
            }
        }
    }
}
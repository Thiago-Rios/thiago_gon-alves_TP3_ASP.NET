using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using thiago_gonçalves_TP1_ASP.NET.Dados;
using thiago_gonçalves_TP1_ASP.NET.Models;

namespace thiago_gonçalves_TP1_ASP.NET.Controllers
{
    public class PessoaController : Controller
    {
        private static readonly List<PessoaModel> pessoas = new List<PessoaModel>();
        BancoDeDados banco = new BancoDeDados(pessoas);

        // GET: Pessoa
        [Route("Pessoas/")]
        public ActionResult Pessoas()
        {
            var pessoa = banco.Listar();
            return View(pessoa);
        }

        // GET: Pessoa/Details/5
        [Route("Pessoas/Mostrar/{id}")]
        public ActionResult MostrarPessoa(int id)
        {
            foreach (var pessoa in pessoas)
            {
                if (pessoa.Id == id)
                {
                    return View(pessoa);
                }
            }
            return View();
        }

        //GET: Pessoa/Buscar
        [Route("Pessoas/Buscar")]
        public ActionResult BuscarPeloNome()
        {
            var pessoa = pessoas.Where(pessoa => pessoa.Nome.Contains(HttpContext.Request.Form["Nome"], StringComparison.InvariantCultureIgnoreCase));
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
                if(pessoas.Count >= 1)
                {
                    foreach(var item in pessoas)
                    {
                        PessoaModel ultimoNaLista = pessoas.Last(x => x.Id == item.Id);
                        pessoa.Id = ultimoNaLista.Id + 1;
                    }
                }
                pessoas.Add(pessoa);

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
           foreach(var pessoa in pessoas)
           {
                if (pessoa.Id == id)
                {
                    return View(pessoa);
                }
           }
            return View();
        }

        // POST: Pessoa/Edit/5
        [Route("Pessoas/Editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPessoa(int id, PessoaModel pessoa)
        {
            try
            {
                foreach(var item in pessoas)
                {
                    if (item.Id == id)
                    {
                        item.Nome = HttpContext.Request.Form["Nome"];
                        item.DataDeAniversario = DateTime.Parse(HttpContext.Request.Form["DataDeAniversario"]);
                    }
                }

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
            foreach (var pessoa in pessoas)
            {
                if (pessoa.Id == id)
                {
                    return View(pessoa);
                }
            }
            return View();
        }

        // POST: Pessoa/Delete/5
        [Route("Pessoas/Deletar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarPessoa(int id, PessoaModel pessoa)
        {
            try
            {
                pessoas.RemoveAll(x => x.Id == pessoa.Id);

                return RedirectToAction(nameof(Pessoas));
            }
            catch
            {
                return View();
            }
        }
    }
}
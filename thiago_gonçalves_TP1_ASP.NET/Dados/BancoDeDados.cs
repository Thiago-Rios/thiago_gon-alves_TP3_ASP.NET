using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thiago_gonçalves_TP1_ASP.NET.Models;

namespace thiago_gonçalves_TP1_ASP.NET.Dados
{
    public class BancoDeDados
    {
        List<PessoaModel> pessoas = new List<PessoaModel>();

        public BancoDeDados(List<PessoaModel> pessoas)
        {
            this.pessoas = pessoas;
        }

        public IEnumerable<PessoaModel> Listar()
        {
            return pessoas;
        }
    }
}

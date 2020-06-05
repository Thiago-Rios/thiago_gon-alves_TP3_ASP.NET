using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace thiago_gonçalves_TP1_ASP.NET.Models
{
    public class PessoaModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeAniversario { get; set; }
        public int ProximoAniversario()
        {
            DateTime momento = DateTime.Today;
            DateTime dataAniversario = new DateTime(momento.Year, DataDeAniversario.Month, DataDeAniversario.Day);

            if (dataAniversario < momento)
            {
                dataAniversario = dataAniversario.AddYears(1);
            }

            int diferancaData = (dataAniversario - momento).Days;
            return diferancaData;
        }
    }
}

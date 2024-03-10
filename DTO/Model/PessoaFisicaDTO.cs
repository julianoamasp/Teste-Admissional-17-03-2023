using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class PessoaFisicaDTO
    {
        public int PessoaId { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public decimal ValorRenda { get; set; }
        public string CPF { get; set; } = string.Empty;
    }
}

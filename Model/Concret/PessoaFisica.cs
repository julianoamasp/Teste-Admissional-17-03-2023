using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Abstract;

namespace Model.Concret
{
    [Table("Pessoa")]
    public class PessoaFisica : Pessoa
    {
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorRenda { get; set; }

        [Required]
        [MaxLength(11)]
        public string CPF { get; set; } = string.Empty;
    }
}

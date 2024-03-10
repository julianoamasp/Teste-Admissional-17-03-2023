using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Abstract
{
    public abstract class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "Date")]
        public DateTime DataNascimento { get; set; }
    }
}

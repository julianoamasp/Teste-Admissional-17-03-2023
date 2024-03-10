using DTO.Model;
using EntityFramework.Context;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Concret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class PessoaFisicaService
    {
        public List<PessoaFisicaDTO> All()
        {
            SistemaContext ctx = new SistemaContext();
            var res = ctx.PessoaFisica.Select(e => new PessoaFisicaDTO()
            {
                PessoaId = e.PessoaId,
                NomeCompleto = e.NomeCompleto,
                DataNascimento = e.DataNascimento,
                ValorRenda = e.ValorRenda,
                CPF = e.CPF
            }).ToList();
            return res;
        }
        public bool Insert(PessoaFisicaDTO pessoaDto)
        {
            bool adicionado = false;
            var pessoa = new PessoaFisica()
            {
                NomeCompleto = pessoaDto.NomeCompleto,
                CPF = pessoaDto.CPF,
                DataNascimento = pessoaDto.DataNascimento,
                ValorRenda = pessoaDto.ValorRenda
            };
            SistemaContext ctx = new SistemaContext();
            ctx.PessoaFisica.Add(pessoa);
            ctx.SaveChanges();
            adicionado = true;
            return adicionado;
        }
        public bool Update(PessoaFisicaDTO pessoaDto)
        {
            bool atualizado = false;
            SistemaContext ctx = new SistemaContext();
            var pessoaBd = ctx.PessoaFisica.Where(e => e.PessoaId == pessoaDto.PessoaId).FirstOrDefault();
            if(pessoaBd != null)
            {
                pessoaBd.NomeCompleto = pessoaDto.NomeCompleto;
                pessoaBd.CPF = pessoaDto.CPF;
                pessoaBd.DataNascimento = pessoaDto.DataNascimento;
                pessoaBd.ValorRenda = pessoaDto.ValorRenda;
                ctx.Update(pessoaBd);
                ctx.SaveChanges();
                atualizado = true;
            }
            return atualizado;
        }
        public bool Delete(int id)
        {
            bool deletado =false;
            SistemaContext ctx = new SistemaContext();
            var pessoaBd = ctx.PessoaFisica.Where(e => e.PessoaId == id).FirstOrDefault();
            if (pessoaBd != null)
            {
                ctx.Remove(pessoaBd);
                ctx.SaveChanges();
                deletado = true;
            }
            return deletado;
        }


        public Table<List<PessoaFisicaDTO>> Table(Table<PessoaFisicaDTO> table)
        {
            Table<List<PessoaFisicaDTO>> tabela = new Table<List<PessoaFisicaDTO>>();
            tabela.busca = table.busca;
            tabela.pagina = table.pagina;
            tabela.quantidadePorPagina = table.quantidadePorPagina;
            tabela.quantidadePaginas = table.quantidadePaginas;
            tabela.total = table.total;

            SistemaContext ctx = new SistemaContext();
            var query = ctx.PessoaFisica.AsQueryable();
            if (!string.IsNullOrEmpty(table.busca))
            {
                query = query.Where(e => e.NomeCompleto.Contains(table.busca));
            }

            tabela.total = query.Count();
            tabela.quantidadePaginas = (int)Math.Ceiling((decimal)query.Count() / table.quantidadePorPagina);

            tabela.data = query
                .Skip(table.pagina * table.quantidadePorPagina)
                .Take(table.quantidadePorPagina)
                .Select(e => new PessoaFisicaDTO()
                {
                    PessoaId = e.PessoaId,
                    NomeCompleto = e.NomeCompleto,
                    DataNascimento = e.DataNascimento,
                    ValorRenda = e.ValorRenda,
                    CPF = e.CPF
                })
                .ToList();

            return tabela;
        }
    }
    public class Table<T>
    {
        public int pagina { get; set; }
        public int quantidadePorPagina { get; set; }
        public int quantidadePaginas { get; set; }
        public int total { get; set; }
        public string busca { get; set; } = string.Empty;
        public T? data {get;set;}
    }
    
}

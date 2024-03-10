using Business.Service;
using DTO.Model;
using EntityFramework.Context;
using Model.Concret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Xunit;


namespace Test.Service
{
    public class PessoaFisicaServiceTest
    {


        private DbContextOptions<SistemaContext> GetDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<SistemaContext>()
                .UseSqlServer(dbName)
                .Options;
        }

        [Fact]
        public void All_ShouldReturnEmptyListWhenNoData()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            var dbContextOptions = GetDbOptions(dbName);

            using (var context = new SistemaContext())
            {
                var service = new PessoaFisicaService();

                // Act
                var result = service.All();

                // Assert
                Assert.Empty(result);
            }
        }

        [Fact]
        public void Insert_ShouldAddNewPersonToDatabase()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            var dbContextOptions = GetDbOptions(dbName);

            using (var context = new SistemaContext())
            {
                var service = new PessoaFisicaService();
                var pessoaDto = new PessoaFisicaDTO
                {
                    NomeCompleto = "John Doe",
                    CPF = "12345678901",
                    DataNascimento = DateTime.Now,
                    ValorRenda = 5000.00m
                };

                // Act
                var result = service.Insert(pessoaDto);

                // Assert
                Assert.True(result);
                Assert.Equal(1, context.PessoaFisica.Count());
            }
        }

        [Fact]
        public void Update_ShouldUpdateExistingPersonInDatabase()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            var dbContextOptions = GetDbOptions(dbName);

            using (var context = new SistemaContext())
            {
                var service = new PessoaFisicaService();
                var pessoaDto = new PessoaFisicaDTO
                {
                    PessoaId = 1,
                    NomeCompleto = "John Doe",
                    CPF = "12345678901",
                    DataNascimento = DateTime.Now,
                    ValorRenda = 5000.00m
                };

                context.PessoaFisica.Add(new PessoaFisica
                {
                    PessoaId = 1,
                    NomeCompleto = "Jane Doe",
                    CPF = "98765432109",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    ValorRenda = 6000.00m
                });

                context.SaveChanges();

                // Act
                var result = service.Update(pessoaDto);

                // Assert
                Assert.True(result);
                var updatedPerson = context.PessoaFisica.Find(1);
                Assert.Equal("John Doe", updatedPerson.NomeCompleto);
                Assert.Equal("12345678901", updatedPerson.CPF);
                Assert.Equal(5000.00m, updatedPerson.ValorRenda);
            }
        }

        [Fact]
        public void Delete_ShouldRemovePersonFromDatabase()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            var dbContextOptions = GetDbOptions(dbName);

            using (var context = new SistemaContext())
            {
                var service = new PessoaFisicaService();

                context.PessoaFisica.Add(new PessoaFisica
                {
                    PessoaId = 1,
                    NomeCompleto = "John Doe",
                    CPF = "12345678901",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    ValorRenda = 5000.00m
                });

                context.SaveChanges();

                // Act
                var result = service.Delete(1);

                // Assert
                Assert.True(result);
                Assert.Empty(context.PessoaFisica);
            }
        }

        [Fact]
        public void Table_ShouldReturnFilteredAndPagedData()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            var dbContextOptions = GetDbOptions(dbName);

            using (var context = new SistemaContext())
            {
                var service = new PessoaFisicaService();

                // Adiciona algumas pessoas fictícias para testar a paginação e filtragem
                context.PessoaFisica.AddRange(
                    new PessoaFisica
                    {
                        NomeCompleto = "John Doe",
                        CPF = "12345678901",
                        DataNascimento = DateTime.Now.AddYears(-30),
                        ValorRenda = 5000.00m
                    },
                    new PessoaFisica
                    {
                        NomeCompleto = "Jane Doe",
                        CPF = "98765432109",
                        DataNascimento = DateTime.Now.AddYears(-25),
                        ValorRenda = 6000.00m
                    },
                    new PessoaFisica
                    {
                        NomeCompleto = "Bob Smith",
                        CPF = "55555555555",
                        DataNascimento = DateTime.Now.AddYears(-40),
                        ValorRenda = 7000.00m
                    });

                context.SaveChanges();

                var tableRequest = new Table<PessoaFisicaDTO>
                {
                    busca = "Doe",
                    pagina = 0,
                    quantidadePorPagina = 1
                };

                // Act
                var result = service.Table(tableRequest);

                // Assert
                Assert.Equal(2, result.total);
                Assert.Equal(2, result.quantidadePaginas);
                Assert.Single(result.data);
                Assert.Equal("John Doe", result.data[0].NomeCompleto);
            }
        }
    }

}
using Business.Service;
using DTO.Model;
using Microsoft.AspNetCore.Mvc;

namespace Teste.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PessoafisicaController : ControllerBase
    {
        [HttpGet]
        public IActionResult All()
        {
            try
            {
                PessoaFisicaService pes = new PessoaFisicaService();
                return Ok(pes.All());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost]
        [Route("table")]
        public IActionResult Table(Table<PessoaFisicaDTO> table)
        {
            try
            {
                PessoaFisicaService pes = new PessoaFisicaService();
                return Ok(pes.Table(table));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost]
        public IActionResult Insert(PessoaFisicaDTO pessoaFisicaDTO)
        {
            try
            {
                PessoaFisicaService pes = new PessoaFisicaService();
                if (pes.Insert(pessoaFisicaDTO))
                {
                    return StatusCode(200);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
        [HttpPut]
        public IActionResult Update(PessoaFisicaDTO pessoaFisicaDTO)
        {
            try
            {
                PessoaFisicaService pes = new PessoaFisicaService();
                if (pes.Update(pessoaFisicaDTO))
                {
                    return StatusCode(200);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
        [HttpDelete]
        public IActionResult Delete(PessoaFisicaDTO pessoaFisicaDTO)
        {
            try
            {
                PessoaFisicaService pes = new PessoaFisicaService();
                if(pes.Delete(pessoaFisicaDTO.PessoaId))
                {
                    return StatusCode(200);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
using ConsoleApp.DOMAIN.Dto;
using ConsoleApp.DOMAIN.Entites;
using ConsoleApp.DOMAIN.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly RegistroEmprestimoService service;

        public EmprestimoController(RegistroEmprestimoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.AllEmprestimo());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.ListarPorId(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] DtoEmprestimo body)
        {
            var result = service.CadastraEmprestimo(body, out List<MensagemErro> msg);

            if (!result)
            {
                return UnprocessableEntity(msg);
            }

            return Ok(body);
        }
        [HttpPut]
        public IActionResult Put([FromBody] DtoEmprestimo body)
        {
            var result = service.EditarEmprestimo(body, out List<MensagemErro> msg);

            if (!result)
            {
                return UnprocessableEntity(msg);
            }

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tenta = service.DeletaEmprestimo(id);

            if (!tenta)
            {
                return NotFound(tenta);
            }

            return Ok(tenta);
        }
    }
}

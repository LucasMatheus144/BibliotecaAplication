using ConsoleApp.DOMAIN.Entites;
using ConsoleApp.DOMAIN.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService service;

        public ClienteController(ClienteService _service)
        {
            this.service = _service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.ListarAllClientes());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.ListarPorId(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente body)
        {
            var result = service.CadastraCliente(body, out List<MensagemErro> erro);

            if (!result)
            {
                return UnprocessableEntity(erro);
            }

            return Ok(body);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Cliente body)
        {
            var result = service.EditarCliente(body, out List<MensagemErro> erro);

            if (!result)
            {
                return UnprocessableEntity(erro);
            }

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = service.ExcluiCliente(id);

            if (!result)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}

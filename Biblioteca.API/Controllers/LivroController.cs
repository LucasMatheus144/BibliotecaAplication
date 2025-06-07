using ConsoleApp.DOMAIN.Entites;
using ConsoleApp.DOMAIN.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroService service;

        public LivroController(LivroService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.ConsultarAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.ConsultaPorId(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Livro livro)
        {
            var result = service.NewLivro(livro, out List<MensagemErro> msg);

            if (!result)
            {
                return UnprocessableEntity(msg);
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Livro livro)
        {
            var result = service.PutLivro(livro, out List<MensagemErro> msg);

            if (!result)
            {
                return UnprocessableEntity(msg);
            }
            return Ok(livro);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tenta = service.DeleteLicro(id);

            if (!tenta)
            {
                return NotFound(tenta);
            }
            return Ok(tenta);
        }
    }
}

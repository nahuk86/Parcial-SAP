using DomainModel.Entities;
using DomainModel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _repo;
        public UsuariosController(IUsuarioRepository repo) => _repo = repo;

        // GET api/usuarios
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetAll()
            => Ok(_repo.GetAll());

        // GET api/usuarios/{id}
        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            var u = _repo.GetById(id);
            if (u == null) return NotFound();
            return Ok(u);
        }
    }
}

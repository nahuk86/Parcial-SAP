using DomainModel.Entities;
using DomainModel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : Controller
    {
        private readonly IProductoRepository _repo;
        public ProductosController(IProductoRepository repo) => _repo = repo;

        // GET api/productos
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetAll()
            => Ok(_repo.GetAll());

        // GET api/productos/{id}
        [HttpGet("{id}")]
        public ActionResult<Producto> Get(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            return Ok(p);
        }
    }
}

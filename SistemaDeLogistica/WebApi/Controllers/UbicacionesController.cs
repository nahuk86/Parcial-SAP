using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using DomainModel.Entities;
using System.Collections.Generic;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UbicacionesController : ControllerBase
    {
        private readonly IUbicacionService _service;
        public UbicacionesController(IUbicacionService service)
            => _service = service;

        // GET api/ubicaciones
        [HttpGet]
        public ActionResult<IEnumerable<Ubicacion>> GetAll()
            => Ok(_service.GetAll());

        // GET api/ubicaciones/{id}
        [HttpGet("{id}")]
        public ActionResult<Ubicacion> Get(int id)
            => Ok(_service.GetById(id));
    }

}

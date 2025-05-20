using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using DomainModel.Entities;
using System.Collections.Generic;
using BusinessLayer.DTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferenciasController : ControllerBase
    {
        private readonly ITransferenciaService _service;
        public TransferenciasController(ITransferenciaService service)
            => _service = service;

        // GET api/transferencias
        [HttpGet]
        public ActionResult<IEnumerable<MovimientoHistorico>> GetAll()
            => Ok(_service.GetAllMovimientos());

        // GET api/transferencias/{id}
        [HttpGet("{id}")]
        public ActionResult<MovimientoHistorico> Get(int id)
            => Ok(_service.GetMovimiento(id));

        // POST api/transferencias
        [HttpPost]
        public ActionResult<MovimientoHistorico> Create([FromBody] TransferRequest request)
        {
            var mov = _service.RealizarTransferencia(request);
            return CreatedAtAction(nameof(Get), new { id = mov.MovimientoHistoricoId }, mov);
        }
    }
}

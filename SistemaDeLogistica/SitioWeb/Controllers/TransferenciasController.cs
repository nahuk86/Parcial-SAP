using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SitioWeb.Services;
using BusinessLayer.DTOs;
using DomainModel.Entities;
using System.Threading.Tasks;

namespace SitioWeb.Controllers
{
    public class TransferenciasController : Controller
    {
        private readonly TransferenciaApiClient _api;

        public TransferenciasController(TransferenciaApiClient api)
        {
            _api = api;
        }

        // GET: /Transferencias/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Ubicaciones"] = new SelectList(
                await _api.GetUbicacionesAsync(),
                nameof(Ubicacion.UbicacionId),
                nameof(Ubicacion.Nombre));

            ViewData["Usuarios"] = new SelectList(
                await _api.GetUsuariosAsync(),
                nameof(Usuario.UsuarioId),
                nameof(Usuario.Nombre));

            // Productos los manejaremos en JS (o podrías pasarlos aquí también)
            return View(new TransferRequest());
        }

        // POST: /Transferencias/Create
        [HttpPost]
        public async Task<IActionResult> Create(TransferRequest request)
        {
            if (!ModelState.IsValid)
            {
                // volver a cargar Ubicaciones y Usuarios
                ViewData["Ubicaciones"] = new SelectList(
                    await _api.GetUbicacionesAsync(), nameof(Ubicacion.UbicacionId), nameof(Ubicacion.Nombre));
                ViewData["Usuarios"] = new SelectList(
                    await _api.GetUsuariosAsync(), nameof(Usuario.UsuarioId), nameof(Usuario.Nombre));
                return View(request);
            }

            var mov = await _api.CreateTransferenciaAsync(request);
            return RedirectToAction(nameof(Details), new { id = mov.MovimientoHistoricoId });
        }

        // GET: /Transferencias/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var mov = await _api.GetMovimientoAsync(id);
            if (mov == null) return NotFound();
            return View(mov);
        }
    }
}

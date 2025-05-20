using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Interfaces;
using BusinessLayer.DTOs;

namespace BusinessLayer.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly IUbicacionRepository _ubicRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IProductoRepository _productoRepo;
        private readonly IMovimientoHistoricoRepository _movRepo;

        public TransferenciaService(
            IUbicacionRepository ubicRepo,
            IUsuarioRepository usuarioRepo,
            IProductoRepository productoRepo,
            IMovimientoHistoricoRepository movRepo)
        {
            _ubicRepo = ubicRepo;
            _usuarioRepo = usuarioRepo;
            _productoRepo = productoRepo;
            _movRepo = movRepo;
        }

        // Ahora coincide con la interfaz

        public IEnumerable<MovimientoHistorico> GetAllMovimientos()
    => _movRepo.GetAll();

        public MovimientoHistorico RealizarTransferencia(TransferRequest request)
        {
            // 1. Obtener entidades
            var origen = _ubicRepo.GetById(request.OrigenId)
                         ?? throw new KeyNotFoundException($"Origen {request.OrigenId} no existe");
            var destino = _ubicRepo.GetById(request.DestinoId)
                         ?? throw new KeyNotFoundException($"Destino {request.DestinoId} no existe");
            var usuario = _usuarioRepo.GetById(request.UsuarioId)
                         ?? throw new KeyNotFoundException($"Usuario {request.UsuarioId} no existe");

            // 2. Mapear detalles DTO → dominio
            var detallesDominio = request.Detalles
                .Select(d =>
                {
                    var prod = _productoRepo.GetById(d.ProductoId)
                               ?? throw new KeyNotFoundException($"Producto {d.ProductoId} no existe");
                    return new DetalleTransferencia(0, prod, d.Cantidad);
                })
                .ToList();

            if (!detallesDominio.Any())
                throw new ArgumentException("La transferencia debe incluir al menos un detalle.");

            // 3. Validar stock y ajustar
            foreach (var d in detallesDominio)
            {
                if (!origen.TieneStockDisponible(d.Producto, d.Cantidad))
                    throw new InvalidOperationException($"Sin stock suficiente de {d.Producto.Nombre} en origen.");

                origen.ReducirStock(d.Producto, d.Cantidad);
                destino.AumentarStock(d.Producto, d.Cantidad);
            }

            // 4. Crear y persistir
            var movimiento = new MovimientoHistorico(0, usuario, origen, destino, detallesDominio);
            _movRepo.Add(movimiento);
            return movimiento;
        }

        public MovimientoHistorico GetMovimiento(int id)
            => _movRepo.GetById(id)
               ?? throw new KeyNotFoundException($"Movimiento {id} no encontrado");
    }
}

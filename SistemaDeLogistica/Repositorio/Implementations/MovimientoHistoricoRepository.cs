using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Interfaces;

namespace Repositorio.Implementations
{
    public class MovimientoHistoricoRepository : IMovimientoHistoricoRepository
    {
        private readonly List<MovimientoHistorico> _movimientos = new List<MovimientoHistorico>();

        public IEnumerable<MovimientoHistorico> GetAll()
            => _movimientos;

        public MovimientoHistorico GetById(int movimientoId)
            => _movimientos.FirstOrDefault(m => m.MovimientoHistoricoId == movimientoId);

        public IEnumerable<MovimientoHistorico> GetByDestino(int ubicacionId)
            => _movimientos.Where(m => m.Destino.UbicacionId == ubicacionId);

        public void Add(MovimientoHistorico movimiento)
        {
            // 1) Si viene con ID = 0, asignamos uno nuevo
            if (movimiento.MovimientoHistoricoId == 0)
            {
                var nextId = _movimientos.Any()
                    ? _movimientos.Max(m => m.MovimientoHistoricoId) + 1
                    : 1;
                movimiento.MovimientoHistoricoId = nextId;
            }

            // 2) Ahora sí validamos duplicados
            if (_movimientos.Any(m => m.MovimientoHistoricoId == movimiento.MovimientoHistoricoId))
                throw new InvalidOperationException("El movimiento ya existe.");

            _movimientos.Add(movimiento);
        }
    }
}

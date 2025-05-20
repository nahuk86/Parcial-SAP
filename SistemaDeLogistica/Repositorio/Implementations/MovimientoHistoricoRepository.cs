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

        public MovimientoHistorico GetById(int movimientoId)
            => _movimientos.FirstOrDefault(m => m.MovimientoHistoricoId == movimientoId);

        public IEnumerable<MovimientoHistorico> GetByDestino(int ubicacionId)
            => _movimientos.Where(m => m.Destino.UbicacionId == ubicacionId);

        public void Add(MovimientoHistorico movimiento)
        {
            if (_movimientos.Any(m => m.MovimientoHistoricoId == movimiento.MovimientoHistoricoId))
                throw new InvalidOperationException("El movimiento ya existe.");
            _movimientos.Add(movimiento);
        }
    }
}

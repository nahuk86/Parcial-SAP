using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IMovimientoHistoricoRepository
    {
        /// <summary>
        /// Obtiene un movimiento histórico por su identificador.
        /// </summary>
        MovimientoHistorico GetById(int movimientoId);

        /// <summary>
        /// Obtiene todos los movimientos que tienen como destino la ubicación indicada.
        /// </summary>
        IEnumerable<MovimientoHistorico> GetByDestino(int ubicacionId);

        /// <summary>
        /// Registra un nuevo movimiento histórico.
        /// </summary>
        void Add(MovimientoHistorico movimiento);
    }
}

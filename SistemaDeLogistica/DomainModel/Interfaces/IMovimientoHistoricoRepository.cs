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
        MovimientoHistorico GetById(int movimientoId);
        IEnumerable<MovimientoHistorico> GetByDestino(int ubicacionId);
        IEnumerable<MovimientoHistorico> GetAll();   // ← nuevo
        void Add(MovimientoHistorico movimiento);

    }
}

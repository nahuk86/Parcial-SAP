using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class MovimientoHistorico
    {
        public int MovimientoHistoricoId { get; private set; }
        public DateTime Fecha { get; private set; }
        public Usuario Usuario { get; private set; }
        public Ubicacion Origen { get; private set; }
        public Ubicacion Destino { get; private set; }
        public List<DetalleTransferencia> Detalles { get; private set; }

        public MovimientoHistorico(int movimientoId,
                                   Usuario usuario,
                                   Ubicacion origen,
                                   Ubicacion destino,
                                   List<DetalleTransferencia> detalles)
        {
            MovimientoHistoricoId = movimientoId;
            Fecha = DateTime.UtcNow;
            Usuario = usuario;
            Origen = origen;
            Destino = destino;
            Detalles = detalles ?? throw new ArgumentNullException(nameof(detalles));

            if (!detalles.Any())
                throw new ArgumentException("El movimiento debe contener al menos un detalle.");
        }

        public void RegistrarMovimiento()
        {
            // Aquí podrías incluir lógica adicional de auditoría o notificaciones
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class TransferRequest
    {
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
        public int UsuarioId { get; set; }
        public List<DetalleTransferDto> Detalles { get; set; }
    }

    /// <summary>
    /// DTO para cada línea de detalle de la transferencia.
    /// </summary>
    public class DetalleTransferDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}

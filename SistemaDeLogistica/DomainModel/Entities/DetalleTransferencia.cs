using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class DetalleTransferencia
    {
        public int DetalleTransferenciaId { get; private set; }
        public Producto Producto { get; private set; }
        public int Cantidad { get; private set; }

        public DetalleTransferencia(int detalleId, Producto producto, int cantidad)
        {
            DetalleTransferenciaId = detalleId;
            Producto = producto;
            Cantidad = cantidad;
        }

        public string GetDetalle() => $"{Producto.Nombre}: {Cantidad}";
    }
}

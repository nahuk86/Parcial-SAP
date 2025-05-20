using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Stock
    {
        public int StockId { get; private set; }
        public Producto Producto { get; private set; }
        public Ubicacion Ubicacion { get; private set; }
        public int Cantidad { get; private set; }

        public Stock(Producto producto, Ubicacion ubicacion, int cantidad)
        {
            Producto = producto;
            Ubicacion = ubicacion;
            Cantidad = cantidad;
        }

        public void AjustarCantidad(int delta)
        {
            if (Cantidad + delta < 0)
                throw new InvalidOperationException("No se puede ajustar stock por debajo de cero.");
            Cantidad += delta;
        }
    }
}

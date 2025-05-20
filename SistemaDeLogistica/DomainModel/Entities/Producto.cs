using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Producto
    {
        public int ProductoId { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public decimal Precio { get; private set; }

        public Producto(int productoId, string nombre, string descripcion, decimal precio)
        {
            ProductoId = productoId;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
        }

        public decimal GetPrecio() => Precio;
    }
}

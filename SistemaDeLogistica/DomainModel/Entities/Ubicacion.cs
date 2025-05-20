using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.ValueObjects;


namespace DomainModel.Entities
{
    public abstract class Ubicacion
    {
        public int UbicacionId { get; private set; }
        public string Nombre { get; private set; }
        public Direccion Direccion { get; private set; }
        private readonly List<Stock> _stock = new List<Stock>();
        public IReadOnlyCollection<Stock> Stock => _stock.AsReadOnly();

        protected Ubicacion(int ubicacionId, string nombre, Direccion direccion)
        {
            UbicacionId = ubicacionId;
            Nombre = nombre;
            Direccion = direccion;
        }

        public abstract bool TieneStockDisponible(Producto producto, int cantidad);
        public abstract void ReducirStock(Producto producto, int cantidad);
        public abstract void AumentarStock(Producto producto, int cantidad);

        protected Stock GetStockItem(Producto producto)
        {
            return _stock.FirstOrDefault(s => s.Producto.ProductoId == producto.ProductoId);
        }

        protected void AddOrUpdateStock(Producto producto, int cantidad)
        {
            var item = GetStockItem(producto);
            if (item == null)
            {
                _stock.Add(new Stock(producto, this, cantidad));
            }
            else
            {
                item.AjustarCantidad(cantidad);
            }
        }
    }
}

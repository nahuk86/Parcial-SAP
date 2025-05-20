using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.ValueObjects;


namespace DomainModel.Entities
{
    public class Tienda : Ubicacion
    {
        public Tienda(int id, string nombre, Direccion direccion)
    : base(id, nombre, direccion)
        {
        }

        public override bool TieneStockDisponible(Producto producto, int cantidad)
        {
            var item = GetStockItem(producto);
            return item != null && item.Cantidad >= cantidad;
        }

        public override void ReducirStock(Producto producto, int cantidad)
        {
            if (!TieneStockDisponible(producto, cantidad))
                throw new InvalidOperationException("Stock insuficiente en la tienda.");
            AddOrUpdateStock(producto, -cantidad);
        }

        public override void AumentarStock(Producto producto, int cantidad)
        {
            AddOrUpdateStock(producto, cantidad);
        }

        public void RecibirStock(Producto producto, int cantidad)
        {
            AumentarStock(producto, cantidad);
        }
    }
}

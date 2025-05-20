using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.ValueObjects;


namespace DomainModel.Entities
{
    public class Deposito : Ubicacion
    {
        public Deposito(int id, string nombre, Direccion direccion)
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
                throw new InvalidOperationException("Stock insuficiente en el depósito.");
            AddOrUpdateStock(producto, -cantidad);
        }

        public override void AumentarStock(Producto producto, int cantidad)
        {
            AddOrUpdateStock(producto, cantidad);
        }

        // Método de conveniencia para transferir directamente a una tienda
        public bool TransferirA(Tienda tienda, Producto producto, int cantidad, Usuario usuario)
        {
            ReducirStock(producto, cantidad);
            tienda.RecibirStock(producto, cantidad);
            return true;
        }
    }
}

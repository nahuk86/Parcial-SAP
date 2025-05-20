using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Interfaces;

namespace Repositorio.Implementations
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly List<Producto> _productos = new List<Producto>();

        public Producto GetById(int id) => _productos.FirstOrDefault(p => p.ProductoId == id);
        public IEnumerable<Producto> GetAll() => _productos;
        public void Add(Producto producto)
        {
            if (_productos.Any(p => p.ProductoId == producto.ProductoId))
                throw new InvalidOperationException($"Producto {producto.ProductoId} ya existe.");
            _productos.Add(producto);
        }
    }
}

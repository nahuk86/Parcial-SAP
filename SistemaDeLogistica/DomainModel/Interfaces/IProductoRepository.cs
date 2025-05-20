using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IProductoRepository
    {
        Producto GetById(int productoId);
        IEnumerable<Producto> GetAll();
        void Add(Producto producto);           // ← nuevo
    }

}


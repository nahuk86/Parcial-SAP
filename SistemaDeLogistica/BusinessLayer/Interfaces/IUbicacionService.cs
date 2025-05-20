using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IUbicacionService
    {
        Ubicacion GetById(int id);
        IEnumerable<Ubicacion> GetAll();
    }
}

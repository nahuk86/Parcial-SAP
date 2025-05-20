using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IUbicacionRepository
    {
        Ubicacion GetById(int ubicacionId);
        IEnumerable<Ubicacion> GetAll();
        void Add(Ubicacion ubicacion);
        void Update(Ubicacion ubicacion);
    }
}

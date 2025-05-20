using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Interfaces;

namespace Repositorio.Implementations
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly List<Ubicacion> _ubicaciones = new List<Ubicacion>();

        public Ubicacion GetById(int ubicacionId)
            => _ubicaciones.FirstOrDefault(u => u.UbicacionId == ubicacionId);

        public IEnumerable<Ubicacion> GetAll()
            => _ubicaciones;

        public void Add(Ubicacion ubicacion)
        {
            if (_ubicaciones.Any(u => u.UbicacionId == ubicacion.UbicacionId))
                throw new InvalidOperationException("La ubicación ya existe.");
            _ubicaciones.Add(ubicacion);
        }

        public void Update(Ubicacion ubicacion)
        {
            var existing = GetById(ubicacion.UbicacionId);
            if (existing == null)
                throw new KeyNotFoundException("Ubicación no encontrada.");
            // En memoria, la instancia ya está actualizada.
        }
    }
}

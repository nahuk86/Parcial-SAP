using BusinessLayer.Interfaces;
using DomainModel.Entities;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IUbicacionRepository _repository;

        public UbicacionService(IUbicacionRepository repository)
        {
            _repository = repository;
        }

        public Ubicacion GetById(int id)
        {
            var ubicacion = _repository.GetById(id);
            if (ubicacion == null)
                throw new KeyNotFoundException($"Ubicación con ID {id} no encontrada.");
            return ubicacion;
        }

        public IEnumerable<Ubicacion> GetAll()
        {
            return _repository.GetAll();
        }
    }
}

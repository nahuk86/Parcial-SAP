using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Interfaces;

namespace Repositorio.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();

        public Usuario GetById(int id) => _usuarios.FirstOrDefault(u => u.UsuarioId == id);
        public IEnumerable<Usuario> GetAll() => _usuarios;
        public void Add(Usuario usuario)
        {
            if (_usuarios.Any(u => u.UsuarioId == usuario.UsuarioId))
                throw new InvalidOperationException($"Usuario {usuario.UsuarioId} ya existe.");
            _usuarios.Add(usuario);
        }
    }
}

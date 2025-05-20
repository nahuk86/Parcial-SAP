using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        Usuario GetById(int usuarioId);

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        IEnumerable<Usuario> GetAll();

        /// <summary>
        /// Agrega un nuevo usuario.
        /// </summary>
        void Add(Usuario usuario);
    }
}
